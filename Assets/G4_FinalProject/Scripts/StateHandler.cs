using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public enum Estado { Crudo, Cocinado, Quemado }

    [Header("Materiales")]
    public Material materialCocinado;
    public Material materialQuemado;

    [Header("Tiempos")]
    public float tiempoParaCocinar = 5f;
    public float tiempoParaQuemar = 10f;

    private Estado estadoActual = Estado.Crudo;
    private Renderer objectRenderer;
    private float tiempoEntrada;
    private bool estaEnHorno = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
            objectRenderer = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        if (estaEnHorno)
        {
            float tiempoTranscurrido = Time.time - tiempoEntrada;

            if (estadoActual == Estado.Crudo && tiempoTranscurrido >= tiempoParaCocinar)
            {
                CambiarEstado(Estado.Cocinado);
            }
            else if (estadoActual == Estado.Cocinado && tiempoTranscurrido >= tiempoParaQuemar)
            {
                CambiarEstado(Estado.Quemado);
            }
        }
    }

    public void EntrarEnHorno(float tiempoActual)
    {
        if (!estaEnHorno)
        {
            estaEnHorno = true;
            tiempoEntrada = tiempoActual;
            Debug.Log(gameObject.name + " entro al horno - CRUDO");
        }
    }

    public void SalirDelHorno()
    {
        estaEnHorno = false;
        Debug.Log(gameObject.name + " salio del horno - Estado: " + estadoActual);
    }

    void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual = nuevoEstado;

        if (nuevoEstado == Estado.Cocinado && materialCocinado != null)
        {
            objectRenderer.material = materialCocinado;
            Debug.Log(gameObject.name + " se COCINO");
        }
        else if (nuevoEstado == Estado.Quemado && materialQuemado != null)
        {
            objectRenderer.material = materialQuemado;
            Debug.Log(gameObject.name + " se QUEMO");
        }
    }

    public Estado GetEstadoActual()
    {
        return estadoActual;
    }
}