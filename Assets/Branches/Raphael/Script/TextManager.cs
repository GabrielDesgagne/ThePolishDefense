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
       // Initialize();
    }

    private void Update()
    {
        //Refresh();
    }


    public void Initialize()
    {
        
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
