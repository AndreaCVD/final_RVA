using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Canvas")]
    public CanvasGroup grup;
    public Image barra_contador;
    public TMP_Text timerText;

    [Header("Variables")]
    public float cuenta_atras;
    public float MAX_cuenta;
    public bool time_finished;
    public bool day_started;

    [Header("Al acabar el timer")]
    public Animator animator;
    public string animationTriggerName = "TimeOut";
    public UnityEvent onTimeFinished;

    [SerializeField] private GameObject ticketPanel;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text customersText;
    public ScoreManager scoreManager;
    public NPCController npcController;

    private void Start()
    {
        day_started = false;
        time_finished = false;

        //Cuando no haya tiempo asignado
        if (timerText != null)
        {
            Debug.Log("AAAAAAAAAAAAA");
            timerText.text = "Day not started";
            timerText.color = Color.white;
        }
    }
    void Update()
    {
        if (!day_started && !time_finished) return;


        cuenta_atras -= Time.deltaTime;
        cuenta_atras = Mathf.Max(cuenta_atras, 0f); //Nunca bajara de 0

        barra_contador.fillAmount = 1f - cuenta_atras / MAX_cuenta;
        UpdateTimerText();

        if(cuenta_atras <= 0f)
        {
            time_finished = true;
            HandleTimeOut();
        }

    }

    private void UpdateTimerText()
    {
        if (timerText == null) return;

        int segundos = Mathf.CeilToInt(cuenta_atras); // redondea hacia arriba
        int min = segundos / 60;
        int sec = segundos % 60;

        // Si el tiempo es menor de 60s muestra solo segundos: "28"
        // Si es mayor muestra "1:05"
        if (MAX_cuenta < 60f) 
        {
            timerText.text = sec.ToString("00");
        }
        else 
        { 
            timerText.text = $"{min}:{sec:00}";

        // Cambia color a rojo cuando queda menos del 25%
        timerText.color = (cuenta_atras / MAX_cuenta < 0.25f) ? Color.red : Color.white;
        }
    }

    private void HandleTimeOut()
    {
        if (timerText != null)
        {
            timerText.text = "00";
            timerText.color = Color.red;
        }

        // Lanza la animaci�n si hay un Animator asignado
        if (animator != null)
            animator.SetTrigger(animationTriggerName);

        // Llama a la funci�n externa (instanciar modelo, etc.)
        onTimeFinished?.Invoke();

        //UI Ticket puntuació
        ShowTicket();
    }


    private void ShowTicket()
    {
        // Obrir UI
        ticketPanel.SetActive(true);

        // Omplir textos
        scoreText.text = scoreManager.total_points.ToString();

        moneyText.text = scoreManager.total_points*0.87 + "€";

        customersText.text = npcController.customersCounter.ToString();

        // Opcional: pausar joc
        Time.timeScale = 0f;
    }

    public void SetTime(float seconds)
    {
        MAX_cuenta = seconds;
        cuenta_atras = seconds;
        time_finished = false;
        day_started = true; //comienza al recibir el tiempo
        barra_contador.fillAmount = 0f;

        UpdateTimerText();
    }

    /*public void Countdown()
    {
        if (cuenta_atras > 0)
        {
            cuenta_atras = cuenta_atras - 0.1f;
            barra_contador.fillAmount = cuenta_atras / MAX_cuenta;
        }
        else if (cuenta_atras <= 0 && !time_finished)
        {
            time_finished = true;
        }

    }
    */

    public void StartDay()
    {
        day_started = true;
    }
    public bool IsTimeOver()
    {
        return time_finished;
    }
    public void opacidad(float nueva_opacidad)
    {
        grup.alpha = Mathf.Lerp(grup.alpha, nueva_opacidad, Time.deltaTime * 5f);

        //grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}
