using UnityEngine;

public class AudioAmbient : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource ambienceSource;

    void Start()
    {
        musicSource.loop = true;
        ambienceSource.loop = true;

        musicSource.Play();
        ambienceSource.Play();
    }
}
