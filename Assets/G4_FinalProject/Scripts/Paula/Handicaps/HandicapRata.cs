using UnityEngine;
using System.Collections;


public class Handicap_Rat : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool is_resolved => resolved;

    [Header("Movement")]
    [SerializeField] private float move_speed = 1.5f;
    [SerializeField] private float wait_time_at_waypoint = 1f;
    [SerializeField] private Vector3 roam_area_center;
    [SerializeField] private Vector3 roam_area_size = new Vector3(4f, 0.5f, 3f);

    [Header("Resolution")]
    [SerializeField] private string trash_tag = "Trash";
    [SerializeField] private string window_tag = "Window";

    private Vector3 target_position;
    private bool is_moving = true;
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

        if (roam_area_center == Vector3.zero) roam_area_center = transform.position;
        SetRandomTarget();
    }

    private void OnGrabbed()
    {
        is_grabbed = true;
        is_moving = false;
        StopAllCoroutines();
    }

    private void OnReleased()
    {
        is_grabbed = false;
        if (!resolved)
        {
            is_moving = true;
            StartCoroutine(MoveRoutine());
        }
    }

    private void Start()
    {
        if (!is_grabbed && !resolved)
            StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (!resolved && !is_grabbed)
        {
            while (Vector3.Distance(transform.position, target_position) > 0.1f && !resolved && !is_grabbed)
            {
                transform.position = Vector3.MoveTowards(transform.position, target_position, move_speed * Time.deltaTime);
                yield return null;
            }
            if (resolved || is_grabbed) yield break;
            yield return new WaitForSeconds(wait_time_at_waypoint);
            if (!resolved && !is_grabbed) SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        float x = Random.Range(roam_area_center.x - roam_area_size.x / 2, roam_area_center.x + roam_area_size.x / 2);
        float z = Random.Range(roam_area_center.z - roam_area_size.z / 2, roam_area_center.z + roam_area_size.z / 2);
        target_position = new Vector3(x, transform.position.y, z);
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