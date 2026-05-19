using UnityEngine;

public class PizzaSalsa : MonoBehaviour
{
    public Material materialConSalsa;

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("ENTRA");
        Debug.Log(other);
        if (!other.CompareTag("Pizza"))
            return;

        Debug.Log("PIZZA");
        // Intentar conseguir el Renderer del padre, si no, del mismo objeto
        Renderer rendererPizza = other.transform.parent != null
            ? other.transform.parent.GetComponent<Renderer>()
            : other.GetComponent<Renderer>();

        if (rendererPizza != null && materialConSalsa != null)
        {
            rendererPizza.material = materialConSalsa;
            Debug.Log("Salsa aplicada");
        }
    }
}