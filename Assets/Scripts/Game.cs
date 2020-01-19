using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : Flow {

    #region Singleton
    static private Game instance = null;

    static public Game Instance {
        get {
            return instance ?? (instance = new Game());
        }
    }
    #endregion

    //Managers
    PlayerManager playerManager;
    InputManager inputManager;
    //TODO Managers :
    PodManager podManager;
    TowerManager towerManager;
    WaveManager waveManager;
    EnemyManager enemyManager;
    TrapManager trapManager;
    ProjectileManager projectileManager;
    LogicManager logicManager;
    TimeManager timeManager;


    //Game Setup has a reference to everything in the scene.
    public GameObject gameSetup;

    //Grid vars
    private Grid hiddenGrid;
    private GridEntity mapGrid;
    private GameObject hiddenGridPrefab;
    private GameObject tileSidesPrefab;



    override public void PreInitialize() {
        //Grab instances
        inputManager = InputManager.Instance;
        podManager = PodManager.Instance;
        playerManager = PlayerManager.Instance;
        waveManager = WaveManager.Instance;
        enemyManager = EnemyManager.Instance;
        trapManager = TrapManager.Instance;
        projectileManager = ProjectileManager.Instance;
        logicManager = LogicManager.Instance;
        towerManager = TowerManager.Instance;
        timeManager = TimeManager.Instance;

        //Setup Variables

        //Instantiates
        gameSetup = GameObject.Instantiate(Main.Instance.GameSetupPrefab);

        //First Initialize
        inputManager.PreInitialize();
        playerManager.PreInitialize();
        waveManager.PreInitialize();
        //enemyManager.PreInitialize();
        trapManager.PreInitialize();
        projectileManager.PreInitialize();
        logicManager.PreInitialize();
        towerManager.PreInitialize();
        timeManager.PreInitialize();


        LoadResources();
    }

    override public void Initialize() {
        inputManager.Initialize();
        playerManager.Initialize();
        waveManager.Initialize();
        //enemyManager.Initialize();
        trapManager.Initialize();
        projectileManager.Initialize();
        logicManager.Initialize();
        towerManager.Initialize();
        timeManager.Initialize();

        InitMap();
    }

    override public void Refresh() {
        inputManager.Refresh();
        playerManager.Refresh();
        //playerManager.Refresh();
        podManager.Refresh();
        waveManager.Refresh();
        //enemyManager.Refresh();
        trapManager.Refresh();
        projectileManager.Refresh();
        logicManager.Refresh();
        towerManager.Refresh();
        timeManager.Refresh();

    }

    override public void PhysicsRefresh() {
        inputManager.PhysicsRefresh();
        playerManager.PhysicsRefresh();
        waveManager.PhysicsRefresh();
        //enemyManager.PhysicsRefresh();
        trapManager.PhysicsRefresh();
        projectileManager.PhysicsRefresh();
        logicManager.PhysicsRefresh();
        towerManager.PhysicsRefresh();
        timeManager.PhysicsRefresh();

    }

    override public void EndFlow() {
        inputManager.EndFlow();
        playerManager.EndFlow();
        waveManager.EndFlow();
        //enemyManager.EndFlow();
        trapManager.EndFlow();
        projectileManager.EndFlow();
        logicManager.EndFlow();
        towerManager.EndFlow();
        timeManager.EndFlow();


        GameObject.Destroy(gameSetup);
    }

    private void LoadResources() {
        this.hiddenGridPrefab = Resources.Load<GameObject>("Prefabs/Grid/HiddenGrid");
        this.tileSidesPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_VisualSides");
    }

    private void InitMap() {

        //Init grids holder
        GameVariables.instance.gridsHolder = new GameObject("GridsStuff");

        //Init hidden grid
        this.hiddenGrid = GameObject.Instantiate<GameObject>(this.hiddenGridPrefab, GameVariables.instance.gridsHolder.transform).GetComponent<Grid>();


        this.mapGrid = new GridEntity("MapMap", this.hiddenGrid, GameVariables.instance.mapStartPointInMap, GameVariables.instance.mapRows, GameVariables.instance.mapColumns, this.tileSidesPrefab);

        MapInfoPck.Instance.TestPopulate();
        SpawnItemsOnGrid();
    }

    private void SpawnItemsOnGrid() {
        //Get info package
        Dictionary<Vector2, TowerType> towersInfo = MapInfoPck.Instance.TileTowerInfos;
        Dictionary<Vector2, TrapType> trapsInfo = MapInfoPck.Instance.TileTrapInfos;


        //Towers
        foreach (KeyValuePair<Vector2, TowerType> info in towersInfo) {
            //Get tile Coords
            Vector2 tileCoords = this.mapGrid.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.mapGrid.GetTileCenterFixed(tileCoords);

            //Create obj
            Tower tower = towerManager.CreateTower(info.Value, tileCenter);
        }

        //Traps
        foreach (KeyValuePair<Vector2, TrapType> info in trapsInfo) {
            //Get tile Coords
            Vector2 tileCoords = this.mapGrid.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.mapGrid.GetTileCenterFixed(tileCoords);

            //Create obj
            //trapManager.CreateTrap(info.Value, tileCenter);
        }
    }
}
