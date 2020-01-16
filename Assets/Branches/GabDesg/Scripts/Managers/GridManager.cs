using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Flow {

    #region Singleton
    private static GridManager instance;
    public static GridManager Instance {
        get {
            return instance ?? (instance = new GridManager());
        }
    }
    #endregion

    //TODO remove turn off obj instead


    public GameObject gridStuffHolder { get; private set; }
    public GameObject gridsHolder { get; private set; }

    private Grid hiddenGrid;

    //Grids
    private Dictionary<string, GridEntity> gridsList = new Dictionary<string, GridEntity>();
    private GridEntity map;
    private GridEntity shop;

    //Prefabs
    public GameObject gridVisualSides { get; private set; }
    public GameObject hiddenGridHitBoxPrefab { get; private set; }
    private GameObject hiddenGridPrefab;
    private GameObject hiddenShopGridPrefab;



    private GridManager() {
        LoadResourcesPath();
    }

    private void LoadResourcesPath() {
        this.gridVisualSides = Resources.Load<GameObject>("Prefabs/Grid/Grid_VisualSides");
        this.hiddenGridPrefab = Resources.Load<GameObject>("Prefabs/Grid/HiddenGrid");
        this.hiddenShopGridPrefab = Resources.Load<GameObject>("Prefabs/Grid/HiddenGridShop");
        this.hiddenGridHitBoxPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_HitBox");
    }

    override public void Initialize() {
        InitializeHolders();
    }

    override public void Refresh() {
    }

    override public void PhysicsRefresh() {

    }

    private void InitializeHolders() {
        //Init Grid Stuff Holder
        this.gridStuffHolder = new GameObject("GridStuff");
        this.gridStuffHolder.transform.position = new Vector3();

        //Init GridsHolder
        this.gridsHolder = new GameObject("GridsHolder");
        this.gridsHolder.transform.SetParent(this.gridStuffHolder.transform);
    }

    public Vector3 GetTileCenterFromWorldPoint(Vector3 pointInWorld) {
        //Get position in grid
        Vector3Int tile = this.hiddenGrid.WorldToCell(pointInWorld);
        //Get tile position in world
        Vector3 tileCenterPosition = this.hiddenGrid.CellToWorld(tile);
        //Add half of tile to get center
        tileCenterPosition.x += this.hiddenGrid.cellSize.x / 2;
        tileCenterPosition.z -= this.hiddenGrid.cellSize.y / 2;

        return tileCenterPosition;
    }

    public bool IsTileInGrid(Vector3 tileCenter) {
        bool tileIsInGrid = false;

        //Check in map if value exist
        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter))
                tileIsInGrid = true;
        }

        return tileIsInGrid;
    }

    //If tile isnt find in any grid, returns EMPTY
    public TileType? GetTileTypeInGrid(Vector3 tileCenter) {
        TileType? type = null;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                type = grid.GetTileTypeAtPosition(tileCenter);
            }
        }

        return type;
    }

    //If tile doesnt exist in any grid, returns null
    public Tile GetTileFromGrid(Vector3 tileCenter) {
        Tile tile = null;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                tile = grid.GetTileAtPosition(tileCenter);
            }
        }

        return tile;
    }

    public GridType? GetGridType(Vector3 tileCenter) {
        GridType? type = null;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                type = grid.Type;
            }
        }

        return type;
    }

    public Vector3? GetTileCenterPositionFromId(ushort id) {
        Vector3? centerPosition = null;

        foreach (GridEntity grid in this.gridsList.Values) {
            centerPosition = grid.GetTileCenterPositionFromId(id);
            if (centerPosition != null)
                break;
        }

        return centerPosition;
    }

    public ushort? GetTileId(Vector3 tileCenter) {
        ushort? id = null;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                id = grid.GetTileId(tileCenter);
            }
        }

        return id;
    }

    public GridEntity GetGrid(string name) {
        GridEntity grid = null;
        if (this.gridsList.ContainsKey(name))
            grid = this.gridsList[name];
        return grid;
    }

    public Vector2 GetTileCoordsFromTileId(string gridName, ushort tileId) {
        return this.gridsList[gridName].TileIdToCoord(tileId);
    }

    //Returns the hit position in world
    public bool LookForHitOnTables(out Vector3? hitPointInWorld) {
        bool tableHasBeenHit = false;
        hitPointInWorld = null;

        //Create a ray from Camera -> Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 300, 1 << LayerMask.NameToLayer("GameBoard"));

        RaycastHit closestHit;
        if (hits.Length > 0) {
            tableHasBeenHit = true;

            closestHit = hits[0];

            if (hits.Length >= 2) {
                //Find which table has been hit
                //TODO Change Camera position to Player position
                closestHit = GetClosestHit(hits, Camera.main.transform.position);
            }

            hitPointInWorld = closestHit.point;
        }

        return tableHasBeenHit;
    }

    private RaycastHit GetClosestHit(RaycastHit[] hits, Vector3 positionToCompareTo) {
        ushort indexSmallestDistance = 0;
        Vector3 closestHit = hits[indexSmallestDistance].point;

        for (ushort i = 1; i < hits.Length; i++) {
            //Find smallest distance
            if (Vector3.Distance(positionToCompareTo, hits[i].point) < Vector3.Distance(positionToCompareTo, closestHit)) {
                closestHit = hits[i].point;
                indexSmallestDistance = i;
            }
        }

        return hits[indexSmallestDistance];
    }

    override public void EndFlow() {
        //TODO Free memory
    }

    private void InitializeItemsOnGrid() {
        //Get items list
        Dictionary<Vector2, ItemValue> items = MapInfoPck.Instance.TileInfos;

        foreach (KeyValuePair<Vector2, ItemValue> item in items) {
            //Figure out which enum it is (turret/trap)

            //Instantiate obj with managers
        }
    }

    public void InitializeGridShop() {
        InitHiddenShopGrid();
        InitGridShopRoom();
        InitGridMapRoom();
    }

    private void InitGridShopRoom() {
        string gridName = "ShopRoom";
        Vector3 position = GameVariables.instance.shopRoomPosition.position;
        ushort shopRows = GameVariables.instance.shopGridHeight, shopColumns = GameVariables.instance.shopGridWidth;

        this.shop = new GridEntity(gridName, GridType.SHOP, position, shopColumns, shopRows, new Vector2(this.hiddenGrid.cellSize.x, this.hiddenGrid.cellSize.y));
        this.gridsList.Add(gridName, this.shop);
    }
    private void InitGridMapRoom() {
        string gridName = "MapRoom";
        Vector3 position = GameVariables.instance.mapRoomPosition.position;
        ushort shopRows = GameVariables.instance.mapGridHeight, shopColumns = GameVariables.instance.mapGridWidth;

        this.map = new GridEntity(gridName, GridType.MAP, position, shopColumns, shopRows, new Vector2(this.hiddenGrid.cellSize.x, this.hiddenGrid.cellSize.y));
        this.gridsList.Add(gridName, this.map);
    }

    private void InitHiddenShopGrid() {
        this.hiddenGrid = GameObject.Instantiate<GameObject>(this.hiddenShopGridPrefab).GetComponent<Grid>();
        this.hiddenGrid.transform.SetParent(this.gridStuffHolder.transform);
    }
    private void InitHiddenMapGrid() {
        this.hiddenGrid = GameObject.Instantiate<GameObject>(this.hiddenGridPrefab).GetComponent<Grid>();
        this.hiddenGrid.transform.SetParent(this.gridStuffHolder.transform);
    }

}
