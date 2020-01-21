using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Flow {
    #region Singleton
    static private UIManager instance = null;

    static public UIManager Instance
    {
        get
        {
            return instance ?? (instance = new UIManager());
        }
    }

    #endregion

    private TextUI score;
    private TextUI cash;
    private TextUI headshot;
    private TextUI playerKill;
    private TextUI towerKill;
    private TextUI totalKill;

    override public void PreInitialize()
    {
        UIVariables.uiLink = GameObject.FindObjectOfType<UIVariables>();
    }

    override public void Initialize()
    {
        InitScoreboard();
        HideUI();
    }

    override public void Refresh()
    {

    }

    override public void PhysicsRefresh()
    {

    }

    override public void EndFlow()
    {

    }

    private void InitScoreboard()
    {
        this.score = new TextUI(UIVariables.uiLink.scoreUI, "Score : ", PlayerStats.Score);
        this.cash = new TextUI(UIVariables.uiLink.cashUI, "Cash : ", PlayerStats.Currency);
        this.headshot = new TextUI(UIVariables.uiLink.headshotUI, "Headshot : ", PlayerStats.Headshot);
        this.playerKill = new TextUI(UIVariables.uiLink.playerKillsUI, "Player Kill : ", PlayerStats.PlayerKill);
        this.towerKill = new TextUI(UIVariables.uiLink.towerKillsUI, "Tower Kill : ", PlayerStats.TowerKill);
        this.totalKill = new TextUI(UIVariables.uiLink.totalKillsUI, "Total Kill : ", PlayerStats.TotalKill);
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

    public void ShowVictory()
    {
        UIVariables.uiLink.canvas.SetActive(true);
        UIVariables.uiLink.loseUI.SetActive(false);
        UIVariables.uiLink.winUI.SetActive(true);
        UIVariables.uiLink.fireworks.SetActive(true);
        UIVariables.uiLink.explosion.SetActive(false);
    }

    public void ShowDefeat()
    {
        UIVariables.uiLink.canvas.SetActive(true);
        UIVariables.uiLink.winUI.SetActive(false);
        UIVariables.uiLink.loseUI.SetActive(true);
        UIVariables.uiLink.explosion.SetActive(true);
        UIVariables.uiLink.fireworks.SetActive(false);
    }

    public void HideUI()
    {
        UIVariables.uiLink.canvas.SetActive(false);
        UIVariables.uiLink.winUI.SetActive(false);
        UIVariables.uiLink.loseUI.SetActive(false);
        UIVariables.uiLink.fireworks.SetActive(false);
        UIVariables.uiLink.explosion.SetActive(false);
    }
}
