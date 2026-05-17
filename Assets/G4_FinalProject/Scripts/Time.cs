using System.Threading;
using UnityEngine.UI;
using UnityEngine;

public class Time : MonoBehaviour
{
    [Header("Canvas")]
    public CanvasGroup grup;
    public Image barra_contador;

    [Header("Variables")]
    public float cuenta_atras = 100f;
    public float MAX_cuenta = 100f;
    public bool time_finished;

    private void Start()
    {
        time_finished = false;
    }
    void FixedUpdate()
    {
        Invoke(nameof(StartDay), 1f); //Every second is called
    }
    public void StartDay()
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
    public bool IsTimeOver()
    {
        return time_finished;
    }
    public void opacidad(float nueva_opacidad)
    {
        grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}
