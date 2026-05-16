using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoop : MonoBehaviour
{
    AudioManager audio_manager;


    public bool dialog_started;
    public bool dialog_finished;
    public bool day_started;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        audio_manager = this.GetComponent<AudioManager>();

        dialog_started = false;
        dialog_finished = false;
        day_started = false;


    }

    // Update is called once per frame
    private void Update()
    {


        if (!dialog_started && !day_started)
        {        
            day_started=true;
            Wait_and_Start(5f);        
        }
        else if (!dialog_started)//empieza el juego y esperamos 5 seundos para que suene el telefono
        {
            dialog_started = true;
            TelefonDialog();
        }
        
        if (!dialog_finished)
        {
            //esperemos a que el dialogo acabe
        }
        else if (!day_started & dialog_finished) //Dialog is over
        {
            day_started = true;
        }
    }
    private void TelefonDialog()
    {
        Debug.Log("EMPIEZA DIALOGO");

        dialog_started = true;
        float duration;

        Scene active_scene = SceneManager.GetActiveScene();
        switch (active_scene.name)
        {
            case "Aina":
                Debug.Log("Estamos en escena: " + active_scene.name);
                audio_manager.StartDialog(0);
                duration = audio_manager.ReturnDuration(0);
                Debug.Log("Duracion = " + duration);

                break;
            default:
                Debug.Log("No se ha leido bien");
                duration = 1.2f;
                break;
        }

        StartCoroutine(Dialog_Finished(duration));

    }
    IEnumerator Wait_and_Continue(float time)
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator Wait_and_Start(float time)
    {
        yield return new WaitForSeconds(time);
        // Código que se ejecuta después del retraso
        dialog_started = true;
    }
    IEnumerator Dialog_Finished(float time)
    {
        yield return new WaitForSeconds(time/2);
        dialog_finished = true;
    }
}
