using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton (может быть только один)

    public static AudioManager manager;

    #endregion

    private void Awake()
    {
        manager = this;
    }

    public AudioSource mainSource;
    public AudioSource winterSource;
    public AudioSource summerSource;
}
