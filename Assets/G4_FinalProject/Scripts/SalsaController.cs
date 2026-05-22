using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SalsaController : MonoBehaviour
{
    public ParticleSystem tomatoeSauce;
    private OnTilt tiltScript;

    void Start()
    {
        tiltScript = GetComponent<OnTilt>();

        if (tiltScript != null)
        {
            tiltScript.onBegin.AddListener(ActivarSalsa);
            tiltScript.onEnd.AddListener(DesactivarSalsa);
        }

        if (tomatoeSauce != null)
        {
            var collision = tomatoeSauce.collision;
            collision.enabled = true;
            collision.sendCollisionMessages = true;
            tomatoeSauce.Stop();
        }
    }

    void ActivarSalsa()
    {
        if (tomatoeSauce != null && !tomatoeSauce.isPlaying)
        {
            tomatoeSauce.Play();
        }
    }

    void DesactivarSalsa()
    {
        if (tomatoeSauce != null && tomatoeSauce.isPlaying)
        {
            tomatoeSauce.Stop();
        }
    }
}