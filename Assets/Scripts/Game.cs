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
    PlayerManager playerManager;
    GridManager gridManager;
    InputManager inputManager;
    //TODO Managers :
    PodManager podManager;
    TowerManager towerManager;
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
        inputManager = InputManager.Instance;
        podManager = PodManager.Instance;
        playerManager = PlayerManager.Instance;
        waveManager = WaveManager.Instance;
        enemyManager = EnemyManager.Instance;
        trapManager = TrapManager.Instance;
        projectileManager = ProjectileManager.Instance;
        logicManager = LogicManager.Instance;
        gridManager = GridManager.Instance;


        //Setup Variables
        gameSetup = GameObject.Instantiate(Main.Instance.GameSetupPrefab);

        //First Initialize
        inputManager.PreInitialize();
        playerManager.PreInitialize();
        waveManager.PreInitialize();
        enemyManager.PreInitialize();
        trapManager.PreInitialize();
        projectileManager.PreInitialize();
        logicManager.PreInitialize();
    }

    override public void Initialize()
    {
        inputManager.Initialize();
        playerManager.Initialize();
        waveManager.Initialize();
        enemyManager.Initialize();
        trapManager.Initialize();
        projectileManager.Initialize();
        logicManager.Initialize();

        gridManager.Initialize();

    }

    override public void Refresh()
    {
        inputManager.Refresh();
        playerManager.Refresh();
        //playerManager.Refresh();
        podManager.Refresh();
        waveManager.Refresh();
        enemyManager.Refresh();
        trapManager.Refresh();
        projectileManager.Refresh();
        logicManager.Refresh();

        gridManager.Refresh();
    }

    override public void PhysicsRefresh()
    {
        inputManager.PhysicsRefresh();
        playerManager.PhysicsRefresh();
        waveManager.PhysicsRefresh();
        enemyManager.PhysicsRefresh();
        trapManager.PhysicsRefresh();
        projectileManager.PhysicsRefresh();
        logicManager.PhysicsRefresh();

        gridManager.PhysicsRefresh();
    }

    override public void EndFlow()
    {
        inputManager.EndFlow();
        playerManager.EndFlow();
        waveManager.EndFlow();
        enemyManager.EndFlow();
        trapManager.EndFlow();
        projectileManager.EndFlow();
        logicManager.EndFlow();        

        gridManager.EndFlow();

        GameObject.Destroy(gameSetup);
    }
}
