using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject overlayLibro;
    public GameObject overlayTicket_carbonara;
    public GameObject overlayTicket_quesos;
    public GameObject overlayTicket_jamon;
    public GameObject overlayTicket_margarita;

    public InputActionReference showOverlay_Libro;
    public InputActionReference showOverlay_Ticket_carbonara;
    public InputActionReference showOverlay_Ticket_quesos;
    public InputActionReference showOverlay_Ticket_jamon;
    public InputActionReference showOverlay_Ticket_margarita;

    [SerializeField] LevelController pizza; //para acceder a la pizza

    private bool paused;
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        showOverlay_Libro.action.performed += TogglePause_Libro;
        showOverlay_Libro.action.Enable();

        showOverlay_Ticket_carbonara.action.performed += TogglePause_Ticket;
        showOverlay_Ticket_carbonara.action.Enable();

        showOverlay_Ticket_quesos.action.performed += TogglePause_Ticket;
        showOverlay_Ticket_quesos.action.Enable();

        showOverlay_Ticket_jamon.action.performed += TogglePause_Ticket;
        showOverlay_Ticket_jamon.action.Enable();

        showOverlay_Ticket_margarita.action.performed += TogglePause_Ticket;
        showOverlay_Ticket_margarita.action.Enable();
    }
    private void OnDisable()
    {
        showOverlay_Libro.action.performed -= TogglePause_Libro;
        showOverlay_Libro.action.Disable();

        showOverlay_Ticket_carbonara.action.performed -= TogglePause_Ticket;
        showOverlay_Ticket_carbonara.action.Disable();

        showOverlay_Ticket_quesos.action.performed -= TogglePause_Ticket;
        showOverlay_Ticket_quesos.action.Disable();

        showOverlay_Ticket_jamon.action.performed -= TogglePause_Ticket;
        showOverlay_Ticket_jamon.action.Disable();

        showOverlay_Ticket_margarita.action.performed -= TogglePause_Ticket;
        showOverlay_Ticket_margarita.action.Disable();
    }

    private void TogglePause_Libro(InputAction.CallbackContext context)
    {
        if (paused)
        {
            ContinueGame_Libro();
        }
        else
        {
            PauseGame_Libro();
        }
    }
    private void TogglePause_Ticket(InputAction.CallbackContext context)
    {
        if (paused)
        {
            ContinueGame_Ticket();
        }
        else
        {
            PauseGame_Ticket();
        }
    }

    public void PauseGame_Libro()
    {
        overlayLibro.SetActive(true);

        paused = true;
    }

    public void ContinueGame_Libro()
    {
        overlayLibro.SetActive(false);

        paused = false;
    }

    public void PauseGame_Ticket()
    {
        paused = true;

        Debug.Log("Pizza actual: " + pizza.recetaActual.recipeName);

        string receta = pizza.recetaActual.recipeName;
        //switch que comprueve el tipo de pizza que se ha pedido y abre el overlay correspontiente
        switch (receta)
        {
            case "Carbonara":
                overlayTicket_carbonara.SetActive(true);
                break;

            case "Cuatro Quesos":
                overlayTicket_quesos.SetActive(true);
                break;

            case "Jamón y queso":
                overlayTicket_jamon.SetActive(true);
                break;

            case "Margarita":
                overlayTicket_margarita.SetActive(true);
                break;
        }
    }

    public void ContinueGame_Ticket()
    {
        overlayTicket_carbonara.SetActive(false);
        overlayTicket_quesos.SetActive(false);
        overlayTicket_jamon.SetActive(false);
        overlayTicket_margarita.SetActive(false);

        paused = false;
    }
}