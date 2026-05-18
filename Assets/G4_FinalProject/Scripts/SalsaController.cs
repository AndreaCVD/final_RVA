using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SalsaController : MonoBehaviour
{
    public ParticleSystem salsaParticles;
    private OnTilt tiltScript;

    void Start()
    {
        tiltScript = GetComponent<OnTilt>();

        tiltScript.onBegin.AddListener(ActivarSalsa);
        tiltScript.onEnd.AddListener(DesactivarSalsa);

        salsaParticles.Stop();
    }

    void ActivarSalsa()
    {
        salsaParticles.Play();
    }

    void DesactivarSalsa()
    {
        salsaParticles.Stop();
    }
}