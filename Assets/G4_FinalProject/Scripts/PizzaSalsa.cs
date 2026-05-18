using UnityEngine;

public class PizzaSalsa : MonoBehaviour
{
    public Material materialConSalsa;
    private Renderer rendererPizza;
    private bool tieneSalsa = false;

    void Start()
    {
        rendererPizza = GetComponent<Renderer>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("DETECTA COLISION PARTICLES");
        if (!tieneSalsa)
        {
            tieneSalsa = true;
            rendererPizza.material = materialConSalsa;
            Debug.Log("Salsa detectada en la pizza");
        }
    }
}