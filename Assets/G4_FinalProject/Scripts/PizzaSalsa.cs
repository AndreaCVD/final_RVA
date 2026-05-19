// Este script va en el objeto con el Box Collider NO-trigger (el de abajo)
using UnityEngine;

public class PizzaSalsa : MonoBehaviour
{
    public Material materialConSalsa;
    private Renderer rendererPizza;
    private bool salsaAplicada = false;

    void Start()
    {
        // Buscar el renderer en este objeto o en el padre
        rendererPizza = GetComponent<Renderer>();
        if (rendererPizza == null)
            rendererPizza = GetComponentInParent<Renderer>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("ENTRA");
        if (salsaAplicada) return;

        if (rendererPizza != null && materialConSalsa != null)
        {
            rendererPizza.material = materialConSalsa;
            salsaAplicada = true;
            Debug.Log("Salsa aplicada al collider físico");
        }
    }
}