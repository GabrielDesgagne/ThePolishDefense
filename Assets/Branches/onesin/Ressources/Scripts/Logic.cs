using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public static bool gameIsOver;

    private void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {

        if (gameIsOver)
        {
            return;
        }

        if (PlayerStats.Hp <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        //go to menu if exist
    }

    public void WinLevel()
    {
        gameIsOver = true;
        //go to next level after transition
    }
}
