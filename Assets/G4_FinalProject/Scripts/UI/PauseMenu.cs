using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public InputActionReference pauseAction;

    private bool paused;

    private void OnEnable()
    {
        pauseAction.action.performed += TogglePause;

        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= TogglePause;

        pauseAction.action.Disable();
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (paused)
        {
            ContinueGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        paused = true;
    }

    public void ContinueGame()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        paused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}