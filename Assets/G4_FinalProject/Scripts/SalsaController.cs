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
        //entra a la variable de las particulas Start Size a 0.60. 
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