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
        if (other.gameObject.tag == "Pizza" || other.gameObject.tag == "Ingredient" || other.gameObject.tag == "Rata")
        {
            // Destruir cualquier objeto que entre
            Destroy(other.gameObject);
            Debug.Log($"Destruido: {other.name}");
        }

    }
}