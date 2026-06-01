using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InGameMenu : MonoBehaviour
{

    [SerializeField] GameObject overlayLibro;
    [SerializeField] GameObject overlayTicket;
    [SerializeField] InputActionReference toggleAction;

    void OnEnable()
    {
        toggleAction.action.performed += OnToggle;
        toggleAction.action.Enable();
    }

    void OnDisable()
    {
        toggleAction.action.performed -= OnToggle;
        toggleAction.action.Disable();
    }

    void OnToggle(InputAction.CallbackContext ctx)
    {
        overlayLibro.SetActive(!overlayLibro.activeSelf);
    }

    private void Start()
    {
        //if (Input.GetButtonDown(GamepadButton.B))
        {
            overlayLibro.SetActive(!overlayLibro.activeSelf);
        }
    }

}