using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Dialog Owner")]
    [SerializeField] List<AudioClip> Dialogs = new List<AudioClip>();
    [SerializeField] AudioClip Ring;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void StartDialog(int x)
    {
        audioSource.clip = Dialogs[x];
        if (audioSource.loop == true)
        {
            audioSource.loop = false;
        }
        audioSource.Play();
    }

    public float ReturnDuration(int x)
    {
        //AudioSource.clip.length -->  obtain clip lenght
        return Dialogs[0].length;
    }

    public void RingTel()
    {
        audioSource.clip = Ring;
        audioSource.loop = true;
        audioSource.Play();
    }
    /* FUNCIONES DE AUDIO
     *  1.  .Play()
     *  2.  .Stop()
     *  3.  .clip
     *  4.  .volume
     */
}
