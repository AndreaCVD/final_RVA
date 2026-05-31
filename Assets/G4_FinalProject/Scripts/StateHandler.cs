using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public Material cookedMaterial;
    public Material burnedMaterial;

    private Renderer rend;
    private float timeInOven = 0f;
    private bool isCooking = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isCooking)
        {
            timeInOven += Time.deltaTime;

            if (timeInOven >= 5f)
                rend.material = burnedMaterial;
            else if (timeInOven >= 3f)
                rend.material = cookedMaterial;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oven"))
        {
            isCooking = true;
            Debug.Log("Ingrediente dentro del horno");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Oven"))
        {
            isCooking = false;
            Debug.Log("Ingrediente fuera del horno");
        }
    }
}