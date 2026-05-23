using UnityEngine;
using System.Collections;

public class OvenHandicap : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool is_resolved => resolved;

    [Header("Oven Door")]
    [SerializeField] private HingeJoint door_hinge;
    [SerializeField] private string hit_tag = "Destroyer";

    [Header("Hit Settings")]
    [SerializeField] private int min_hits = 3;
    [SerializeField] private int max_hits = 10;
    [SerializeField] private Transform hit_detection_point;

    private int hits_required;
    private int current_hits = 0;
    private JointLimits original_limits;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject hit_feedback_object;
    [SerializeField] private Color hit_flash_color = Color.red;
    [SerializeField] private float hit_flash_duration = 0.1f;
    private Material original_material;
    private MeshRenderer feedback_renderer;
    private Coroutine flash_coroutine;

    private void Awake()
    {
        hits_required = Random.Range(min_hits, max_hits + 1);
        Debug.Log($"Oven handicap active. Needs {hits_required} hits to unlock door.");

        if (door_hinge != null)
        {
            original_limits = door_hinge.limits;
            JointLimits locked_limits = original_limits;
            locked_limits.min = 0;
            locked_limits.max = 0;
            door_hinge.limits = locked_limits;
        }

        if (hit_feedback_object == null) hit_feedback_object = gameObject;
        feedback_renderer = hit_feedback_object.GetComponent<MeshRenderer>();
        if (feedback_renderer != null && feedback_renderer.material != null)
            original_material = feedback_renderer.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (resolved) return;
        if (collision.gameObject.CompareTag(hit_tag))
        {
            if (hit_detection_point != null)
            {
                float dist = Vector3.Distance(collision.contacts[0].point, hit_detection_point.position);
                if (dist > 0.3f) return;
            }

            current_hits++;
            Debug.Log($"Oven hit! {current_hits}/{hits_required}");
            FlashRed();
            if (current_hits >= hits_required)
            {
                Resolve();
            }
        }
    }

    private void FlashRed()
    {
        if (feedback_renderer == null) return;
        if (flash_coroutine != null) StopCoroutine(flash_coroutine);
        flash_coroutine = StartCoroutine(DoFlashRed());
    }

    private IEnumerator DoFlashRed()
    {
        feedback_renderer.material.color = hit_flash_color;
        yield return new WaitForSeconds(hit_flash_duration);
        if (feedback_renderer != null && original_material != null)
            feedback_renderer.material = original_material;
    }

    public void Resolve()
    {
        if (resolved) return;
        resolved = true;

        if (door_hinge != null)
        {
            door_hinge.limits = original_limits;
            Debug.Log("Oven door unlocked!");
        }

        HandicapManager.instance.RemoveHandicap(this);
        Debug.Log("Oven handicap resolved.");
    }
}