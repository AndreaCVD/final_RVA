using System.Threading;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Canvas")]
    public CanvasGroup grup;
    public Image barra_contador;

    [Header("Variables")]
    public float cuenta_atras = 100f;
    public float MAX_cuenta = 100f;
    public bool time_finished;
    public bool day_started;
    private void Start()
    {
        barra_contador.fillAmount = cuenta_atras / MAX_cuenta;

        day_started = false;
        time_finished = false;
    }
    void FixedUpdate()
    {
        if (day_started && !time_finished)
        {
            Invoke(nameof(Countdown), 1f); //Every second is called
        }

    }
    public void Countdown()
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
        grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}
