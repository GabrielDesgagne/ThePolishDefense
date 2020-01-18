using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjInfo {
    public GameObject objPrefab;
    public Vector3 spawnPosition;

    public ObjInfo(GameObject objPrefab, Vector3 spawnPositionInShop) {
        this.objPrefab = objPrefab;
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

    private Dictionary<HandType, HandShop> hands = new Dictionary<HandType, HandShop>();

    public GridEntity Map { get; private set; }
    private GridEntity shop;

    public Dictionary<TowerType, ObjInfo> towerSpawnInfo = new Dictionary<TowerType, ObjInfo>();
    public Dictionary<TrapType, ObjInfo> trapSpawnInfo = new Dictionary<TrapType, ObjInfo>();

    private GameObject shopItemsHolder;

    private GameObject hiddenGridPrefab;
    private GameObject hiddenHitBoxPrefab;
    private GameObject tileSidesPrefab;

    private Grid hiddenGrid;



    public override void EndFlow() { }
    public override void PreInitialize() {
        //Load Resources
        this.hiddenGridPrefab = Resources.Load<GameObject>("Prefabs/Grid/HiddenGridShop");
        this.hiddenHitBoxPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_HitBox");
        this.tileSidesPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_VisualSides");
    }

    public override void Initialize() {

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
    }

    public override void Refresh() {
        foreach (HandShop hand in this.hands.Values) {
            hand.Refresh();
        }
    }

    private void InitializeGrids() {
        //Init grids
        this.Map = new GridEntity("MapShop", this.hiddenGrid, GameVariables.instance.mapStartPoint, GameVariables.instance.mapRows, GameVariables.instance.mapColumns, GameVariables.instance.pathTilesCoords, this.tileSidesPrefab, this.hiddenHitBoxPrefab);
        this.shop = new GridEntity("ShopShop", this.hiddenGrid, GameVariables.instance.shopStartPoint, GameVariables.instance.shopRows, GameVariables.instance.shopColumns, this.tileSidesPrefab);
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

    private void InitSpawnPositions() {
        //Get all positions available in shop
        Dictionary<Vector2, Tile> tiles = this.shop.Tiles;

        GameObject obj;
        //Init obj with row and column
        //Init turrets
        obj = Resources.Load<GameObject>("Prefabs/Room/TowerPiece_Basic");
        this.towerSpawnInfo.Add(TowerType.BASIC, new ObjInfo(obj, tiles[this.shop.GetTileCoords(new TileInfo(0, 0))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/TowerPiece_Heavy");
        this.towerSpawnInfo.Add(TowerType.HEAVY, new ObjInfo(obj, tiles[this.shop.GetTileCoords(new TileInfo(0, 1))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/TowerPiece_Ice");
        this.towerSpawnInfo.Add(TowerType.ICE, new ObjInfo(obj, tiles[this.shop.GetTileCoords(new TileInfo(0, 2))].TileCenter));

        //Init traps
        obj = Resources.Load<GameObject>("Prefabs/Room/TrapPiece_Glue");
        this.trapSpawnInfo.Add(TrapType.GLUE, new ObjInfo(obj, tiles[this.shop.GetTileCoords(new TileInfo(1, 0))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/TrapPiece_Mine");
        this.trapSpawnInfo.Add(TrapType.MINE, new ObjInfo(obj, tiles[this.shop.GetTileCoords(new TileInfo(1, 1))].TileCenter));
        obj = Resources.Load<GameObject>("Prefabs/Room/TrapPiece_Spike");
        this.trapSpawnInfo.Add(TrapType.SPIKE, new ObjInfo(obj, tiles[this.shop.GetTileCoords(new TileInfo(1, 2))].TileCenter));
    }

    public void ObjGrabbed(GameObject obj, HandType hand, TowerPiece type) {
        if (this.hands[hand].GrabObj(obj, type))
            //Respawn turret in shop
            SpawnTurretInShop(type.currentType);
    }
    public void ObjGrabbed(GameObject obj, HandType hand, TrapPiece type) {
        if (this.hands[hand].GrabObj(obj, type))
            //Respawn trap in shop
            SpawnTrapInShop(type.currentType);
    }

    public void ObjDropped(HandType hand) {
        this.hands[hand].DropObj();
    }

    public void ObjTrashed() {
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
}
