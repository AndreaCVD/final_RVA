using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoop : MonoBehaviour
{
    [SerializeField] private GameObject cubeToHide;

    [SerializeField] AudioManager audio_manager;
    [SerializeField] Timer _time;

    public bool telephone_grabed;
    public bool dialog_started;
    public bool dialog_finished;
    public bool day_started;
    public bool blindsOpened = false;

    bool levelChosen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //audio_manager = this.GetComponent<AudioManager>();
        levelChosen = false;
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

            cubeToHide.SetActive(false);

            //time.StartDay();

        }

        //-------------------------------------------------

        if (_time.IsTimeOver() == false)
        {
            //El juego continua
            if (!levelChosen && dialog_finished && blindsOpened)
            {
                StartDay();
            }

        }
        else
        {
            //El tiempo ha acabado
            Debug.Log("Time is Over");


        }

    }

    public void OnBlindsOpened()
    {
        blindsOpened = true;
    }
    public void StartDay()
    {
        //Vemos en que escena estamos
        Scene active_scene = SceneManager.GetActiveScene();
        switch (active_scene.name)
        {
            case "Intento_Implementacio_01":
                LevelController levelController = Object.FindFirstObjectByType<LevelController>();
                if (levelController != null)
                    {
                        levelController.SetupLevel(
                            timePerNPC: 30f,
                            totalNPCs: 5,
                            recipes: levelController.availableRecipes
                        );
                    }
                    else
                    {
                        Debug.LogWarning("LevelController no trobat a l'escena!");
                    }
                    levelChosen = true;
                break;

        }
    }

    private void TelefonDialog()
    {
        dialog_started = true;
        float duration;

        Scene active_scene = SceneManager.GetActiveScene();
        switch (active_scene.name)
        {
            case "Aina": // aina tiene que ser la escena, nivel uno
                Debug.Log("Estamos en escena: " + active_scene.name);
                audio_manager.StartDialog(0);
                duration = audio_manager.ReturnDuration(0);
                //Debug.Log("Duracion = " + duration);

                break;
            case "Intento_Implementacio_01": // andrea tiene que ser la escena, nivel dos
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
    public void _Telephone()
    {
        telephone_grabed = true;
        Debug.Log("Se ha cojido el telefono");

    }
}
