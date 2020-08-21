using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySounds : MonoBehaviour
{
    public GameObject Light1;
    public GameObject Light2;

    public AudioClip audioClipLights;
    public AudioSource audioSource;

    public AudioMixerSnapshot mainSnapshot;
    public AudioMixerSnapshot winterSnapshot;
    public AudioMixerSnapshot summerSnapshot;

    public AudioMixerSnapshot ambienceOutSnap;
    public AudioMixerSnapshot ambienceInSnap;

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Lights"))
        {
            Light1.SetActive(true);
            Light2.SetActive(true);
            audioSource.PlayOneShot(audioClipLights);
        }

        if(other.CompareTag("Winter"))
        {
            winterSnapshot.TransitionTo(0.5f);
        }
        if(other.CompareTag("Ambience"))
        {
            ambienceInSnap.TransitionTo(0.5f);
        }
        if(other.CompareTag("Summer"))
        {
            summerSnapshot.TransitionTo(0.5f);
        }

    }
    
    

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Lights"))
        {
            Light1.SetActive(false);
            Light2.SetActive(false);
            audioSource.PlayOneShot(audioClipLights);
        }

        if(other.CompareTag("Ambience"))
        {
            ambienceOutSnap.TransitionTo(0.5f);
        }

        if(other.CompareTag("Winter"))
        {
            mainSnapshot.TransitionTo(0.5f);
        }
        if(other.CompareTag("Summer"))
        {
            mainSnapshot.TransitionTo(0.5f);
        }
    }
}
