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

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {

    }

    override public void Refresh()
    {
        if (PlayerStats.IsPlayerDead)
        {
            LevelLost();
            Debug.Log("YOU LOST!!!");
        }
    }

    override public void PhysicsRefresh()
    {

    }

    override public void EndFlow()
    {

    }

    public void LevelWon()
    {
        UIManager.Instance.ShowVictory();
        Debug.Log("Won Level!!!");
    }

    public void LevelLost()
    {
        UIManager.Instance.ShowDefeat();
    }

    public void GameFinish()
    {

    }
}
