using UnityEngine;

public class Oven : MonoBehaviour
{
    public GameObject fire;

    void OnTriggerEnter(Collider other)
    {
        fire.SetActive(true);   // Enciende la luz
    }

    void OnTriggerExit(Collider other)
    {
        fire.SetActive(false);  // Apaga la luz
    }
}