using UnityEngine;

public class ParticlesOnColl : MonoBehaviour
{
    public Material TomatoSauce;
    //public Material WhiteSauce;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Hay colision con: {other?.name}");
        if (other.CompareTag("SalsaTomatoe"))
        {
            gameObject.GetComponent<MeshRenderer>().material = TomatoSauce;
        }
    }
}
