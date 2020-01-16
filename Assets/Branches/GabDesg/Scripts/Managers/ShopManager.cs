using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ObjInfo {
    public GameObject objPrefab;
    public Vector3 spawnPosition;

    public ObjInfo(GameObject objPrefab, Vector3 spawnPositionInShop) {
        this.objPrefab = objPrefab;
        this.spawnPosition = spawnPositionInShop;
    }
}

public class ShopManager : Flow {

    #region Singleton
    private static ShopManager instance;
    public static ShopManager Instance {
        get {
            return instance ?? (instance = new ShopManager());
        }
    }
    #endregion

    PlacingObjectManager placingObjectManager;
    GridManager gridManager;
    TowerManager towerManager;
    TrapManager trapManager;
    TimeManager timeManager;
    GameVariables gameVariables;
    MapInfoPck mapInfoPck;

    private GameObject objInMyHand;
    public GameObject objSelected;

    public TowerPiece towerPiece;
    public TrapPiece trapPiece;

    private Dictionary<TowerType, ObjInfo> towerSpawnInfo = new Dictionary<TowerType, ObjInfo>();
    private Dictionary<TrapType, ObjInfo> trapSpawnInfo = new Dictionary<TrapType, ObjInfo>();

    private GameObject shopItemsHolder;


    private ShopManager() {
        //TODO add ListenForItemPickedUp to Anthony actions
    }

    public override void EndFlow() {
        base.EndFlow();
    }
    public override void PreInitialize() {
        base.PreInitialize();
        this.placingObjectManager = PlacingObjectManager.Instance;
        this.gridManager = GridManager.Instance;
        this.towerManager = TowerManager.Instance;
        this.trapManager = TrapManager.Instance;
        this.timeManager = TimeManager.Instance;
        this.gameVariables = GameVariables.instance;
        this.mapInfoPck = MapInfoPck.Instance;

        this.placingObjectManager.PreInitialize();
    }

    public override void Initialize() {
        base.Initialize();
        this.placingObjectManager.Initialize();
        this.gridManager.InitializeGridShop();

        this.shopItemsHolder = new GameObject("ShopItems");
        this.shopItemsHolder.transform.position = new Vector3();
        InitializeShopItems();
    }

    public override void PhysicsRefresh() {
        base.PhysicsRefresh();
    }

    public override void Refresh() {
        base.Refresh();

        if (this.objSelected != null) {
            ListenForBuyingInput();
            this.placingObjectManager.MoveObj(this.objSelected);
        }
    }

    //If it comes here, its because its a shop item for sure!!!
    public void ListenForItemPickedUp(GameObject obj, TowerPiece towerPiece) {
        //Make sure hand is empty
        if (this.objInMyHand == null) {
            //-----------------------------TODO remove-------------------------
            obj.transform.position = new Vector3(0, 10, 0);

            this.objInMyHand = obj;
            this.towerPiece = towerPiece;

            //Spawn ghost obj
            SpawnObjGrabbed(towerPiece.currentType);

            if (!towerPiece.itemWasPlacedOnMap) {
                //Check if item was pickup from shop or map
                if (!towerPiece.itemWasPlacedOnMap)
                    //Respawn the right obj
                    SpawnTheRightObj(towerPiece);
            }
        }
    }
    public void ListenForItemPickedUp(GameObject obj, TrapPiece trapPiece) {
        //Make sure hand is empty
        if (this.objInMyHand == null) {
            //-----------------------------TODO remove-------------------------
            obj.transform.position = new Vector3(0, 10, 0);

            this.objInMyHand = obj;
            this.trapPiece = trapPiece;

            //Spawn ghost obj
            SpawnObjGrabbed(trapPiece.currentType);

            if (!trapPiece.itemWasPlacedOnMap) {
                //Check if item was pickup from shop or map
                if (!trapPiece.itemWasPlacedOnMap)
                    //Respawn the right obj
                    SpawnTheRightObj(trapPiece);
            }
        }
    }

    public void ListenForItemDropped(GameObject obj, TowerPiece towerPiece) {
        if (towerPiece.itemWasPlacedOnMap) {
            //Place back to where it was
            PlaceObjBackWhereItWas(this.objInMyHand, towerPiece);
        }
        else {
            //Remove from map
            RemoveObjWithAnimation(this.objSelected);

            this.objInMyHand = null;
            this.objSelected = null;
            this.towerPiece = null;
        }
    }

    public void ListenForItemDropped(GameObject obj, TrapPiece trapPiece) {
        //TODO implement real inputs
        if (Input.GetButtonDown("Jump")) {
            if (trapPiece.itemWasPlacedOnMap) {
                //Place back to where it was
                PlaceObjBackWhereItWas(this.objInMyHand, trapPiece);
            }
            else {
                //Remove from map
                RemoveObjWithAnimation(this.objSelected);

                this.objInMyHand = null;
                this.objSelected = null;
                this.trapPiece = null;
            }
        }
    }

    private void ListenForBuyingInput() {
        //TODO implement real buying inputs
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Make sure tile is available
            if (this.placingObjectManager.IsObjectPlaceableThere()) {
                //Save obj in MapInfoPck
                SaveObjPositionInMapPck();

                //-----------------TODO--------------------------
                //Take money from player
            }
        }
    }

    private void SaveObjPositionInMapPck() {
        if (this.towerPiece != null) {
            //Update obj info
            Vector3 tileSelected = this.placingObjectManager.tileSelected;
            this.towerPiece.itemWasPlacedOnMap = true;
            this.towerPiece.positionOnMap = tileSelected;

            //Update MapInfoPck
            this.mapInfoPck.AddTower(this.gridManager.GetTileCoords(tileSelected), this.towerPiece.currentType);
        }
        else if (this.trapPiece != null) {
            //Update obj info
            Vector3 tileSelected = this.placingObjectManager.tileSelected;
            this.trapPiece.itemWasPlacedOnMap = true;
            this.trapPiece.positionOnMap = tileSelected;

            //Update MapInfoPck
            this.mapInfoPck.AddTrap(this.gridManager.GetTileCoords(tileSelected), this.trapPiece.currentType);
        }
    }

    private void SpawnTheRightObj(TowerPiece towerPiece) {
        this.timeManager.AddTimedAction(new TimedAction(() => { SpawnTurretInShop(towerPiece.currentType); }, 3f));
    }
    private void SpawnTheRightObj(TrapPiece trapPiece) {
        this.timeManager.AddTimedAction(new TimedAction(() => { SpawnTrapInShop(trapPiece.currentType); }, 3f));
    }

    private void SpawnObjGrabbed(TowerType type) {
        //Instantiate obj
        this.objSelected = GameObject.Instantiate<GameObject>(this.towerSpawnInfo[type].objPrefab);
        //Set item to inactive
        this.objSelected.SetActive(false);
    }
    private void SpawnObjGrabbed(TrapType type) {
        //Instantiate obj
        this.objSelected = GameObject.Instantiate<GameObject>(this.trapSpawnInfo[type].objPrefab);
        //Set item to inactive
        this.objSelected.SetActive(false);
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

    private void RemoveObjWithAnimation(GameObject obj) {
        //TODO implement animation here
        //Unable collider
        obj.GetComponent<Collider>().enabled = false;

        //Delete obj
        GameObject.Destroy(obj);
    }

    public void MoveSelectedObj(Vector3 newPosition) {
        this.objSelected.transform.position = newPosition;
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
        Dictionary<ushort, Tile> tiles = GridManager.Instance.GetGrid("ShopRoom").tiles;

        GameObject obj;
        //Init turrets
        obj = Resources.Load<GameObject>("Prefabs/Room/TowerPiece_Basic");
        this.towerSpawnInfo.Add(TowerType.BASIC, new ObjInfo(obj, tiles[1].CenterPosition));
        obj = Resources.Load<GameObject>("Prefabs/Room/TowerPiece_Heavy");
        this.towerSpawnInfo.Add(TowerType.HEAVY, new ObjInfo(obj, tiles[2].CenterPosition));
        obj = Resources.Load<GameObject>("Prefabs/Room/TowerPiece_Ice");
        this.towerSpawnInfo.Add(TowerType.ICE, new ObjInfo(obj, tiles[3].CenterPosition));

        //Init traps
        obj = Resources.Load<GameObject>("Prefabs/Room/TrapPiece_Glue");
        this.trapSpawnInfo.Add(TrapType.GLUE, new ObjInfo(obj, tiles[9].CenterPosition));
        obj = Resources.Load<GameObject>("Prefabs/Room/TrapPiece_Mine");
        this.trapSpawnInfo.Add(TrapType.MINE, new ObjInfo(obj, tiles[10].CenterPosition));
        obj = Resources.Load<GameObject>("Prefabs/Room/TrapPiece_Spike");
        this.trapSpawnInfo.Add(TrapType.SPIKE, new ObjInfo(obj, tiles[11].CenterPosition));
    }

    public void TrashObj(GameObject obj) {
        //-----------------TODO--------------------------
        //Get price of obj

        //-----------------TODO--------------------------
        //Give back money to player

        //Remove
        RemoveObjWithAnimation(obj);
    }

    private void PlaceObjBackWhereItWas(GameObject obj, TowerPiece towerPiece) {
        //Get old position
        Vector3 position = this.gridManager.GetGrid("MapRoom").GetCenterTileFromCoords(towerPiece.positionOnMap);

        //Set position to item in my hand
        this.objInMyHand.transform.position = position;

        //Remove ghost item
        RemoveObjWithAnimation(obj);
    }
    private void PlaceObjBackWhereItWas(GameObject obj, TrapPiece trapPiece) {
        //Get old position
        Vector3 position = this.gridManager.GetGrid("MapRoom").GetCenterTileFromCoords(trapPiece.positionOnMap);

        //Set position to item in my hand
        this.objInMyHand.transform.position = position;

        //Remove ghost item
        RemoveObjWithAnimation(obj);
    }
}
