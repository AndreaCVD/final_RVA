using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoop : MonoBehaviour
{
    [SerializeField] AudioManager audio_manager;
    [SerializeField] Time time;
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
        //Cuando el telefono se coja:
            // 1. El dialogo empieza
            // 2. Cambiar el audio
        if (telephone_grabed && !dialog_started)
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
            time.StartDay();
        }

        //---------------------------
        if (time.IsTimeOver() == false)
        {
            //El juego continua
        }
        else
        {
            //El tiempo ha acabado
            Debug.Log("Time is Over");

        }

    }
    private void TelefonDialog()
    {
        dialog_started = true;
        float duration;

        Scene active_scene = SceneManager.GetActiveScene();
        switch (active_scene.name)
        {
            case "Aina":
                Debug.Log("Estamos en escena: " + active_scene.name);
                audio_manager.StartDialog(0);
                duration = audio_manager.ReturnDuration(0);
                //Debug.Log("Duracion = " + duration);

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
    IEnumerator Dialog_Finished(float time)
    {
        yield return new WaitForSeconds(time);
        dialog_finished = true;
        Debug.Log("El dialogo ha terminado");
    }
    public void Telephone()
    {
        telephone_grabed = true;
        Debug.Log("Se ha cojido el telefono");

    }
}
