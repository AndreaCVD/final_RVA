using UnityEngine;

public class ParticlesOnColl : MonoBehaviour
{
    public GameObject MeshPizza;
    public Material TomatoSauce;
    public Material WhiteSauce;
    public Material Pimineta;

    private bool Tomatoe = false;
    private bool White = false;
    private bool Pepper = false;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Hay colision con: {other?.name}");
        if (other.CompareTag("SalsaTomatoe"))
        {
            if(Tomatoe == true)
            {
                return;
            }
            else
            {
                MeshPizza.GetComponent<MeshRenderer>().material = TomatoSauce;
                Tomatoe = true;
            }
                
        }
        else if (other.CompareTag("WitheSauce"))
        {
            if (White == true)
            {
                return;
            }
            else
            {
                MeshPizza.GetComponent<MeshRenderer>().material = WhiteSauce;
                White = true;
            }
                
        }
        else if (other.CompareTag("Pepper"))
        {
            if (Pepper == true)
            {
                return;
            }
            else
            {
                MeshPizza.GetComponent<MeshRenderer>().material = Pimineta;
                Pepper = true;
            }
        }
    }
}
