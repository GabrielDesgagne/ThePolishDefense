using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Flow {
    #region Singleton
    static private UIManager instance = null;

    static public UIManager Instance {
        get {
            return instance ?? (instance = new UIManager());
        }
    }

    #endregion

    private MapVariables mapVariables;

    private TextUI score;
    private TextUI cash;
    private TextUI headshot;
    private TextUI playerKill;
    private TextUI towerKill;
    private TextUI totalKill;

    override public void PreInitialize() {
    }

    override public void Initialize() {
        UIVariables.uiLink = GameObject.FindObjectOfType<UIVariables>();

        score = UIVariables.uiLink.scoreUI.GetComponent<TextUI>();
        cash = UIVariables.uiLink.cashUI.GetComponent<TextUI>();
        headshot = UIVariables.uiLink.headshotUI.GetComponent<TextUI>();
        playerKill = UIVariables.uiLink.playerKillsUI.GetComponent<TextUI>();
        towerKill = UIVariables.uiLink.towerKillsUI.GetComponent<TextUI>();
        totalKill = UIVariables.uiLink.totalKillsUI.GetComponent<TextUI>();

        InitScoreboard();
        HideUI();
    }

    override public void Refresh() { }

    override public void PhysicsRefresh() { }

    override public void EndFlow() {
        instance = null;
    }

    private void InitScoreboard() {
        this.score.Initialize(UIVariables.uiLink.scoreUI, "Score : ", PlayerStats.Score);
        this.cash.Initialize(UIVariables.uiLink.cashUI, "Cash : ", PlayerStats.Currency);
        this.headshot.Initialize(UIVariables.uiLink.headshotUI, "Headshot : ", PlayerStats.Headshot);
        this.playerKill.Initialize(UIVariables.uiLink.playerKillsUI, "Player Kill : ", PlayerStats.PlayerKill);
        this.towerKill.Initialize(UIVariables.uiLink.towerKillsUI, "Tower Kill : ", PlayerStats.TowerKill);
        this.totalKill.Initialize(UIVariables.uiLink.totalKillsUI, "Total Kill : ", PlayerStats.TotalKill);
    }

    public void TurningOff() {
        this.score.SetActive(false);
        this.cash.SetActive(false);
        this.headshot.SetActive(false);
        this.playerKill.SetActive(false);
        this.towerKill.SetActive(false);
        this.totalKill.SetActive(false);
    }

    public void TurningOn() {
        this.score.SetActive(true);
        this.cash.SetActive(true);
        this.headshot.SetActive(true);
        this.playerKill.SetActive(true);
        this.towerKill.SetActive(true);
        this.totalKill.SetActive(true);
    }

    public void ShowVictory() {
        UIVariables.uiLink.canvas.SetActive(true);
        UIVariables.uiLink.loseUI.SetActive(false);
        UIVariables.uiLink.winUI.SetActive(true);
        UIVariables.uiLink.fireworks.SetActive(true);
        UIVariables.uiLink.explosion.SetActive(false);
    }

    public void ShowDefeat() {
        UIVariables.uiLink.canvas.SetActive(true);
        UIVariables.uiLink.winUI.SetActive(false);
        UIVariables.uiLink.loseUI.SetActive(true);
        UIVariables.uiLink.explosion.SetActive(true);
        UIVariables.uiLink.fireworks.SetActive(false);
    }

    public void HideUI() {
        UIVariables.uiLink.canvas.SetActive(false);
        UIVariables.uiLink.winUI.SetActive(false);
        UIVariables.uiLink.loseUI.SetActive(false);
        UIVariables.uiLink.fireworks.SetActive(false);
        UIVariables.uiLink.explosion.SetActive(false);
    }
}
