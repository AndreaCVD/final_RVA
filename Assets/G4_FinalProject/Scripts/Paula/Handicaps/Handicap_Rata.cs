using UnityEngine;

using System.Collections;

public class Handicap_Rata : MonoBehaviour, IHandicap
{
    private bool resolved = false;
    public bool IsResolved => resolved;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float waitTimeAtWaypoint = 1f;
    [SerializeField] private Vector3 roamAreaCenter;
    [SerializeField] private Vector3 roamAreaSize = new Vector3(4f, 0.5f, 3f);

    [Header("Resolved")]
    [SerializeField] private string trashTag = "Trash";
    [SerializeField] private string windowTag = "Window";

    private Vector3 targetPosition;
    private bool isMoving = true;
    private bool isGrabbed = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(_ => OnGrabbed());
            grabInteractable.selectExited.AddListener(_ => OnReleased());
        }

        if (roamAreaCenter == Vector3.zero) roamAreaCenter = transform.position;
        SetRandomTarget();
    }

    private void OnGrabbed()
    {
        isGrabbed = true;
        isMoving = false;
        StopAllCoroutines();
    }

    private void OnReleased()
    {
        isGrabbed = false;
        if (!resolved)
        {
            isMoving = true;
            StartCoroutine(MoveRoutine());
        }
    }

    private void Start()
    {
        if (!isGrabbed && !resolved)
            StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (!resolved && !isGrabbed)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f && !resolved && !isGrabbed)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            if (resolved || isGrabbed) yield break;
            yield return new WaitForSeconds(waitTimeAtWaypoint);
            if (!resolved && !isGrabbed) SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        float x = Random.Range(roamAreaCenter.x - roamAreaSize.x / 2, roamAreaCenter.x + roamAreaSize.x / 2);
        float z = Random.Range(roamAreaCenter.z - roamAreaSize.z / 2, roamAreaCenter.z + roamAreaSize.z / 2);
        targetPosition = new Vector3(x, transform.position.y, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (resolved) return;
        if (other.CompareTag(trashTag) || other.CompareTag(windowTag))
        {
            Resolve();
        }
    }

    public void Resolve()
    {
        if (resolved) return;
        resolved = true;
        HandicapManager.Instance.RemoveHandicap(this);
        Destroy(gameObject);
    }
}