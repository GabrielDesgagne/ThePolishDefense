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

    private LevelSystem levelSystem;
    private Wave[] waves;
    private int currentWave;

    private Countdown waveCountdownTimer;
    private float timeBetweenWaves;

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {
        waveCountdownTimer=GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/TimerUI")).GetComponent<Countdown>();
        //waveCountdownTimer = new Countdown();
        waveCountdownTimer.Initialize();
        levelSystem = MapVariables.instance.levelSystem;
        waves = levelSystem.levels[PlayerStats.CurrentLevel].waves;
        timeBetweenWaves = levelSystem.levels[PlayerStats.CurrentLevel].timeBetweenWaves;
    }

    override public void Refresh()
    {
        if (LogicManager.Instance.IsGameOver) { return; }
        if (EnemyManager.Instance.enemies.Count <= 0)
        {
            if (currentWave == waves.Length)
            {
                currentWave = 0;
                if (PlayerStats.CurrentLevel < levelSystem.levels.Length - 1)
                {
                    PlayerStats.nextLevel();
                    LogicManager.Instance.LevelWon();
                    TimeManager.Instance.AddTimedAction(new TimedAction(() =>
                    {
                        Debug.Log("New Level Begin!");
                        UIManager.Instance.HideUI();
                        NextLevelTest();
                    }, 5));
                }
                else
                {
                    LogicManager.Instance.IsGameOver = true;
                    Debug.Log("Game Over!");
                }
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
        waves = levelSystem.levels[PlayerStats.CurrentLevel].waves;
        timeBetweenWaves = levelSystem.levels[PlayerStats.CurrentLevel].timeBetweenWaves;
    }

    private void SpawnWave()
    {
        Wave wave = waves[currentWave];
        GameObject newEnemy;
        float time = 0;
        for (int i = 0; i < wave.types.Length; i++)
        {
            for (int j = 0; j < wave.types[i].number; j++)
            {
                newEnemy = EnemyManager.Instance.enemyPrefabDict[wave.types[i].type];
                SpawnEnemyAfterTime(newEnemy, time);
                time += 0.35f;
            }
        }
        currentWave++;
    }

    private void SpawnEnemyAfterTime(GameObject enemyPrefab, float time)
    {
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            GameObject enemyObj = EnemyManager.Instance.SpawnEnemy(enemyPrefab);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.Initialize();
            EnemyManager.Instance.toAdd.Push(enemy);
        }, time));
    }

    private void NextLevelTest()
    {
        waves = levelSystem.levels[PlayerStats.CurrentLevel].waves;
        timeBetweenWaves = levelSystem.levels[PlayerStats.CurrentLevel].timeBetweenWaves;
    }
}
