using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrack : MonoBehaviour
{
    public AudioClip clip;
    AudioSource source;

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.Stop();
        }
    }
}
