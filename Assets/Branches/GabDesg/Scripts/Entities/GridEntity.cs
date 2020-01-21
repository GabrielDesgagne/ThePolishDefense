using System.Collections.Generic;
using UnityEngine;


public class TileInfo
{
    public ushort Row { get; set; }
    public ushort Column { get; set; }

    public TileInfo() { }
    public TileInfo(ushort row, ushort column) { this.Row = row; this.Column = column; }
}

public class GridEntity
{
    private static ulong nextGridId = 0;

    public ulong Id { get; private set; }
    public Dictionary<Vector2, Tile> Tiles { get; private set; } = new Dictionary<Vector2, Tile>();

    private readonly Grid hiddenGrid;

    private GameObject tilesHolder;
    private readonly Transform startPoint;
    private readonly ushort rows, columns;
    private readonly Vector2 tileSize;
    private readonly List<Vector2> pathCorners;
    private GrabbableObject grabbableComponent;

    private bool isHitBoxActive;

    public GridEntity(string name, Grid _HiddenGrid, Transform _StartPoint, ushort _Rows, ushort _Columns, List<Vector2> _PathCorners, GameObject tilePrefab, GameObject hitBoxPrefab, bool _IsHitBoxActive = true)
    {
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

    public GridEntity(string name, Grid _HiddenGrid, Transform _StartPoint, ushort _Rows, ushort _Columns, List<Vector2> _PathCorners, GameObject tilePrefab)
    {
        this.Id = GetNextGridId();
        this.hiddenGrid = _HiddenGrid;
        this.startPoint = _StartPoint;
        this.startPoint.position = (Vector3)this.hiddenGrid.WorldToCell(_StartPoint.position);
        this.rows = _Rows;
        this.columns = _Columns;
        this.tileSize = this.hiddenGrid.cellSize;
        this.pathCorners = _PathCorners;
        this.isHitBoxActive = false;

        CreateTilesHolder(name);

        InitTiles(this.startPoint, this.rows, this.columns, this.tileSize, this.pathCorners, tilePrefab);
    }

    public GridEntity(string name, Grid _HiddenGrid, Transform _StartPoint, ushort _Rows, ushort _Columns, GameObject tilePrefab)
    {
        this.hiddenGrid = _HiddenGrid;
        this.startPoint = _StartPoint;
        this.rows = _Rows;
        this.columns = _Columns;
        this.tileSize = new Vector2(this.hiddenGrid.cellSize.x, this.hiddenGrid.cellSize.y);
        this.isHitBoxActive = false;

        CreateTilesHolder(name);

        InitTiles(this.startPoint, this.rows, this.columns, this.tileSize, tilePrefab);
    }

    private void CreateTilesHolder(string name)
    {
        this.tilesHolder = new GameObject(name);
        this.tilesHolder.transform.position = new Vector3();
        this.tilesHolder.transform.SetParent(GameVariables.instance.gridsHolder.transform);
    }

    private void InitGridHitBox(GameObject hitBoxPrefab, Transform startPoint, Vector2 tileSize, ushort rows, ushort columns)
    {
        //Instantiate prefab inside parent
        GameObject hitbox = GameObject.Instantiate<GameObject>(hitBoxPrefab, this.tilesHolder.transform);
        grabbableComponent = hitbox.GetComponent<GrabbableObject>();
        //Set at position
        hitbox.transform.position = startPoint.position;
        //hitbox.transform.rotation = this.hiddenGrid.transform.rotation;

        //Resize depending on game board width/height
        Vector3 size = new Vector3((tileSize.x * rows) / 10f, 0.2f, (tileSize.y * columns) / 10f);        //Divide by 10 because its a plane I think lol
        hitbox.transform.localScale = size;
    }

    //------------TODO----------- implement rotation on hiddenGrid
    private void InitTiles(Transform startPoint, ushort rows, ushort columns, Vector2 tileSize, List<Vector2> pathCorners, GameObject prefab)
    {
        Quaternion rotation = this.hiddenGrid.transform.rotation;
        Vector3 scale = new Vector3(tileSize.x, prefab.transform.localScale.y, tileSize.y);

        //Start at center of the startpoint
        Vector2 tileCoords;
        Vector3 tileCenter;
        float xPos = startPoint.position.x;
        float zPos = startPoint.position.z;

        for (ushort i = 0; i < rows; i++)
        {
            for (ushort j = 0; j < columns; j++)
            {
                tileCoords.x = xPos;
                tileCoords.y = zPos;

                //Check if Path or Map
                tileCenter = new Vector3(tileCoords.x + tileSize.x / 2f, startPoint.position.y, tileCoords.y + tileSize.y / 2f);

                if (this.pathCorners.Contains(new Vector2(i, j)))
                {
                    //Add Path tile
                    Vector3Int realCoords = hiddenGrid.WorldToCell(tileCenter);
                    Vector2 coords = new Vector2(realCoords.x, -realCoords.y);

                    if (!this.Tiles.ContainsKey(coords))
                        this.Tiles.Add(coords, new Tile(TileType.PATH, tileCenter, rotation, scale, prefab, this.tilesHolder.transform));
                }
                else
                {
                    //Add Map tile
                    Vector3Int realCoords = hiddenGrid.WorldToCell(tileCenter);
                    Vector2 coords = new Vector2(realCoords.x, -realCoords.y);

                    if (!this.Tiles.ContainsKey(coords))
                        this.Tiles.Add(coords, new Tile(TileType.MAP, tileCenter, rotation, scale, prefab, this.tilesHolder.transform));
                }

                zPos += tileSize.y;
            }

            xPos += tileSize.x;
            zPos = startPoint.position.z;
        }

    }

    //------------TODO----------- implement rotation on hiddenGrid
    private void InitTiles(Transform startPoint, ushort rows, ushort columns, Vector2 tileSize, GameObject prefab)
    {
        Quaternion rotation = this.hiddenGrid.transform.rotation;
        Vector3 scale = new Vector3(tileSize.x, prefab.transform.localScale.y, tileSize.y);

        //Start at center of the startpoint
        Vector2 tileCoords;
        Vector3 tileCenter;
        float xPos = startPoint.position.x;
        float zPos = startPoint.position.z;

        for (ushort i = 0; i < rows; i++)
        {
            for (ushort j = 0; j < columns; j++)
            {
                tileCoords = new Vector2(xPos, zPos);

                //Check if Path or Map
                tileCenter = new Vector3(tileCoords.x + tileSize.x / 2f, startPoint.position.y, tileCoords.y + tileSize.y / 2f);
                Vector3Int realCoords = hiddenGrid.WorldToCell(tileCenter);
                Vector2 coords = new Vector2(realCoords.x, -realCoords.y);

                if (!this.Tiles.ContainsKey(coords))
                    this.Tiles.Add(coords, new Tile(TileType.NONE, tileCenter, rotation, scale, prefab, this.tilesHolder.transform));
                zPos += tileSize.y;
            }

            xPos += tileSize.x;
            zPos = startPoint.position.z;
        }
    }

    public Vector2 GetTileCoords(Vector3 pointInWorld)
    {
        if (this.startPoint.position.x - pointInWorld.x <= this.tileSize.x)
            pointInWorld.x += this.tileSize.x;
        if (this.startPoint.position.z - pointInWorld.z <= this.tileSize.y)
            pointInWorld.z += this.tileSize.y;
        Vector3Int realCoords = this.hiddenGrid.WorldToCell(pointInWorld);
        Vector2 tileCoords = new Vector2(realCoords.x - 1, -realCoords.y - 1);

        return tileCoords;        //NOT SURE IT SHOULD BE MINUS Y
    }
    public Vector2 GetTileCoords(TileInfo tileInfo)
    {
        Vector2 coords = new Vector2();

        Vector3Int realCoords = this.hiddenGrid.WorldToCell(new Vector3(this.startPoint.position.x + tileInfo.Row * this.tileSize.x + this.tileSize.x / 2f, this.hiddenGrid.transform.position.y, this.startPoint.position.z + tileInfo.Column * this.tileSize.y + this.tileSize.y / 2f));

        //coords.x = this.startPoint.position.x + tileInfo.Row * this.tileSize.x;
        //coords.y = this.startPoint.position.z + tileInfo.Column * this.tileSize.y;

        coords.x = realCoords.x;
        coords.y = -realCoords.y;

        return coords;
    }
    public Vector2 GetTileCoords(Vector2 row_column)
    {
        return new Vector2(this.startPoint.position.x + (row_column.x * this.tileSize.x), this.startPoint.position.z + (row_column.y * this.tileSize.y));
    }

    //--------------TODO-------------- Implement grid rotation
    public TileInfo GetRowColumn(Vector2 coords)
    {
        TileInfo tileInfo = null;

        //Test if tile in grid
        if (IsTileInGrid(coords))
        {
            tileInfo = new TileInfo();

            //Find starting tile coord
            Vector2 startPointTile = GetTileCoords(this.startPoint.position);
            Vector2 rowColumn = new Vector2((coords.x - startPointTile.x) - 1, (coords.y - startPointTile.y) - 1);
            Vector2 startTileCoord = GetTileCoords(this.startPoint.position);

            //Get distance between the two coords
            ushort row = (ushort)((coords.x - startTileCoord.x) / this.tileSize.x);
            ushort column = (ushort)((coords.y - startTileCoord.y) / this.tileSize.y);

            tileInfo.Row = (ushort)rowColumn.x;
            tileInfo.Column = (ushort)rowColumn.y;
        }

        return tileInfo;
    }

    public Vector3 GetTileCenter(Vector2 tileCoords)
    {
        return this.Tiles[tileCoords].TileCenter;
        //return new Vector3(tileCoords.x * this.tileSize.x + this.tileSize.x / 2f, this.startPoint.position.y, tileCoords.y * this.tileSize.y - this.tileSize.y / 2f);       // + 1 because needed a quick fix
    }
    public Vector3 GetTileCenterFixed(Vector2 tileCoords)
    {
        return new Vector3(tileCoords.x + this.tileSize.x / 2f, this.startPoint.position.y, (tileCoords.y + this.tileSize.y / 2f));
    }

    public TileType GetTileType(Vector2 tileCoords)
    {
        TileType type = TileType.NONE;
        if (Tiles.ContainsKey(tileCoords))
            type = this.Tiles[tileCoords].Type;
        return type;
    }

    public bool IsTileInGrid(Vector2 coords)
    {
        bool tileIsInGrid = false;

        if (this.Tiles.ContainsKey(coords))
            tileIsInGrid = true;

        return tileIsInGrid;
    }
    public bool IsTileInGrid(TileInfo tileInfo)
    {
        bool tileIsInGrid = false;

        if (this.Tiles.ContainsKey(GetTileCoords(tileInfo)))
            tileIsInGrid = true;

        return tileIsInGrid;
    }

    public void SetHitBoxActive(bool active)
    {
        this.isHitBoxActive = active;
    }

    private ulong GetNextGridId()
    {
        nextGridId++;
        return nextGridId;
    }
}