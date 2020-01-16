using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextUI : MonoBehaviour
{
    [SerializeField] private UIVariables variables;

    private TextMeshProUGUI textMesh;
    private string text;
    public int number { get; private set; }
    private int initialeValue = 0;

    public TextUI(string text, int initialeValue, TextMeshProUGUI textMesh)
    {
        this.text = text;
        this.number = initialeValue;
        this.initialeValue = initialeValue;
        this.textMesh = textMesh;
        this.textMesh.text = this.text + this.number;
    }


    public void AddScore(int number)
    {
        this.variables.scoreUI.text = "Score : " + Convert.ToString(number);
        this.variables.score = number;
    }
    public void AddCash(int number)
    {
        this.variables.cashUI.text = "Cash : " + Convert.ToString(number);
        this.variables.cash = number;
    }
    public void AddHeadshot(int number)
    {
        this.variables.headshotUI.text = "Headshot : " + Convert.ToString(number);
        this.variables.headshot = number;
    }
    public void AddPlayerKill(int number)
    {
        this.variables.playerKillsUI.text = "Player Kills : " + Convert.ToString(number);
        this.variables.playerKills = number;

    }
    public void AddTowerKill(int number)
    {
        this.variables.towerKillsUI.text = "Tower Kills : " + Convert.ToString(number);
        this.variables.towerKills = number;
    }

    public void AddTotalKill()
    {
        this.variables.totalKills = this.variables.playerKills + this.variables.towerKills;
        this.variables.totalKillsUI.text = "Total Kills : " + Convert.ToString(this.variables.totalKills);
    }

    public void Reset()
    {
        this.number = this.initialeValue;
        
    }

    public void SetActive(bool activate)
    {
        this.textMesh.gameObject.SetActive(activate);
    }
   
    
   
   


}
