using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Display (fix a l'escena)")]
    public Transform commandDisplay;
    public float slotSpacing = 0.3f;

    private NPC_AudioRandom_new npcAudio;

    [HideInInspector] public List<string> currentOrder = new List<string>();

    [Header("Animation")]
    public Animator animator;

    private void Awake()
    {
        npcAudio = GetComponent<NPC_AudioRandom_new>();
    }

    // LevelController crida això en el moment del spawn
    public void SetOrder(Recipe recipe, List<GameObject> prefabs)
    {
        //Aqui pasa el recipe i puede llamar a Ticket para hacer display ticket
        //DebugLog recipe
        currentOrder = new List<string>(recipe.ingredients);

        //Debug.Log(string.Join(", ", currentOrder));

        DisplayOrder(prefabs);

        //if (npcAudio != null)
        //{
        //    npcAudio.PlayRandomAudio();

        //}
        //else
        //{
        //    Debug.Log("No agafa npc audio");           
                
        //}
    }

    void DisplayOrder(List<GameObject> prefabs) //no hace nada???
    {

        // Debug — ver nombres de prefabs disponibles
        //foreach (var p in prefabs)
        //    Debug.Log($"Prefab disponible: '{p.name}'");

        foreach (Transform child in commandDisplay)
            Destroy(child.gameObject);

        float totalWidth = (currentOrder.Count - 1) * slotSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < currentOrder.Count; i++)
        {
            GameObject prefab = prefabs.Find(p => p.name == currentOrder[i]);
            if (prefab == null)
            {
                Debug.LogWarning($"Prefab no trobat per ingredient: {currentOrder[i]}");
                continue;
            }
            Vector3 offset = new Vector3(startX + i * slotSpacing, 0, 0);
            GameObject slot = Instantiate(prefab, commandDisplay);
            slot.transform.localPosition = offset;
            slot.transform.localRotation = Quaternion.identity;
        }
    }

    public void DismissClient() //se llama??
    {
        gameObject.SetActive(false); 
    }

    public void AnimClient() //coge animator NpC - marxa
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("dismiss", true);
    }

    public void OnDissmissAnimationEnd()
    {
        Destroy(gameObject);
    }

    public void StartDismiss(System.Action onComplete)
    {
        StartCoroutine(DismissCoroutine(onComplete));
    }

    private IEnumerator DismissCoroutine(System.Action onComplete)
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("NPCController: No hay Animator! Destruyendo directamente.");
            Destroy(gameObject);
            onComplete?.Invoke();
            yield break;
        }

        animator.SetBool("dismiss", true);
        Debug.Log("NPCController: dismiss = true, esperando animación...");

        yield return null; // espera 1 frame para que el animator procese

        // Log para ver el nombre real del estado actual
        Debug.Log($"Estado actual: '{animator.GetCurrentAnimatorStateInfo(0).shortNameHash}' " +
                  $"IsName(dismiss): {animator.GetCurrentAnimatorStateInfo(0).IsName("dismiss")}");

        // Espera máximo X segundos en lugar de depender del nombre del estado
        float timeout = 2f;
        float elapsed = 0f;

        // Espera a que ENTRE en un estado con dismiss (tag o nombre)
        yield return new WaitUntil(() => {
            elapsed += Time.deltaTime;
            var info = animator.GetCurrentAnimatorStateInfo(0);
            bool inDismiss = info.IsName("dismiss") || info.IsTag("dismiss");
            if (!inDismiss) Debug.Log($"Esperando estado dismiss... elapsed: {elapsed:F1}s");
            return inDismiss || elapsed >= timeout;
        });

        if (elapsed >= timeout)
        {
            Debug.LogWarning("NPCController: Timeout esperando estado dismiss. Destruyendo igualmente.");
            Destroy(gameObject);
            onComplete?.Invoke();
            yield break;
        }

        // Espera a que TERMINE la animación
        elapsed = 0f;
        yield return new WaitUntil(() => {
            elapsed += Time.deltaTime;
            var info = animator.GetCurrentAnimatorStateInfo(0);
            return info.normalizedTime >= 1f || elapsed >= timeout;
        });

        Debug.Log("NPCController: Animación dismiss terminada. Destruyendo NPC.");
        Destroy(gameObject);
        onComplete?.Invoke();
    }
}