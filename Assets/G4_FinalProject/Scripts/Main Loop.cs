using System.Collections;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    bool dialog_started;
    public bool dialog_finished;
    public bool day_started;
    float time = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialog_started = false;
        dialog_finished = false;
        day_started = false;
    }

    // Update is called once per frame
    void Update()
    {
        //empieza el juego
        if (dialog_started)
        {
            //Despues de X tiempo suena el telefono
            TelefonDialog();
        }
        else if (dialog_finished)
        {
            //esperemos a que el dialogo acabe
        }
        else //El dialogo ha acabado
        {
            //Aparecen los clientes
            day_started = true;
        }
    }
    private void TelefonDialog()
    {
        dialog_started = true;
        StartCoroutine(Wait_and_Continue());
        //Podemos hacer que dependiendo del enemigo, de sus estats, estas varien
        //Cualquier opcion tenemos que hablar con commandManager
        Debug.Log("--ORDEN--");
    }
    IEnumerator Wait_and_Continue()
    {
        yield return new WaitForSeconds(time);
        // Código que se ejecuta después del retraso
        Debug.Log("Han pasado 3 segundos.");
        dialog_finished = true;
    }
}
