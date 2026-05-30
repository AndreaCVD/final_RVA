using UnityEngine;
using System.Collections;

public class OvenHandicap : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool is_resolved => resolved;

    [Header("Oven Door")]
    [SerializeField] private ConfigurableJoint door_joint; // Ara és ConfigurableJoint
    [SerializeField] private string hit_tag = "Destroyer";

    [Header("Hit Settings")]
    [SerializeField] private int min_hits = 3;
    [SerializeField] private int max_hits = 10;
    // Eliminat hit_detection_point

    private int hits_required;
    private int current_hits = 0;
    private ConfigurableJointMotion original_x, original_y, original_z;
    private ConfigurableJointMotion original_ax, original_ay, original_az;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject hit_feedback_object;
    [SerializeField] private Color hit_flash_color = Color.red;
    [SerializeField] private float hit_flash_duration = 0.1f;
    private Material original_material;
    private MeshRenderer feedback_renderer;
    private Coroutine flash_coroutine;

    private void Awake()
    {
        if (door_joint == null) door_joint = GetComponent<ConfigurableJoint>();
        if (door_joint == null)
        {
            Debug.LogError("OvenHandicap: No ConfigurableJoint found!");
            enabled = false;
            return;
        }

        hits_required = Random.Range(min_hits, max_hits + 1);
        Debug.Log($"Oven handicap active. Needs {hits_required} hits to unlock door.");

        // Guardar moviments originals
        original_x = door_joint.xMotion;
        original_y = door_joint.yMotion;
        original_z = door_joint.zMotion;
        original_ax = door_joint.angularXMotion;
        original_ay = door_joint.angularYMotion;
        original_az = door_joint.angularZMotion;

        // Bloquejar tots els eixos
        door_joint.xMotion = ConfigurableJointMotion.Locked;
        door_joint.yMotion = ConfigurableJointMotion.Locked;
        door_joint.zMotion = ConfigurableJointMotion.Locked;
        door_joint.angularXMotion = ConfigurableJointMotion.Locked;
        door_joint.angularYMotion = ConfigurableJointMotion.Locked;
        door_joint.angularZMotion = ConfigurableJointMotion.Locked;

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
            current_hits++;
            Debug.Log($"Oven hit! {current_hits}/{hits_required}");
            FlashRed();
            if (current_hits >= hits_required)
                Resolve();
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
        // Restaurar moviments originals
        door_joint.xMotion = original_x;
        door_joint.yMotion = original_y;
        door_joint.zMotion = original_z;
        door_joint.angularXMotion = original_ax;
        door_joint.angularYMotion = original_ay;
        door_joint.angularZMotion = original_az;
        Debug.Log("Oven door unlocked!");
        HandicapManager.instance.RemoveHandicap(this); // Atenció: RemoveHandicap espera que l'handicap estigui a la llista; però aquest no hi és perquè no es va afegir via SpawnRandomHandicap. Hauràs de gestionar-ho a part.
        // En lloc de RemoveHandicap, simplement pots desactivar aquest script.
        enabled = false;
    }
}