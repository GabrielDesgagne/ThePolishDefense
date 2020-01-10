using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager {

    #region Singleton
    private static GridManager instance;
    public static GridManager Instance {
        get {
            return instance ?? (instance = new GridManager());
        }
    }
    #endregion

    public GameVariables gameVariables;

    private Grid hiddenGrid;
    private Vector3 tileSelected;

    private GridEntity map;
    private GridEntity shop;

    public void Initialise() {
        this.gameVariables = GameObject.Find("GameLogic").GetComponent<GameVariables>();

        this.hiddenGrid = gameVariables.hiddenGrid;
        tileSelected = GetTilePointIsIn(GetWorldMousePositionOnGrid());

        InitialiseGrids();
    }

    public void Refresh() {
        if (HasTileSelectedChanged()) {
            Debug.Log("Changed tile, now at: " + this.tileSelected.ToString());
        }
    }

    public void PhysicRefresh() {

    }

    private void InitialiseGrids() {
        this.map = new GridEntity("Map", GridType.MAP, new Vector3(0, 0, 0), 15, 15, new Vector2(10, 10));
        this.shop = new GridEntity("Shop", GridType.SHOP, new Vector3(-50, 0, 0), 3, 9, new Vector2(10, 10));
    }

    public bool HasTileSelectedChanged() {
        bool changedTile = false;

        //Compare old selection with new selection
        Vector3 newTileSelected = GetTilePointIsIn(GetWorldMousePositionOnGrid());

        if (newTileSelected != this.tileSelected) {
            changedTile = true;
            Debug.Log("OldPos: " + this.tileSelected.ToString() + ", NewPos: " + newTileSelected.ToString());
            this.tileSelected = newTileSelected;
        }

        return changedTile;
    }

    private Vector3 GetTilePointIsIn(Vector3 pointInWorld) {
        //Get position in grid
        Vector3Int tile = this.hiddenGrid.WorldToCell(pointInWorld);
        //Get tile center position in world
        Vector3 tileCenterPosition = this.hiddenGrid.CellToWorld(tile);

        return new Vector3(tileCenterPosition.x, this.hiddenGrid.transform.position.y, tileCenterPosition.z);
    }

    private Vector3 GetWorldMousePositionOnGrid() {
        //Get mouse position
        Vector3 mousePosition = Input.mousePosition;

        //         RaycastHit hit;
        //         Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        // 
        //         if (Physics.Raycast(ray,  out hit)) {
        //             Transform objectHit = hit.transform;
        //         }
        return mousePosition;
    }
}
