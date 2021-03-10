using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int scoreNeededToWin = 20;
    public static int playerHP = 3;
    public static int enemyCount = 3;

    public GameObject endGameUI;
    public GameObject victoryUI;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Text playerHPText;
    public Text scoreText;
    public Text timerText;
    public Text endGameScoreText;
    public Text endGameTimeText;
    public Text victoryTextStars;

    private int seconds = 0;
    private bool isDead = false;

    private void Update()
    {
        seconds = (int)(Time.timeSinceLevelLoad);
        timerText.text = "Time: " + seconds;

        playerHPText.text = "Hearts left: " + playerHP;
        scoreText.text = "Score: " + score;

        if(playerHP <= 0)
        {
            isDead = true;
            if (isDead == true)
            {
                endGameScoreText.text = "Well done! Ur score is: "+score;
                endGameTimeText.text = "U did it in " + seconds + " seconds!";
                endGameUI.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (score >= scoreNeededToWin)
        {
            victoryUI.SetActive(true);
            if(playerHP == 3)
            {
                victoryTextStars.text = "Good job! You got " + playerHP + " stars!";
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                score = 0;
                Time.timeScale = 0;
            } else if(playerHP == 2)
            {
                victoryTextStars.text = "Good job! You got " + playerHP + " stars!";
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                score = 0;
                Time.timeScale = 0;
            } else if(playerHP == 1)
            {
                victoryTextStars.text = "Good job! You got " + playerHP + " star!";
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                score = 0;
                Time.timeScale = 0;
            }
        }

    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
        isDead = false;
        endGameUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(1);
    }

}
