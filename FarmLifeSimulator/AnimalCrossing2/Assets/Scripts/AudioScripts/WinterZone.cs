using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WinterZone : MonoBehaviour
{
    public AudioSource audioSourc;
    public AudioMixerSnapshot winterSnapshot;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            winterSnapshot.TransitionTo(0.5f);
        }
    }
}
