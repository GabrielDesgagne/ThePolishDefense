using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : Flow {
    #region Singleton
    static private LogicManager instance = null;

    static public LogicManager Instance
    {
        get
        {
            return instance ?? (instance = new LogicManager());
        }
    }

    #endregion

    public bool IsGameOver { get; set; }

    override public void PreInitialize() { }

    override public void Initialize() { }

    override public void Refresh()
    {
        if (PlayerStats.IsPlayerDead)
            LevelLost();

    }

    override public void PhysicsRefresh() { }

    override public void EndFlow()
    {
        instance = null;
    }

    public void LevelWon()
    {
        UIManager.Instance.ShowVictory();
        //Change scene
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            Main.Instance.ChangeCurrentFlow();
        }, 4f));
    }

    public void LevelLost()
    {
        UIManager.Instance.ShowDefeat();
    }

    public void GameFinish()
    {
        //------------------TODO----------------- Implement
    }
}
