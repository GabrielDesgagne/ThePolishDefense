using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : Flow
{
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

    LevelSystem levelSystem;
    public static bool gameIsOver;
    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {
       /* float hp;
        int nbEnemies = 0;
        for (int k = 0; k < levelSystem.levels[(int)levelSystem.currentLevel].waves.Length; k++)
        {
            for (int i = 0; i < levelSystem.levels[(int)levelSystem.currentLevel].waves[k].types.Length; i++)
            {
                nbEnemies += levelSystem.levels[(int)levelSystem.currentLevel].waves[k].types[i].number;

            }
        }

        hp = (float)(0.25 * nbEnemies);
        PlayerStats.Hp = hp;
        gameIsOver = false;*/
    }

    override public void Refresh()
    {
        /*if (gameIsOver)
        {
            return;
        }

        if (PlayerStats.Hp <= 0)
        {
            EndGame();
        }*/
    }

    override public void PhysicsRefresh()
    {

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

    override public void EndFlow()
    {

    }
}
