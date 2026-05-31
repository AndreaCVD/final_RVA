using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InstantiateOnceOnGrab : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabAInstanciar;
    public Transform referenciaDestino;

    private bool yaSeInstancio = false;

    void Start()
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (!yaSeInstancio && prefabAInstanciar != null && referenciaDestino != null)
        {
            yaSeInstancio = true;

            // Instanciar el prefab
            //GameObject nuevoObjeto = Instantiate(prefabAInstanciar, referenciaDestino.position, referenciaDestino.rotation);
            StartCoroutine(InstanciarConDelay());

            // Configurar el Rigidbody
            /*Rigidbody rb = nuevoObjeto.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            Debug.Log($"Instanciado: {prefabAInstanciar.name} con física activada");*/
        }
    }
    //InstatiateOnceOnGrab().SetActive(false);
    IEnumerator InstanciarConDelay()
    {
        yield return new WaitForSeconds(1f);

        GameObject nuevoObjeto = Instantiate(
            prefabAInstanciar,
            referenciaDestino.position,
            referenciaDestino.rotation
        );

        Rigidbody rb = nuevoObjeto.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

    }
}

