using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSTicker : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float time;

    private void Update()
    {
        time += (Time.deltaTime - time) * 0.1f;
        float fps = 1.0f / time;
        text.text = "FPS: " + Mathf.Ceil(fps);
    }
}
