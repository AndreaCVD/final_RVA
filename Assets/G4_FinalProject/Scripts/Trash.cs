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
        // Si el objeto que entra es una rata
        /*if (other.CompareTag("Rata"))
        {
            // Buscar el script HandicapRata en la rata
            HandicapRata rata = other.GetComponent<HandicapRata>();

            if (rata != null)
            {
                // Cambiar la bool a true
                rata.resolved = true;
                Debug.Log("Rata resuelta = true");
            }
        }*/

        // Destruir cualquier objeto que entre
        Destroy(other.gameObject);
        Debug.Log($"Destruido: {other.name}");
    }
}