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
    public int currentWave;

    private Countdown waveCountdownTimer;
    private float timeBetweenWaves;

    public int EnemyInWaveLeft { get; set; }
    private bool isLevelOver;

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {
        waveCountdownTimer = new Countdown();
        waveCountdownTimer.Initialize();
        levelSystem = MapVariables.instance.levelSystem;
        waves = levelSystem.levels[PlayerStats.CurrentLevel].waves;
        timeBetweenWaves = levelSystem.levels[PlayerStats.CurrentLevel].timeBetweenWaves;
        currentWave = 0;
        for (int i = 0; i < waves[currentWave].types.Length; i++)
            EnemyInWaveLeft += waves[currentWave].types[i].number;
        SpawnWave();
        currentWave++;
        isLevelOver = false;
    }

    override public void Refresh()
    {
        if (LogicManager.Instance.IsGameOver) { return; }
        if (EnemyInWaveLeft <= 0 && !isLevelOver)
        {
            if (currentWave > waves.Length)
            {
                currentWave = 0;
                if (PlayerStats.CurrentLevel < levelSystem.levels.Length - 1)
                {
                    PlayerStats.nextLevel();
                    LogicManager.Instance.LevelWon();
                    isLevelOver = true;
                    return;
                }
                else
                {
                    LogicManager.Instance.IsGameOver = true;
                    Debug.Log("Game Over");
                }
            }
            else
            {
                if (currentWave < waves.Length)
                {
                    for (int i = 0; i < waves[currentWave].types.Length; i++)
                        EnemyInWaveLeft += waves[currentWave].types[i].number;
                    SpawnWave();
                }
                currentWave++;
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
}
