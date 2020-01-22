using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjInfo {
    public GameObject objPrefab;
    public GameObject ghostPrefab;
    public Vector3 spawnPosition;

    public ObjInfo(GameObject objPrefab, GameObject ghostPrefab, Vector3 spawnPositionInShop) {
        this.objPrefab = objPrefab;
        this.ghostPrefab = ghostPrefab;
        this.spawnPosition = spawnPositionInShop;
    }
}

public class ShopManager : Flow {

    #region Singleton
    private ShopManager() { }
    private static ShopManager instance;
    public static ShopManager Instance {
        get {
            return instance ?? (instance = new ShopManager());
        }
    }
    #endregion

    //
    GameVariables gameVariables;
    RoomVariables roomVariables;

    private Dictionary<HandType, HandShop> hands = new Dictionary<HandType, HandShop>();

    public GridEntity Map { get; private set; }
    private GridEntity shopTurret;
    private GridEntity shopTrap;

    public Dictionary<TowerType, ObjInfo> towerSpawnInfo = new Dictionary<TowerType, ObjInfo>();
    public Dictionary<TrapType, ObjInfo> trapSpawnInfo = new Dictionary<TrapType, ObjInfo>();

    private GameObject shopItemsHolder;

    private GameObject hiddenGridPrefab;
    private GameObject hiddenHitBoxPrefab;
    private GameObject tileSidesPrefab;

    private Grid hiddenGrid;



    public override void EndFlow() {
        instance = null;
    }
    public override void PreInitialize() {
        //Load Resources
        this.hiddenGridPrefab = Resources.Load<GameObject>("Prefabs/Grid/HiddenGridShop");
        this.hiddenHitBoxPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_HitBox");
        this.tileSidesPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_Shop");
    }

    public override void Initialize() {

        this.gameVariables = GameVariables.instance;
        this.roomVariables = RoomVariables.instance;


        //Init grids holder
        GameVariables.instance.gridsHolder = new GameObject("GridsStuff");

        //Init shop items holder
        this.shopItemsHolder = new GameObject("ShopItems");
        this.shopItemsHolder.transform.position = new Vector3();

        //Init hidden grid
        this.hiddenGrid = GameObject.Instantiate<GameObject>(this.hiddenGridPrefab, GameVariables.instance.gridsHolder.transform).GetComponent<Grid>();

        InitializeGrids();
        InitializeHands();
        InitializeShopItems();
        InitializeItemOnMap();
    }

    public override void Refresh() {  }

    private void InitializeGrids() {
        //Init grids
        this.Map = new GridEntity("MapShop", this.hiddenGrid, this.roomVariables.mapStartPointInShop, this.gameVariables.mapRows, this.gameVariables.mapColumns, this.gameVariables.inactiveTilesCoords, this.gameVariables.pathTilesCoords, this.tileSidesPrefab, this.hiddenHitBoxPrefab);
        this.shopTurret = new GridEntity("ShopTurret", this.hiddenGrid, this.roomVariables.shopTurretStartPoint, this.roomVariables.shopTurretRows, this.roomVariables.shopTurretColumns, this.tileSidesPrefab);
        this.shopTrap = new GridEntity("ShopTrap", this.hiddenGrid, this.roomVariables.shopTrapStartPoint, this.roomVariables.shopTrapRows, this.roomVariables.shopTrapColumns, this.tileSidesPrefab);
    }

    private void InitializeHands() {
        this.hands.Add(HandType.LEFT, new HandShop(HandType.LEFT));
        this.hands.Add(HandType.RIGHT, new HandShop(HandType.RIGHT));
    }

    private void InitializeShopItems() {
        InitSpawnPositions();

        SpawnTurretInShop(TowerType.BASIC);
        SpawnTurretInShop(TowerType.HEAVY);
        SpawnTurretInShop(TowerType.ICE);
        // 
        SpawnTrapInShop(TrapType.GLUE);
        SpawnTrapInShop(TrapType.MINE);
        SpawnTrapInShop(TrapType.SPIKE);
    }

    private void InitializeItemOnMap() {
        //MapInfoPck.Instance.TestPopulate();

        //Get info package
        Dictionary<Vector2, TowerType> towersInfo = MapInfoPck.Instance.TileTowerInfos;
        Dictionary<Vector2, TrapType> trapsInfo = MapInfoPck.Instance.TileTrapInfos;


        //Towers
        foreach (KeyValuePair<Vector2, TowerType> info in towersInfo) {
            //Get tile Coords
            Vector2 tileCoords = this.Map.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.Map.GetTileCenterFixed(tileCoords);

            //Create obj
            GameObject obj = SpawnTurret(info.Value, tileCenter);

            //Set tower piece info
//             TowerPiece towerPiece = obj.GetComponent<TowerPiece>();
//             towerPiece.itemWasPlacedOnMap = true;
//             towerPiece.positionOnMap = tileCoords;
        }

        //Traps
        foreach (KeyValuePair<Vector2, TrapType> info in trapsInfo) {
            //Get tile Coords
            Vector2 tileCoords = this.Map.GetTileCoords(info.Key);

            //Get tileCenter
            Vector3 tileCenter = this.Map.GetTileCenterFixed(tileCoords);

            //Create obj
            GameObject obj = SpawnTrap(info.Value, tileCenter);

            //Set trap piece info
//             TrapPiece trapPiece = obj.GetComponent<TrapPiece>();
//             trapPiece.itemWasPlacedOnMap = true;
//             trapPiece.positionOnMap = tileCoords;
        }
    }

    private void InitSpawnPositions() {
        //Get all positions available in shop
        Dictionary<Vector2, Tile> tilesTurretShop = this.shopTurret.Tiles;
        Dictionary<Vector2, Tile> tilesTrapShop = this.shopTrap.Tiles;

        GameObject obj;
        GameObject ghost;
        //Init obj with row and column
        //Init turrets
        obj = Resources.Load<GameObject>("Prefabs/Room/Towers/TowerPiece_Basic_Room");
        ghost = Resources.Load<GameObject>("Prefabs/Room/Towers/TowerPiece_Basic_Ghost");
        this.towerSpawnInfo.Add(TowerType.BASIC, new ObjInfo(obj, ghost, tilesTurretShop[this.shopTurret.GetTileCoords(new TileInfo(0, 0))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/Towers/TowerPiece_Heavy_Room");
        ghost = Resources.Load<GameObject>("Prefabs/Room/Towers/TowerPiece_Heavy_Ghost");
        this.towerSpawnInfo.Add(TowerType.HEAVY, new ObjInfo(obj, ghost, tilesTurretShop[this.shopTurret.GetTileCoords(new TileInfo(0, 1))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/Towers/TowerPiece_Ice_Room");
        ghost = Resources.Load<GameObject>("Prefabs/Room/Towers/TowerPiece_Ice_Ghost");
        this.towerSpawnInfo.Add(TowerType.ICE, new ObjInfo(obj, ghost, tilesTurretShop[this.shopTurret.GetTileCoords(new TileInfo(1, 0))].TileCenter));

        //Init traps
        obj = Resources.Load<GameObject>("Prefabs/Room/Traps/TrapPiece_Glue_Room");
        ghost = Resources.Load<GameObject>("Prefabs/Room/Traps/TrapPiece_Glue_Ghost");
        this.trapSpawnInfo.Add(TrapType.GLUE, new ObjInfo(obj, ghost, tilesTrapShop[this.shopTrap.GetTileCoords(new TileInfo(0, 0))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/Traps/TrapPiece_Mine_Room");
        ghost = Resources.Load<GameObject>("Prefabs/Room/Traps/TrapPiece_Mine_Ghost");
        this.trapSpawnInfo.Add(TrapType.MINE, new ObjInfo(obj, ghost, tilesTrapShop[this.shopTrap.GetTileCoords(new TileInfo(0, 1))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/Traps/TrapPiece_Spike_Room");
        ghost = Resources.Load<GameObject>("Prefabs/Room/Traps/TrapPiece_Spike_Ghost");
        this.trapSpawnInfo.Add(TrapType.SPIKE, new ObjInfo(obj, ghost, tilesTrapShop[this.shopTrap.GetTileCoords(new TileInfo(1, 0))].TileCenter));
    }

    public void ObjGrabbed(GameObject obj, HandType hand, TowerPiece type) {
        if (this.hands[hand].GrabObj(obj, type))
            //Respawn turret in shop
            TimeManager.Instance.AddTimedAction(new TimedAction(() => { SpawnTurretInShop(type.currentType); },3.0f));
            
    }
    public void ObjGrabbed(GameObject obj, HandType hand, TrapPiece type) {
        if (this.hands[hand].GrabObj(obj, type))
            //Respawn trap in shop
            TimeManager.Instance.AddTimedAction(new TimedAction(() => { SpawnTrapInShop(type.currentType); }, 3.0f));

    }

    public void ObjDropped(HandType hand) {
        this.hands[hand].DropObj();
    }

    public void BuyItem(HandType type) {
        this.hands[type].BuyItem();
    }

    public void ObjTrashed(GameObject obj, HandType hand, TowerPiece type) {
        //----------------TODO----------------
    }
    public void ObjTrashed(GameObject obj, HandType hand, TrapPiece type) {
        //----------------TODO----------------
    }

    public void OnEnterBoard(HandType hand, Vector3 position) {
        this.hands[hand].SetHitPointOnBoard(position);
    }
    public void OnExitBoard(HandType hand) {
        this.hands[hand].SetHitPointOnBoard(null);
    }

    private void SpawnTurretInShop(TowerType type) {
        //Get turret spawn position
        Vector3 spawnPosition = this.towerSpawnInfo[type].spawnPosition;
        //Spawn turret
        GameObject obj = GameObject.Instantiate<GameObject>(this.towerSpawnInfo[type].objPrefab, this.shopItemsHolder.transform);
        obj.transform.position = spawnPosition;
    }

    private void SpawnTrapInShop(TrapType type) {
        //Get trap spawn position
        Vector3 spawnPosition = this.trapSpawnInfo[type].spawnPosition;
        //Spawn trap
        GameObject obj = GameObject.Instantiate<GameObject>(this.trapSpawnInfo[type].objPrefab, this.shopItemsHolder.transform);
        obj.transform.position = spawnPosition;
    }

    private GameObject SpawnTurret(TowerType type, Vector3 spawnPosition) {
        //Create obj
        GameObject obj = GameObject.Instantiate<GameObject>(this.towerSpawnInfo[type].ghostPrefab, this.shopItemsHolder.transform);
        obj.transform.position = spawnPosition;

        return obj;
    }
    private GameObject SpawnTrap(TrapType type, Vector3 spawnPosition) {
        GameObject obj = GameObject.Instantiate<GameObject>(this.trapSpawnInfo[type].ghostPrefab, this.shopItemsHolder.transform);
        obj.transform.position = spawnPosition;

        return obj;
    }
}
