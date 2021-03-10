using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Misc : MonoBehaviour
{
    public static bool _isPaused = false;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
        GameManager.playerHP = 3;
        GameManager.score = 0;
        Time.timeScale = 1;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        _isPaused = false;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
