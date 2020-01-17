using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextUI : MonoBehaviour
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
    public int totalKills;

    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
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

    }
    public void Refresh()
    {
        ReadStats();
    }

    public int Add(int number)
    {
        return number;
    }

    public void clearStats()
    {
        score = 0;
        cash = 0;
        headshot = 0;
        playerKills = 0;
        towerKills = 0;
        totalKills = 0;

    }

    public void clearKills()
    {
        headshot = 0;
        playerKills = 0;
        towerKills = 0;
    }
   

    public void ReadStats()
    {
        totalKills = playerKills + towerKills;
        scoreUI.text = "Score : " + Convert.ToString(score);
        cashUI.text = "Cash : " + Convert.ToString(cash);
        headshotUI.text = "Head shot : " + Convert.ToString(headshot);
        playerKillsUI.text = "Player kills : " + Convert.ToString(playerKills);
        towerKillsUI.text = "Tower kills : " + Convert.ToString(towerKills);
        totalKillsUI.text = "Total kills : " + Convert.ToString(totalKills);
    }

    


}
