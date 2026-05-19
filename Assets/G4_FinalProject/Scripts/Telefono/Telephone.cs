using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

public class Telephone : MonoBehaviour
{
    [SerializeField] MainLoop main_loop;
    public XRGrabInteractable grab_interactable;
    private bool isReady = false;

    private void Awake()
    {
        //var socket = GetComponent<XRSocketInteractor>();
        //socket.selectEntered.AddListener(OnSelectEntered);

        //grab_interactable = GetComponent<XRGrabInteractable>();
        //grab_interactable.selectEntered.AddListener(OnGrabbed);

        grab_interactable = GetComponent<XRGrabInteractable>();
        grab_interactable.selectEntered.AddListener(OnGrabbed);
    }

    //private void OnSelectEntered(SelectEnterEventArgs args)
    //{
    //    // 'args.interactableObject' is the object that entered the socket
    //    Debug.Log("telefono cogido " + args);
    //}
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); // espera antes de activar
        isReady = true;
    }
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!isReady) return; // ignora eventos al inicio

        Debug.Log("Telefono cogido");
        main_loop._Telephone();
    }

    private void OnReleased()
    {

    }

}
