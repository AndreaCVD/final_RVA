using UnityEngine;


public class RatHandicap : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool is_resolved => resolved;

    [Header("Movement")]
    [SerializeField] private float move_speed = 5f;          // molt més ràpida
    [SerializeField] private float direction_change_interval = 0.2f; // canvia direcció cada 0.2 segons
    [SerializeField] private Vector3 roam_area_center;
    [SerializeField] private Vector3 roam_area_size = new Vector3(4f, 0.5f, 3f);

    [Header("Resolution")]
    [SerializeField] private string trash_tag = "Trash";
    [SerializeField] private string window_tag = "Window";

    private Vector3 current_direction;
    private float next_direction_change_time;
    private bool is_grabbed = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab_interactable;

    private void Awake()
    {
        grab_interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab_interactable != null)
        {
            grab_interactable.selectEntered.AddListener(_ => OnGrabbed());
            grab_interactable.selectExited.AddListener(_ => OnReleased());
        }

        if (roam_area_center == Vector3.zero)
            roam_area_center = transform.position;

        SetRandomPosition();
        SetRandomDirection();
    }

    private void Start()
    {
        next_direction_change_time = Time.time + direction_change_interval;
    }

    private void Update()
    {
        if (resolved || is_grabbed) return;

        // Canvi aleatori de direcció cada interval (molt freqüent)
        if (Time.time >= next_direction_change_time)
        {
            SetRandomDirection();
            next_direction_change_time = Time.time + direction_change_interval;
        }

        Vector3 new_pos = transform.position + current_direction * move_speed * Time.deltaTime;
        new_pos = BounceInsideArea(new_pos);   // rebota en comptes de clampar
        transform.position = new_pos;
    }

    // Direcció aleatòria horitzontal (X,Z), normalitzada
    private void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        current_direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    // Posició inicial aleatòria dins l'àrea
    private void SetRandomPosition()
    {
        float x = Random.Range(roam_area_center.x - roam_area_size.x / 2, roam_area_center.x + roam_area_size.x / 2);
        float z = Random.Range(roam_area_center.z - roam_area_size.z / 2, roam_area_center.z + roam_area_size.z / 2);
        transform.position = new Vector3(x, roam_area_center.y, z);
    }

    // Funció de rebot: si la rata surt dels límits, la reposiciona a dins i canvia la direcció
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

        // Si ha tocat un límit, normalitzem la direcció per mantenir velocitat constant
        if (bounced)
        {
            current_direction.Normalize();
            // Opcional: tornar a canviar l'interval per fer-la més caòtica
            next_direction_change_time = Time.time + direction_change_interval * 0.5f;
        }

        return new_pos;
    }

    private void OnGrabbed()
    {
        is_grabbed = true;
    }

    private void OnReleased()
    {
        is_grabbed = false;
        SetRandomDirection();
        next_direction_change_time = Time.time + direction_change_interval;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (resolved) return;
        if (other.CompareTag(trash_tag) || other.CompareTag(window_tag))
        {
            Resolve();
        }
    }

    public void Resolve()
    {
        if (resolved) return;
        resolved = true;
        HandicapManager.instance.RemoveHandicap(this);
        Destroy(gameObject);
    }
}