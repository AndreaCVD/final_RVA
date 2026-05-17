using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoop : MonoBehaviour
{
    [SerializeField] AudioManager audio_manager;

    public bool telephone_grabed;
    public bool dialog_started;
    public bool dialog_finished;
    public bool day_started;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //audio_manager = this.GetComponent<AudioManager>();
        telephone_grabed = false;
        dialog_started = false;
        dialog_finished = false;
        day_started = false;

        Invoke(nameof(RingTelephone), 3f);
    }

    // Update is called once per frame
    private void Update()
    {


        if (telephone_grabed)
        {        
            
            dialog_started = true;
            TelefonDialog();
        }

        
        if (!dialog_finished)
        {
            //esperemos a que el dialogo acabe
        }
        else if (dialog_finished) //Dialog is over
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
    void RingTelephone ()
    {
        audio_manager.RingTel();
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
    public void Telephone()
    {
        telephone_grabed = true;
        Debug.Log("Se ha cojido el telefono");

    }
}
