using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileInfo {
    public ushort Row { get; set; }
    public ushort Column { get; set; }

    public TileInfo() { }
    public TileInfo(ushort row, ushort column) { this.Row = row; this.Column = column; }
}

public class GridEntity {
    private static ulong nextGridId = 0;

    public ulong Id { get; private set; }
    public Dictionary<Vector2, Tile> Tiles { get; private set; } = new Dictionary<Vector2, Tile>();

    private readonly Grid hiddenGrid;

    private GameObject tilesHolder;
    private readonly Transform startPoint;
    private readonly ushort rows, columns;
    private readonly Vector2 tileSize;
    private readonly List<Vector2> pathCorners;

    private bool isHitBoxActive;

    public GridEntity(string name, Grid _HiddenGrid, Transform _StartPoint, ushort _Rows, ushort _Columns, List<Vector2> _PathCorners, GameObject tilePrefab, GameObject hitBoxPrefab, bool _IsHitBoxActive = true) {
        this.Id = GetNextGridId();
        this.hiddenGrid = _HiddenGrid;
        this.startPoint = _StartPoint;
        this.rows = _Rows;
        this.columns = _Columns;
        this.tileSize = this.hiddenGrid.cellSize;
        this.pathCorners = _PathCorners;
        this.isHitBoxActive = _IsHitBoxActive;

        CreateTilesHolder(name);

        InitGridHitBox(hitBoxPrefab, this.startPoint, this.tileSize, this.rows, this.columns);

        InitTiles(this.startPoint, this.rows, this.columns, this.tileSize, this.pathCorners, tilePrefab);
    }
    public GridEntity(string name, Grid _HiddenGrid, Transform _StartPoint, ushort _Rows, ushort _Columns, GameObject tilePrefab) {
        this.hiddenGrid = _HiddenGrid;
        this.startPoint = _StartPoint;
        this.rows = _Rows;
        this.columns = _Columns;
        this.tileSize = new Vector2(this.hiddenGrid.cellSize.x, this.hiddenGrid.cellSize.y);
        this.isHitBoxActive = false;

        CreateTilesHolder(name);

        InitTiles(this.startPoint, this.rows, this.columns, this.tileSize, tilePrefab);
    }

    private void CreateTilesHolder(string name) {
        this.tilesHolder = new GameObject(name);
        this.tilesHolder.transform.position = new Vector3();
        this.tilesHolder.transform.SetParent(GameVariables.instance.gridsHolder.transform);
    }

    private void InitGridHitBox(GameObject hitBoxPrefab, Transform startPoint, Vector2 tileSize, ushort rows, ushort columns) {
        //Instantiate prefab inside parent
        GameObject hitbox = GameObject.Instantiate<GameObject>(hitBoxPrefab, this.tilesHolder.transform);

        //Set at position
        hitbox.transform.position = startPoint.position;
        //hitbox.transform.rotation = this.hiddenGrid.transform.rotation;

        //Resize depending on game board width/height
        Vector3 size = new Vector3((tileSize.x * rows) / 10, 0.2f, (tileSize.y * columns) / 10);        //Divide by 10 because its a plane I think lol
        hitbox.transform.localScale = size;
    }

    //------------TODO----------- implement rotation on hiddenGrid
    private void InitTiles(Transform startPoint, ushort rows, ushort columns, Vector2 tileSize, List<Vector2> pathCorners, GameObject prefab) {
        Quaternion rotation = this.hiddenGrid.transform.rotation;
        Vector3 scale = new Vector3(tileSize.x, 0.5f, tileSize.y);

        //Start at center of the startpoint
        Vector2 tileCoords;
        Vector3 tileCenter;
        float xPos = startPoint.position.x;
        float zPos = startPoint.position.z;

        for (ushort i = 0; i < rows; i++) {
            for (ushort j = 0; j < columns; j++) {
                tileCoords.x = xPos;
                tileCoords.y = zPos;

                //Check if Path or Map
                tileCenter = new Vector3(tileCoords.x + tileSize.x / 2, startPoint.position.y, tileCoords.y + tileSize.y / 2);

                if (this.pathCorners.Contains(new Vector2(i, j))) {
                    //Add Path tile
                    this.Tiles.Add(tileCoords, new Tile(TileType.PATH, tileCenter, rotation, scale, prefab, this.tilesHolder.transform));
                }
                else {
                    //Add Map tile
                    this.Tiles.Add(tileCoords, new Tile(TileType.MAP, tileCenter, rotation, scale, prefab, this.tilesHolder.transform));
                }

                zPos += tileSize.y;
            }

            xPos += tileSize.x;
            zPos = startPoint.position.z;
        }
    }

    //------------TODO----------- implement rotation on hiddenGrid
    private void InitTiles(Transform startPoint, ushort rows, ushort columns, Vector2 tileSize, GameObject prefab) {
        Quaternion rotation = this.hiddenGrid.transform.rotation;
        Vector3 scale = new Vector3(tileSize.x, 0.5f, tileSize.y);

        //Start at center of the startpoint
        Vector2 tileCoords;
        Vector3 tileCenter;
        float xPos = startPoint.position.x;
        float zPos = startPoint.position.z;

        for (ushort i = 0; i < rows; i++) {
            for (ushort j = 0; j < columns; j++) {
                tileCoords = new Vector2(xPos, zPos);

                //Check if Path or Map
                tileCenter = new Vector3(tileCoords.x + tileSize.x / 2, startPoint.position.y, tileCoords.y + tileSize.y / 2);
                this.Tiles.Add(tileCoords, new Tile(TileType.NONE, tileCenter, rotation, scale, prefab, this.tilesHolder.transform));

                zPos += tileSize.y;
            }

            xPos += tileSize.x;
            zPos = startPoint.position.z;
        }
    }

    public Vector2 GetTileCoords(Vector3 pointInWorld) {
        Vector3Int tileCoords = this.hiddenGrid.WorldToCell(pointInWorld);
        return new Vector2((int)tileCoords.x, (int)-tileCoords.y - 1);        //NOT SURE IT SHOULD BE MINUS Y
    }
    public Vector2 GetTileCoords(TileInfo tileInfo) {
        Vector2 coords = new Vector2();

        coords.x = this.startPoint.position.x + tileInfo.Row * this.tileSize.x;
        coords.y = this.startPoint.position.z + tileInfo.Column * this.tileSize.y;

        return coords;
    }

    //--------------TODO-------------- Implement grid rotation
    public TileInfo GetRowColumn(Vector2 coords) {
        TileInfo tileInfo = null;

        //Test if tile in grid
        if (IsTileInGrid(coords)) {
            tileInfo = new TileInfo();

            //Find starting tile coord
            Vector2 startTileCoord = GetTileCoords(this.startPoint.position);

            //Get distance between the two coords
            ushort row = (ushort)((coords.x - startTileCoord.x) / this.tileSize.x);
            ushort column = (ushort)((coords.y - startTileCoord.y) / this.tileSize.y);

            tileInfo.Row = row;
            tileInfo.Column = column;
        }

        return tileInfo;
    }

    public Vector3 GetTileCenter(Vector2 tileCoords) {
        return new Vector3(tileCoords.x + this.tileSize.x / 2, this.startPoint.position.y, (tileCoords.y - this.tileSize.y / 2) + 1);       // + 1 because needed a quick fix
    }

    public TileType GetTileType(Vector2 tileCoords) {
        return this.Tiles[tileCoords].Type;
    }

    public bool IsTileInGrid(Vector2 coords) {
        bool tileIsInGrid = false;

        if (this.Tiles.ContainsKey(coords))
            tileIsInGrid = true;

        return tileIsInGrid;
    }
    public bool IsTileInGrid(TileInfo tileInfo) {
        bool tileIsInGrid = false;

        if (this.Tiles.ContainsKey(GetTileCoords(tileInfo)))
            tileIsInGrid = true;

        return tileIsInGrid;
    }

    public void SetHitBoxActive(bool active) {
        this.isHitBoxActive = active;
    }

    private ulong GetNextGridId() {
        nextGridId++;
        return nextGridId;
    }
}