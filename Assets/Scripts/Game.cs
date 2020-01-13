using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : Flow
{

    #region Singleton
    static private Game instance = null;

    static public Game Instance
    {
        get {
            return instance ?? (instance = new Game());
        }
    }
    #endregion 

    //Managers
    TowerManager towerManager;
    PlayerManager playerManager;
    WaveManager waveManager;
    EnemyManager enemyManager;
    TrapManager trapManager;
    ProjectileManager projectileManager;
    LogicManager logicManager;

    //Game Setup has a reference to everything in the scene.
    public GameObject gameSetup;

    override public void PreInitialize()
    {
        //Grab instances
        playerManager = PlayerManager.Instance;
        waveManager = WaveManager.Instance;
        enemyManager = EnemyManager.Instance;
        trapManager = TrapManager.Instance;
        projectileManager = ProjectileManager.Instance;
        logicManager = LogicManager.Instance;

        //Setup Variables
        gameSetup = GameObject.Instantiate(Main.Instance.GameSetupPrefab);
        
        //First Initialize
        playerManager.PreInitialize();
        waveManager.PreInitialize();
        enemyManager.PreInitialize();
        trapManager.PreInitialize();
        projectileManager.PreInitialize();
        logicManager.PreInitialize();
    }

    override public void Initialize()
    {
        playerManager.Initialize();
        waveManager.Initialize();
        enemyManager.Initialize();
        trapManager.Initialize();
        projectileManager.Initialize();
        logicManager.Initialize();
    }

    override public void Refresh()
    {
        playerManager.Refresh();
        waveManager.Refresh();
        enemyManager.Refresh();
        trapManager.Refresh();
        projectileManager.Refresh();
        logicManager.Refresh();
    }

    override public void PhysicsRefresh()
    {
        playerManager.PhysicsRefresh();
        waveManager.PhysicsRefresh();
        enemyManager.PhysicsRefresh();
        trapManager.PhysicsRefresh();
        projectileManager.PhysicsRefresh();
        logicManager.PhysicsRefresh();
    }

    override public void EndFlow()
    {
        playerManager.EndFlow();
        waveManager.EndFlow();
        enemyManager.EndFlow();
        trapManager.EndFlow();
        projectileManager.EndFlow();
        logicManager.EndFlow();
        //TODO Save variables in main variables holder.
        GameObject.Destroy(gameSetup);
    }
}
