using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void playAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
