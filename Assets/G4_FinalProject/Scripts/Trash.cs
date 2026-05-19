using UnityEngine;

public class Trash : MonoBehaviour
{
    void Start()
    {
        // El collider debe ser trigger
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Destruir cualquier objeto que entre
        Destroy(other.gameObject);
        Debug.Log($"Destruido: {other.name}");
    }
}