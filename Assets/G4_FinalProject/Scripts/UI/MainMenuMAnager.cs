using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Intento_Implementacio_01");
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("EXIT GAME");
    }
}