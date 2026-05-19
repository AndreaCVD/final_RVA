using System.Collections.Generic;
using UnityEngine;

public class TimeMaterialManager : MonoBehaviour
{
    [Header("Referencias")]
    public Light pointLight;

    private Dictionary<GameObject, float> tiemposDeEntrada = new Dictionary<GameObject, float>();
    private bool hayAlgoDentro = false;

    void Start()
    {
        if (pointLight != null)
            pointLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject obj = GetRootObject(other.gameObject);

        StateHandler stateHandler = obj.GetComponent<StateHandler>();

        if (stateHandler != null && !tiemposDeEntrada.ContainsKey(obj))
        {
            tiemposDeEntrada[obj] = Time.time;
            stateHandler.EntrarEnHorno(Time.time);
            Debug.Log(obj.name + " entro al horno - ID: " + obj.GetInstanceID());
        }

        if (!hayAlgoDentro)
        {
            hayAlgoDentro = true;
            if (pointLight != null)
                pointLight.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject obj = GetRootObject(other.gameObject);

        if (tiemposDeEntrada.ContainsKey(obj))
        {
            StateHandler stateHandler = obj.GetComponent<StateHandler>();
            if (stateHandler != null)
                stateHandler.SalirDelHorno();

            tiemposDeEntrada.Remove(obj);
            Debug.Log(obj.name + " salio del horno - ID: " + obj.GetInstanceID());
        }

        if (tiemposDeEntrada.Count == 0)
        {
            hayAlgoDentro = false;
            if (pointLight != null)
                pointLight.enabled = false;
        }
    }

    GameObject GetRootObject(GameObject obj)
    {
        Transform current = obj.transform;

        while (current.parent != null)
        {
            if (current.GetComponent<StateHandler>() != null)
                return current.gameObject;

            current = current.parent;
        }

        return current.gameObject;
    }
}