using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
   public TextUI score;
   public TextUI cash;
   public TextUI headshot;
   public TextUI playerKill;
   public TextUI towerKill;
   public TextUI totalKill;

    private UIVariables variables;


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

        
        this.variables = GameObject.Find("Script").GetComponent<UIVariables>();
        this.score = new TextUI("Score : ", 0, variables.scoreUI);
        this.cash = new TextUI("Cash : ", 0, variables.cashUI);
        this.headshot = new TextUI("Headshot : ", 0, variables.headshotUI);
        this.playerKill = new TextUI("Player Kill : ", 0, variables.playerKillsUI);
        this.towerKill = new TextUI("Tower Kill : ", 0, variables.towerKillsUI);
        this.totalKill = new TextUI("Total Kill : ", 0, variables.totalKillsUI);

    }

   
  

    public void Refresh()
    {
       
    }

    public void TurningOff()
    {
        this.score.SetActive(false);
        this.cash.SetActive(false);
        this.headshot.SetActive(false);
        this.playerKill.SetActive(false);
        this.towerKill.SetActive(false);
        this.totalKill.SetActive(false);
    }
    public void TurningOn()
    {
        this.score.SetActive(true);
        this.cash.SetActive(true);
        this.headshot.SetActive(true);
        this.playerKill.SetActive(true);
        this.towerKill.SetActive(true);
        this.totalKill.SetActive(true);
    }

   
}
