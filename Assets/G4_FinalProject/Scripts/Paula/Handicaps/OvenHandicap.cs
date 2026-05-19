using UnityEngine;

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

    private void Awake()
    {
        hits_required = Random.Range(min_hits, max_hits + 1);
        Debug.Log($"Oven handicap active. Needs {hits_required} hits to unlock door.");

        if (door_hinge != null)
        {
            // Guardem límits originals i bloquejem la porta (min=max=0)
            original_limits = door_hinge.limits;
            JointLimits locked_limits = original_limits;
            locked_limits.min = 0;
            locked_limits.max = 0;
            door_hinge.limits = locked_limits;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (resolved) return;
        if (collision.gameObject.CompareTag(hit_tag))
        {
            // Opcional: comprovar si el cop és a la zona de detecció
            if (hit_detection_point != null)
            {
                float dist = Vector3.Distance(collision.contacts[0].point, hit_detection_point.position);
                if (dist > 0.3f) return; // ignora cops lluny
            }

            current_hits++;
            Debug.Log($"Oven hit! {current_hits}/{hits_required}");
            if (current_hits >= hits_required)
            {
                Resolve();
            }
        }
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
        // No destruïm l'objecte del forn perquè és part de l'escena
        Debug.Log("Oven handicap resolved.");
    }
}