using UnityEngine;

public class RatHandicap : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool is_resolved => resolved;

    [Header("Hit Settings")]
    [SerializeField] private int min_hits = 3;
    [SerializeField] private int max_hits = 8;
    [SerializeField] private string hit_tag = "Destroyer";

    private int hits_required;
    private int current_hits = 0;

    [Header("Movement")]
    [SerializeField] private bool enable_movement = true;
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float direction_change_interval = 0.2f;
    [SerializeField] private Vector3 roam_area_center;
    [SerializeField] private Vector3 roam_area_size = new Vector3(4f, 0.5f, 3f);

    private Vector3 current_direction;
    private float next_direction_change_time;

    private void Awake()
    {
        hits_required = Random.Range(min_hits, max_hits + 1);
        Debug.Log($"Rat spawned. Needs {hits_required} hits with tag '{hit_tag}' to resolve.");

        if (roam_area_center == Vector3.zero)
            roam_area_center = transform.position;
        SetRandomPosition();
        if (enable_movement) SetRandomDirection();
    }

    private void Start()
    {
        if (enable_movement)
            next_direction_change_time = UnityEngine.Time.time + direction_change_interval;
    }

    private void Update()
    {
        if (resolved || !enable_movement) return;

        if (UnityEngine.Time.time >= next_direction_change_time)
        {
            SetRandomDirection();
            next_direction_change_time = UnityEngine.Time.time + direction_change_interval;
        }

        Vector3 new_pos = transform.position + current_direction * move_speed * UnityEngine.Time.deltaTime;
        new_pos = BounceInsideArea(new_pos);
        transform.position = new_pos;
    }

    private void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        current_direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    private void SetRandomPosition()
    {
        float x = Random.Range(roam_area_center.x - roam_area_size.x / 2, roam_area_center.x + roam_area_size.x / 2);
        float z = Random.Range(roam_area_center.z - roam_area_size.z / 2, roam_area_center.z + roam_area_size.z / 2);
        transform.position = new Vector3(x, roam_area_center.y, z);
    }

    private Vector3 BounceInsideArea(Vector3 pos)
    {
        Vector3 new_pos = pos;
        bool bounced = false;
        float half_x = roam_area_size.x / 2;
        float half_z = roam_area_size.z / 2;

        if (pos.x < roam_area_center.x - half_x)
        {
            new_pos.x = roam_area_center.x - half_x;
            current_direction.x = Mathf.Abs(current_direction.x);
            bounced = true;
        }
        else if (pos.x > roam_area_center.x + half_x)
        {
            new_pos.x = roam_area_center.x + half_x;
            current_direction.x = -Mathf.Abs(current_direction.x);
            bounced = true;
        }

        if (pos.z < roam_area_center.z - half_z)
        {
            new_pos.z = roam_area_center.z - half_z;
            current_direction.z = Mathf.Abs(current_direction.z);
            bounced = true;
        }
        else if (pos.z > roam_area_center.z + half_z)
        {
            new_pos.z = roam_area_center.z + half_z;
            current_direction.z = -Mathf.Abs(current_direction.z);
            bounced = true;
        }

        if (bounced) current_direction.Normalize();
        return new_pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (resolved) return;
        if (collision.gameObject.CompareTag(hit_tag))
        {
            current_hits++;
            Debug.Log($"Rat hit! {current_hits}/{hits_required}");
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
        HandicapManager.instance.RemoveHandicap(this);
        Destroy(gameObject);
        Debug.Log("Rat resolved and destroyed.");
    }
}