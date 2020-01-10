using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RoundOver : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI cashUI;
    public TextMeshProUGUI headshotUI;
    public TextMeshProUGUI playerKillsUI;
    public TextMeshProUGUI towerKillsUI;
    public TextMeshProUGUI totalKillsUI;
    

    public int score;
    public int cash;
    public int headshot;
    public int playerKills;
    public int towerKills;
    private int totalKills;

    private bool win = false;
    private bool lose = false;
    public GameObject canvas;
    public GameObject youWin;
    public GameObject youLose;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Refresh();
    }

    public void Initialize()
    {
        score = 0;
        cash = 0;
        headshot = 0;
        playerKills = 0;
        towerKills = 0;
        youLose.SetActive(false);
        youWin.SetActive(false);
    }

    public void Refresh()
    {
        totalKills = playerKills + towerKills;

        scoreUI.text = "Score : " + Convert.ToString(score);
        cashUI.text = "Cash : " + Convert.ToString(cash);
        headshotUI.text = "Head shot : " + Convert.ToString(headshot);
        playerKillsUI.text = "Player kills : " + Convert.ToString(playerKills);
        towerKillsUI.text = "Tower kills : " + Convert.ToString(towerKills);
        totalKillsUI.text = "Total kills : " + Convert.ToString(totalKills);

        /*
        if (win == true && lose == false)
        {
            youWin.SetActive(true);
            youLose.SetActive(false);
        }
        else if (win == false && lose == true)
        {
            youWin.SetActive(false);
            youLose.SetActive(true);
        }
        */
    }
    public void showVictory()
    {
        canvas.SetActive(true);
        youLose.SetActive(false);
        youWin.SetActive(true);
    }
    public void showDefeat()
    {
        canvas.SetActive(true);
        youWin.SetActive(false);
        youLose.SetActive(true);
    }
    public void hideUI()
    {
        canvas.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
    }

    public void addScore()
    {
        score++;
    }

    public void subtractScore()
    {
        score--;
    }

    public void addCash()
    {
        cash++;
    }

    public void subtractCash()
    {
        cash--;
    }

    public void addHeadshot()
    {
        headshot++;
    }

    public void subtractHeadshot()
    {
        headshot--;
    }
    public void addPlayerKill()
    {
        playerKills++;
    }
    public void subtractPlayerKill()
    {
        playerKills--;
    }

    public void addTowerKill()
    {
        towerKills++;
    }

    public void subtractTowerKill()
    {
        towerKills--;
    }

    public void clearStats()
    {
        score = 0;
        cash = 0;
        headshot = 0;
        playerKills = 0;
        towerKills = 0;

    }

    public void clearKills()
    {
        headshot = 0;
        playerKills = 0;
        towerKills = 0;
    }
}
