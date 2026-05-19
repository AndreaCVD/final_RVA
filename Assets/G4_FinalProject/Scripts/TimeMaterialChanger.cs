using System.Collections.Generic;
using UnityEngine;

public class TimeMaterialChanger : MonoBehaviour
{
    public Material material5Segundos;
    public Material material10Segundos;
    public float tiempoMaterial1 = 5f;
    public float tiempoMaterial2 = 10f;

    private Dictionary<GameObject, float> tiemposDeEntrada = new Dictionary<GameObject, float>();

    void OnTriggerEnter(Collider other)
    {
        GameObject root = GetRootObject(other.gameObject);

        if (!tiemposDeEntrada.ContainsKey(root))
        {
            tiemposDeEntrada[root] = Time.time;
            Debug.Log($"{root.name} entró");
        }
    }

    void OnTriggerStay(Collider other)
    {
        GameObject root = GetRootObject(other.gameObject);

        if (tiemposDeEntrada.ContainsKey(root))
        {
            float tiempo = Time.time - tiemposDeEntrada[root];
            Renderer renderer = GetRenderer(root);

            if (renderer != null)
            {
                if (tiempo >= tiempoMaterial1 && tiempo < tiempoMaterial2)
                    renderer.material = material5Segundos;
                else if (tiempo >= tiempoMaterial2)
                    renderer.material = material10Segundos;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject root = GetRootObject(other.gameObject);

        if (tiemposDeEntrada.ContainsKey(root))
        {
            tiemposDeEntrada.Remove(root);
            Debug.Log($"{root.name} salió");
        }
    }

    GameObject GetRootObject(GameObject obj)
    {
        // Buscar la raíz (el objeto más arriba sin padre o con tag especial)
        Transform current = obj.transform;
        while (current.parent != null && current.parent != transform)
        {
            current = current.parent;
        }
        return current.gameObject;
    }

    Renderer GetRenderer(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) renderer = obj.GetComponentInChildren<Renderer>();
        return renderer;
    }
}