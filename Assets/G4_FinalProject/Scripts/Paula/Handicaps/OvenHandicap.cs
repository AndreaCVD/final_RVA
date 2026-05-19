using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OvenHandicap : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool is_resolved => resolved;

    [Header("Oven Settings")]
    [SerializeField] private ConfigurableJoint oven_door_joint;
    [SerializeField] private Transform hit_detection_point;
    [SerializeField] private float hit_radius = 0.2f;

    [Header("Hit Settings")]
    [SerializeField] private int min_hits_required = 3;
    [SerializeField] private int max_hits_required = 10;
    [SerializeField] private float hit_cooldown = 0.5f;

    [Header("VR Hand Settings")]
    [SerializeField] private LayerMask hand_layer_mask; // Assigna la capa de les mans VR

    private int hits_required;
    private int current_hits = 0;
    private float last_hit_time = 0f;

    // Guardem els valors originals del joint
    private bool original_angular_x_locked;
    private bool original_angular_y_locked;
    private bool original_angular_z_locked;

    private void Awake()
    {
        if (oven_door_joint == null)
            oven_door_joint = GetComponent<ConfigurableJoint>();

        if (oven_door_joint == null)
        {
            Debug.LogError("OvenHandicap: No ConfigurableJoint found!");
            enabled = false;
            return;
        }

        if (hit_detection_point == null)
            hit_detection_point = transform;

        // Guardem configuració original
        original_angular_x_locked = oven_door_joint.angularXMotion == ConfigurableJointMotion.Locked;
        original_angular_y_locked = oven_door_joint.angularYMotion == ConfigurableJointMotion.Locked;
        original_angular_z_locked = oven_door_joint.angularZMotion == ConfigurableJointMotion.Locked;

        LockDoor();
        hits_required = Random.Range(min_hits_required, max_hits_required + 1);
        Debug.Log($"OvenHandicap: Need {hits_required} hits to unlock.");
    }

    private void LockDoor()
    {
        oven_door_joint.angularXMotion = ConfigurableJointMotion.Locked;
        oven_door_joint.angularYMotion = ConfigurableJointMotion.Locked;
        oven_door_joint.angularZMotion = ConfigurableJointMotion.Locked;
    }

    private void UnlockDoor()
    {
        oven_door_joint.angularXMotion = original_angular_x_locked ? ConfigurableJointMotion.Locked : ConfigurableJointMotion.Limited;
        oven_door_joint.angularYMotion = original_angular_y_locked ? ConfigurableJointMotion.Locked : ConfigurableJointMotion.Limited;
        oven_door_joint.angularZMotion = original_angular_z_locked ? ConfigurableJointMotion.Locked : ConfigurableJointMotion.Limited;
        Debug.Log("OvenHandicap: Door unlocked!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (resolved) return;

        // Comprova si és una mà VR (per layer o tag)
        if (!IsVRHand(other.gameObject)) return;

        // Comprova si està dins la zona de colpeig
        float dist = Vector3.Distance(other.transform.position, hit_detection_point.position);
        if (dist > hit_radius) return;

        RegisterHit();
    }

    private bool IsVRHand(GameObject obj)
    {
        // Opció 1: per layer
        if (((1 << obj.layer) & hand_layer_mask) != 0)
            return true;

        // Opció 2: per tag (si les mans tenen tag "Hand")
        if (obj.CompareTag("Hand"))
            return true;

        // Opció 3: per component (si té XR Controller)
        if (obj.GetComponentInParent<XRController>() != null)
            return true;

        return false;
    }

    private void RegisterHit()
    {
        if (Time.time - last_hit_time < hit_cooldown) return;

        last_hit_time = Time.time;
        current_hits++;
        Debug.Log($"OvenHandicap: Hit {current_hits}/{hits_required}");

        // Efecte visual/so (opcional)
        // (Pots afegir una petita vibració al comandament aquí si vols)

        if (current_hits >= hits_required)
        {
            Resolve();
        }
    }

    public void Resolve()
    {
        if (resolved) return;
        resolved = true;
        UnlockDoor();
        HandicapManager.instance.RemoveHandicap(this);
        // No destruïm el forn!
        Debug.Log("OvenHandicap: Resolved!");
    }

    // Per depuració: dibuixa la zona de colpeig a l'escena
    private void OnDrawGizmosSelected()
    {
        if (hit_detection_point != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hit_detection_point.position, hit_radius);
        }
    }
}