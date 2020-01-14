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
    private Dictionary<ushort, GridEntity> gridsList = new Dictionary<ushort, GridEntity>();
    private GridEntity map;
    private GridEntity shop;

    //Prefabs
    public GameObject gridVisualSides { get; private set; }
    public GameObject hiddenGridHitBoxPrefab { get; private set; }
    private GameObject hiddenGridPrefab;



    private GridManager() {
        LoadResourcesPath();
    }

    private void LoadResourcesPath() {
        this.gridVisualSides = Resources.Load<GameObject>("Prefabs/Grid/Grid_VisualSides");
        this.hiddenGridPrefab = Resources.Load<GameObject>("Prefabs/Grid/HiddenGrid");
        this.hiddenGridHitBoxPrefab = Resources.Load<GameObject>("Prefabs/Grid/Grid_HitBox");
    }

    override public void Initialize() {
        InitializeHolders();

        InitializeHiddenGrid();

        InitializeGrids();
    }

    override public void Refresh() {
        //DisplayObjectOnGrid(this.gameVariables.randomPrefab);
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

    private void InitializeHiddenGrid() {
        this.hiddenGrid = GameObject.Instantiate<GameObject>(this.hiddenGridPrefab).GetComponent<Grid>();
        this.hiddenGrid.transform.SetParent(this.gridStuffHolder.transform);
    }

    private void InitializeGrids() {

        //TODO get those outside of here RIGHT NOOOOOOWWW
        string mapGridName = "Map";
        Vector3 mapPosition = new Vector3();
        GridType mapGridType = GridType.MAP;
        ushort mapWidth = 15, mapHeight = 15;

        this.map = new GridEntity(mapGridName, mapGridType, mapPosition, mapWidth, mapHeight, new Vector2(this.hiddenGrid.cellSize.x, this.hiddenGrid.cellSize.y));
        this.gridsList.Add(this.map.Id, this.map);


        //this.shop = new GridEntity("Shop", GridType.SHOP, new Vector3(-50, 0, 0), 3, 9, new Vector2(this.gameVariables.hiddenGridWidth, this.gameVariables.hiddenGridHeight));
    }

    //     public void DisplayObjectOnGrid(GameObject objectToDisplay) {
    //         //Check if table hit by laser
    //         Vector3 targetPointInWorld = new Vector3();
    //         if (LookForHitOnTables(ref targetPointInWorld)) {
    //             //Get tile position targetPoint is in
    //             Vector3 tileCenter = GetTilePositionFromWorldPoint(targetPointInWorld);
    // 
    //             //Update position to center of the tile
    //             tileCenter.x += this.hiddenGrid.cellSize.x / 2;
    //             tileCenter.z -= this.hiddenGrid.cellSize.y / 2;
    // 
    //             //Get table height
    //             tileCenter.y = targetPointInWorld.y;
    // 
    //             if (HasTileSelectedChanged(tileCenter)) {
    //                 //Display prefab at new position
    //                 objectToDisplay.transform.position = tileCenter;
    //                 //TODO Start event tile changed -> make sound change lighing position etc...
    //             }
    //         }
    //         else {
    //             //TODO deactivate obj
    // 
    //             //Dont display turret on grid anymore
    //             //objectToDisplay.transform.position = this.tileSelected;
    //         }
    //     }

    //     public bool HasTileSelectedChanged(Vector3 newTileSelected) {
    //         bool changedTile = false;
    // 
    //         if (newTileSelected != this.tileSelected) {
    //             changedTile = true;
    //             this.tileSelected = newTileSelected;
    //         }
    // 
    //         return changedTile;
    //     }

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
    public TileType GetTileTypeInGrid(Vector3 tileCenter) {
        TileType type = TileType.EMPTY;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                type = grid.GetTileTypeAtPosition(tileCenter);
            }
        }

        return type;
    }

    public Tile GetTileFromGrid(Vector3 tileCenter) {
        Tile tile = null;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                tile = grid.GetTileAtPosition(tileCenter);
            }
        }

        return tile;
    }

    public GridType GetGridType(Vector3 tileCenter) {
        GridType type = GridType.NONE;

        foreach (GridEntity grid in this.gridsList.Values) {
            if (grid.IsTileInGrid(tileCenter)) {
                type = grid.Type;
            }
        }

        return type;
    }


    //Returns the hit position in world
    private bool LookForHitOnTables(out Vector3 hitPointInWorld) {
        bool tableHasBeenHit = false;
        hitPointInWorld = new Vector3();

        //Create a ray from Camera -> Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 200, 1 << LayerMask.NameToLayer("GameBoard"));

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

}
