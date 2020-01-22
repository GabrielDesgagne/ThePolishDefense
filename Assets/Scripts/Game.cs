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
    LogicManager logicManager;
    TimeManager timeManager;

    PlayerManager playerManager;
    InputManager inputManager;

    TowerManager towerManager;
    TrapManager trapManager;

    WaveManager waveManager;
    EnemyManager enemyManager;

    PodManager podManager;
    ArrowManager arrowManager;
    ProjectileManager projectileManager;

    UIManager uiManager;

    private GameVariables gameVariables;
    private MapVariables mapVariables;

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
        arrowManager = ArrowManager.Instance;
        uiManager = UIManager.Instance;

        //First Initialize
        inputManager.PreInitialize();
        playerManager.PreInitialize();
        waveManager.PreInitialize();
        enemyManager.PreInitialize();
        trapManager.PreInitialize();
        projectileManager.PreInitialize();
        logicManager.PreInitialize();
        towerManager.PreInitialize();
        timeManager.PreInitialize();
        arrowManager.PreInitialize();
        podManager.PreInitialize();
        uiManager.PreInitialize();

        PreInitializeMap();
    }

    override public void Initialize() {
        inputManager.Initialize();
        playerManager.Initialize();
        waveManager.Initialize();
        enemyManager.Initialize();
        trapManager.Initialize();
        projectileManager.Initialize();
        logicManager.Initialize();
        towerManager.Initialize();
        timeManager.Initialize();
        arrowManager.Initialize();
        podManager.Initialize();
        uiManager.Initialize();

        //Setup Variables
        gameVariables = GameVariables.instance;
        mapVariables = MapVariables.instance;

        InitializeMap();
    }

    override public void Refresh() {
        inputManager.Refresh();
        playerManager.Refresh();
        //playerManager.Refresh();
        podManager.Refresh();
        waveManager.Refresh();
        enemyManager.Refresh();
        trapManager.Refresh();
        projectileManager.Refresh();
        logicManager.Refresh();
        towerManager.Refresh();
        timeManager.Refresh();
        arrowManager.Refresh();
        uiManager.Refresh();

    }

    override public void PhysicsRefresh() {
        inputManager.PhysicsRefresh();
        playerManager.PhysicsRefresh();
        waveManager.PhysicsRefresh();
        enemyManager.PhysicsRefresh();
        trapManager.PhysicsRefresh();
        projectileManager.PhysicsRefresh();
        logicManager.PhysicsRefresh();
        towerManager.PhysicsRefresh();
        timeManager.PhysicsRefresh();
        arrowManager.PhysicsRefresh();
        podManager.PhysicsRefresh();
        uiManager.PhysicsRefresh();
    }

    override public void EndFlow() {
        inputManager.EndFlow();
        playerManager.EndFlow();
        waveManager.EndFlow();
        enemyManager.EndFlow();
        trapManager.EndFlow();
        projectileManager.EndFlow();
        logicManager.EndFlow();
        towerManager.EndFlow();
        timeManager.EndFlow();
        uiManager.EndFlow();
    }


    private void PreInitializeMap() {
    }
    private void InitializeMap() {
        //Create grid to place items
        InitGrid();

        //Create enemy start/end point
        StartEndPath(this.gameVariables.pathTilesCoords[0], this.gameVariables.pathTilesCoords[this.gameVariables.pathTilesCoords.Count - 1]);

        //Create enemy path on grid
        PlacePointInMap();

        //Spawn items on map
        SpawnItemsOnGrid();



    }

    private void InitGrid() {
        //Init grids holder
        this.gameVariables.gridsHolder = new GameObject("GridsStuff");

        //Init hidden grid
        this.mapVariables.hiddenGrid = GameObject.Instantiate<Grid>(this.mapVariables.hiddenGridPrefab, this.gameVariables.gridsHolder.transform);

        //Init map grid entity
        this.mapVariables.mapGrid = new GridEntity("MapMap", this.mapVariables.hiddenGrid, this.mapVariables.mapStartPointInMap, this.gameVariables.mapRows, this.gameVariables.mapColumns, this.gameVariables.inactiveTilesCoords, this.gameVariables.pathTilesCoords, this.mapVariables.tileSidesPrefab);

    }

    private void SpawnItemsOnGrid() {
        //Get info package
        Dictionary<Vector2, TowerType> towersInfo = MapInfoPck.Instance.TileTowerInfos;
        Dictionary<Vector2, TrapType> trapsInfo = MapInfoPck.Instance.TileTrapInfos;


        //Towers
        foreach (KeyValuePair<Vector2, TowerType> info in towersInfo) {
            //Get tile Coords
            Vector2 tileCoords = this.mapVariables.mapGrid.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.mapVariables.mapGrid.GetTileCenterFixed(tileCoords);

            //Create obj
            Tower tower = towerManager.CreateTower(info.Value, tileCenter);
        }

        //Traps
        foreach (KeyValuePair<Vector2, TrapType> info in trapsInfo) {
            //Get tile Coords
            Vector2 tileCoords = this.mapVariables.mapGrid.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.mapVariables.mapGrid.GetTileCenterFixed(tileCoords);

            //Create obj
            trapManager.CreateTrap(info.Value, tileCenter);
        }    
    }

    private void StartEndPath(Vector2 startPath, Vector2 endPath)
    {
        this.mapVariables.enemyStart.transform.position = this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(startPath));
        this.mapVariables.enemyEnd.transform.position = this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(endPath));
        this.mapVariables.enemyPoint.transform.position = this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(startPath));
    }

    private void PlacePointInMap()
    {
        GameObject enemyPoint = this.mapVariables.enemyParentPoint;
        foreach (Vector2 vec in GameVariables.instance.pathTilesCoords)
        {
            GameObject ob = GameObject.Instantiate(this.mapVariables.enemyPoint,
                this.mapVariables.mapGrid.GetTileCenterFixed(this.mapVariables.mapGrid.GetTileCoords(vec)), 
                Quaternion.identity,
                enemyPoint.transform);
        }
        EnemyManager.Instance.SetPoints(enemyPoint.transform);
    }
}
