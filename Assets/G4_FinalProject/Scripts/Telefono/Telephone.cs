using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
public class Telephone : MonoBehaviour
{
    [SerializeField] MainLoop main_loop;
    private XRGrabInteractable grab_interactable;

    private void Awake()
    {
        grab_interactable = GetComponent<XRGrabInteractable>();
        if (grab_interactable != null)
        {
            grab_interactable.selectEntered.AddListener(_ => OnGrabbed());
            grab_interactable.selectExited.AddListener(_ => OnReleased());
        }


    }
    private void OnGrabbed()
    {
        //El telefono se ha cojido
        main_loop.Telephone();
    }

    private void OnReleased()
    {

    }

}
