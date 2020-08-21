using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "p1ayer";
    public int playerHP = 100;
    public int playerStamina = 100;
    public int playerCreativity = 0;


    
    void Update()
    {

        #region обнуляем статы к 0 если они падают ниже 0 
        if (playerHP > 100)
        {
            playerHP = 100;
        }

        if (playerHP < 0)
        {
            playerHP = 0;
        }

        if (playerCreativity < 0)
        {
            playerCreativity = 0;
        }

        if (playerStamina < 0)
        {
            playerStamina = 0;
        }

        if (playerStamina > 100)
        {
            playerStamina = 100;
        }
        #endregion 

    }

}
