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

    private GameObject objInMyHand;
    public GameObject objSelected;
    public ItemValue objValue;

    private Dictionary<TowerType, ObjInfo> towerSpawnInfo = new Dictionary<TowerType, ObjInfo>();
    private Dictionary<TrapType, ObjInfo> trapSpawnInfo = new Dictionary<TrapType, ObjInfo>();

    private GameObject shopItemsHolder;


    private GameObject lol;

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

        if (this.objSelected == null) {
            if (Input.GetKeyDown(KeyCode.Q))
                //TODO change param for item picked up
                ListenForItemPickedUp(this.lol);
        }
        else {
            ListenForBuyingInput();
            ListenForItemDropped();
            this.placingObjectManager.MoveObj(this.objSelected);
        }
    }

    //If it comes here, its because its a shop item for sure!!!
    private void ListenForItemPickedUp(GameObject obj) {
        //Make sure hand is empty
        if (this.objInMyHand == null) {
            //TODO remove
            obj.transform.position = new Vector3(0, 10, 0);

            this.objInMyHand = obj;
            this.objValue = obj.GetComponent<ItemValue>();

            //Spawn ghost obj
            SpawnObjGrabbed(this.objValue);

            if (this.objValue != null) {
                //Check if item was pickup from shop or map
                if (!this.objValue.itemWasPlacedOnMap)
                    //Respawn the right obj
                    SpawnTheRightObj();
            }
        }
    }

    private void ListenForItemDropped() {
        //TODO implement real inputs
        if (Input.GetButtonDown("Jump")) {
            if (this.objValue.itemWasPlacedOnMap) {
                //Place back to where it was
                PlaceObjBackWhereItWas(this.objInMyHand, this.objValue);
            }
            else {
                //Remove from map
                RemoveObjWithAnimation(this.objSelected);

                this.objInMyHand = null;
                this.objSelected = null;
                this.objValue = null;
            }
        }
    }

    private void ListenForBuyingInput() {
        //TODO implement real buying inputs
        if (Input.GetMouseButton(1)) {
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

    }

    private void SpawnTheRightObj() {
        //Test if turret or trap
        if(this.objValue.towerType != null) {
            this.timeManager.AddTimedAction(new TimedAction(() => { SpawnTurretInShop((TowerType)this.objValue.towerType); }, 3f));
        }
        else if (this.objValue.trapType != null) {
            this.timeManager.AddTimedAction(new TimedAction(() => { SpawnTrapInShop((TrapType)this.objValue.trapType); }, 3f));
        }
    }

    private void SpawnObjGrabbed(ItemValue itemValue) {
        //Instantiate obj
        if(itemValue.towerType != null) {
            this.objSelected = GameObject.Instantiate<GameObject>(this.towerSpawnInfo[(TowerType)itemValue.towerType].objPrefab);
        }
        else if (itemValue.trapType != null) {
            this.objSelected = GameObject.Instantiate<GameObject>(this.trapSpawnInfo[(TrapType)itemValue.trapType].objPrefab);
        }

        //Set item to inactive
        this.objSelected.SetActive(false);
    }

    private void SpawnTurretInShop(TowerType type) {
        //Get turret spawn position
        Vector3 spawnPosition = this.towerSpawnInfo[type].spawnPosition;
        //---------------------TODO-----------------
        //Spawn turret
        this.lol = GameObject.Instantiate<GameObject>(this.gameVariables.turretBasicPrefab, this.shopItemsHolder.transform);
        this.lol.transform.position = spawnPosition;
    }

    private void SpawnTrapInShop(TrapType type) {
        //Get trap spawn position
        Vector3 spawnPosition = this.trapSpawnInfo[type].spawnPosition;
        //---------------------TODO-----------------
        //Spawn trap
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
//         SpawnTurretInShop(TowerType.HEAVY);
//         SpawnTurretInShop(TowerType.ICE);
// 
//         SpawnTrapInShop(TrapName.MINE);
//         SpawnTrapInShop(TrapName.GLUE);
//         SpawnTrapInShop(TrapName.SPIKE);
    }

    private void InitSpawnPositions() {
        //Get all positions available in shop
        Dictionary<ushort, Tile> tiles = GridManager.Instance.GetGrid("ShopRoom").tiles;

        //Init turrets
        this.towerSpawnInfo.Add(TowerType.BASIC, new ObjInfo(this.gameVariables.turretBasicPrefab, tiles[1].CenterPosition));
//         this.towerSpawnPositions.Add(TowerType.BASIC, tiles[1].CenterPosition);
//         this.towerSpawnPositions.Add(TowerType.HEAVY, tiles[2].CenterPosition);
//         this.towerSpawnPositions.Add(TowerType.ICE, tiles[3].CenterPosition);
// 
//         //Init traps
//         this.trapSpawnPositions.Add(TrapName.GLUE, tiles[14].CenterPosition);
//         this.trapSpawnPositions.Add(TrapName.MINE, tiles[15].CenterPosition);
//         this.trapSpawnPositions.Add(TrapName.SPIKE, tiles[16].CenterPosition);
    }

    public void TrashObj(GameObject obj) {
        //-----------------TODO--------------------------
        //Get price of obj

        //-----------------TODO--------------------------
        //Give back money to player

        //Remove
        RemoveObjWithAnimation(obj);
    }

    private void PlaceObjBackWhereItWas(GameObject obj, ItemValue itemValue) {
        //Get old position
        Vector3 position = this.gridManager.GetGrid("MapRoom").GetPathTilePosition(itemValue.positionOnMap);

        //Set position to item in my hand
        this.objInMyHand.transform.position = position;

        //Remove ghost item
        RemoveObjWithAnimation(obj);
    }
}
