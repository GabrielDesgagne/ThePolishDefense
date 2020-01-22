using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Flow {

    #region Singleton
    static private WaveManager instance = null;

    static public WaveManager Instance
    {
        get
        {
            return instance ?? (instance = new WaveManager());
        }
    }

    #endregion

    private Wave[] waves;
    private int currentWave;

    private Countdown waveCountdownTimer;
    private float timeBetweenWaves;

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {
        waveCountdownTimer = MapVariables.instance.timerUI;
        waveCountdownTimer.Initialize();
        LevelSystem levelSystem = MapVariables.instance.levelSystem;
        waves = levelSystem.levels[(int)levelSystem.currentLevel].waves;
        timeBetweenWaves = levelSystem.levels[(int)levelSystem.currentLevel].timeBetweenWaves;
    }

    override public void Refresh()
    {
        if (EnemyManager.Instance.enemies.Count <= 0)
        {
            if (currentWave == waves.Length)
            {
                LogicManager.Instance.LevelWon();
            }
            else
            {
                if (waveCountdownTimer.countdown <= 0f)
                {
                    SpawnWave();
                    waveCountdownTimer.countdown = timeBetweenWaves;
                    return;
                }
                waveCountdownTimer.Deduct();
            }
        }
    }

    override public void PhysicsRefresh()
    {

    }

    override public void EndFlow()
    {

    }

    private void SpawnWave()
    {
        Wave wave = waves[currentWave];

        for (int i = 0; i < wave.types.Length; i++)
        {
            for (int j = 0; j < wave.types[i].number; j++)
            {
                GameObject newEnemy = EnemyManager.Instance.SpawnEnemy(EnemyManager.Instance.enemyPrefabDict[wave.types[i].type]);
                Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
                e.Initialize();
                EnemyManager.Instance.toAdd.Push(e);
            }
        }
        currentWave++;
    }
}
