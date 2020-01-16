using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType { MAP, SHOP }

public class GridEntity {
    private static ushort nextGridId = 0;
    private static ushort nextTileId = 0;

    public GridType Type { get; private set; }

    //Keep start point to remember the Y
    public Vector3 StartPoint { get; private set; }
    private ushort gridWidth, gridHeight;
    private Vector2 tileSize;

    public Dictionary<ushort, Tile> tiles { get; private set; } = new Dictionary<ushort, Tile>();
    private GameObject tilesHolder;

    private List<Vector2> pathCorners = new List<Vector2>();

    public GridEntity(string name, GridType type, Vector3 _StartPoint, ushort width, ushort height, Vector2 _TileSize) {

        this.Type = type;
        this.StartPoint = _StartPoint;
        this.gridWidth = width;
        this.gridHeight = height;
        this.tileSize = _TileSize;

        CreateTilesHolder(name);
        InitGridHitBox(_StartPoint, _TileSize, width, height);
        if (this.Type == GridType.MAP)
            InitPath(_TileSize);
        InitTiles(StartPoint, width, height, _TileSize);
    }

    private void CreateTilesHolder(string name) {
        this.tilesHolder = new GameObject(name);
        this.tilesHolder.transform.position = new Vector3();
        this.tilesHolder.transform.SetParent(GridManager.Instance.gridsHolder.transform);
    }

    private void InitGridHitBox(Vector3 position, Vector2 tileSize, ushort width, ushort height) {
        //Instantiate prefab inside parent
        GameObject hitbox = GameObject.Instantiate<GameObject>(GridManager.Instance.hiddenGridHitBoxPrefab, this.tilesHolder.transform);

        //Set at position
        hitbox.transform.position = position;

        //Resize depending on game board width/height
        Vector3 size = new Vector3((tileSize.x * width) / 10, 0.2f, (tileSize.y * height) / 10);
        hitbox.transform.localScale = size;
    }

    private void InitPath(Vector2 tileSize) {
        //Create path here (hardcoded for now)
//         this.pathCorners.Add(new Vector2(3, 8));
//         this.pathCorners.Add(new Vector2(13, 8));
//         this.pathCorners.Add(new Vector2(8, 3));
//         this.pathCorners.Add(new Vector2(8, 13));
//         this.pathCorners.Add(new Vector2(3, 8));
//         this.pathCorners.Add(new Vector2(3, 13));
//         this.pathCorners.Add(new Vector2(13, 8));
//         this.pathCorners.Add(new Vector2(13, 3));
//         this.pathCorners.Add(new Vector2(8, 13));
//         this.pathCorners.Add(new Vector2(13, 13));
//         this.pathCorners.Add(new Vector2(8, 3));
//         this.pathCorners.Add(new Vector2(3, 3));

        this.pathCorners.Add(new Vector2(4, 1));
        this.pathCorners.Add(new Vector2(4, 10));
        this.pathCorners.Add(new Vector2(8, 10));
        this.pathCorners.Add(new Vector2(8, 4));
        this.pathCorners.Add(new Vector2(12, 4));
        this.pathCorners.Add(new Vector2(12, 15));
    }

    private void InitTiles(Vector3 startPoint, ushort width, ushort height, Vector2 tileSize) {
        //Init tile into array

        ushort tileId;
        //Start at center of the startpoint
        Vector3 tilePosition;
        float x = startPoint.x + tileSize.x / 2;
        float z = startPoint.z + tileSize.y / 2;

        for (ushort i = 0; i < width; i++) {
            for (ushort j = 0; j < height; j++) {
                tileId = GetNextTileId();
                tilePosition = new Vector3(x, StartPoint.y, z);

                //If grid isnt a shop, check what tile it is (path or terrain)
                if (this.Type == GridType.SHOP)
                    this.tiles.Add(tileId, new Tile(tileId, tilePosition, TileType.SHOP, GridManager.Instance.gridVisualSides));
                else
                    this.tiles.Add(tileId, new Tile(tileId, tilePosition, GetTileTypeAtPositionToCreatePath(tilePosition), GridManager.Instance.gridVisualSides));

                this.tiles[tileId].Initialize(this.tilesHolder.transform, tileSize);

                z += tileSize.y;
            }

            x += tileSize.x;
            z = startPoint.z + tileSize.y / 2;
        }
    }

    private TileType GetTileTypeAtPositionToCreatePath(Vector3 position) {
        //Get if its a path tile or not
        bool isPathTile = false;
        for (int i = 0; i < this.pathCorners.Count - 1; i++) {
            if (IsVectorBetweenBounds(position, this.pathCorners[i], this.pathCorners[i + 1])) {
                isPathTile = true;
                break;
            }
        }
        return isPathTile ? TileType.PATH : TileType.MAP;
    }

    private ushort GetNextTileId() {
        nextTileId++;
        return nextTileId;
    }

    private bool IsVectorBetweenBounds(Vector3 point, Vector2 a, Vector2 b) {
        //Find position of a
        Vector3 aPosition = GetPathTilePosition(a);
        //Find position of b
        Vector3 bPosition = GetPathTilePosition(b);

        //Look if point is between a and b
        if (aPosition.x == bPosition.x) {
            if (point.x == aPosition.x) {
                return IsNumberBetweenBounds(point.z, aPosition.z, bPosition.z);
            }
        }
        else if (aPosition.z == bPosition.z) {
            if (point.z == aPosition.z) {
                return IsNumberBetweenBounds(point.x, aPosition.x, bPosition.x);
            }
        }

        return false;
    }

    public Vector3 GetPathTilePosition(Vector2 vec) {
        Vector3 tilePosition = new Vector3(this.StartPoint.x + (vec.x - 1) * this.tileSize.x, 0, this.StartPoint.z + (vec.y - 1) * this.tileSize.y);
        //Get Center of the tile
        tilePosition.x += this.tileSize.x / 2;
        tilePosition.z += this.tileSize.y / 2;
        return tilePosition;
    }

    private bool IsNumberBetweenBounds(float a, float boundA, float boundB) {
        return (a >= GetSmallest(boundA, boundB) && a <= GetBiggest(boundA, boundB));
    }

    private float GetSmallest(float a, float b) {
        return a < b ? a : b;
    }
    private float GetBiggest(float a, float b) {
        return a > b ? a : b;
    }

    public bool IsTileInGrid(Vector3 tileCenter) {
        bool tileIsInGrid = false;

        //Check all tiles to see if tileCenter is found
        foreach (Tile tile in this.tiles.Values) {
            if (tile.CenterPosition == tileCenter)
                tileIsInGrid = true;
        }
        return tileIsInGrid;
    }
    public TileType? GetTileTypeAtPosition(Vector3 tileCenter) {
        TileType? type = null;

        //Check all tiles to see if tileCenter is found
        foreach (Tile tile in this.tiles.Values) {
            if (tile.CenterPosition == tileCenter)
                type = tile.Type;
        }

        return type;
    }

    public Tile GetTileAtPosition(Vector3 tileCenter) {
        Tile tileResult = null;

        //Check all tiles to see if tileCenter is found
        foreach (Tile tile in this.tiles.Values) {
            if (tile.CenterPosition == tileCenter)
                tileResult = tile;
        }

        return tileResult;
    }

    public Vector3? GetTileCenterPositionFromId(ushort id) {
        Vector3? tileCenter = null;
        if (this.tiles.ContainsKey(id))
            tileCenter = this.tiles[id].CenterPosition;
        return tileCenter;
    }

    public ushort? GetTileId(Vector3 tileCenter) {
        ushort? id = null;

        foreach (Tile tile in this.tiles.Values) {
            if (tile.CenterPosition == tileCenter)
                id = tile.Id;
        }

        return id;
    }

    public Vector2 TileIdToCoord(ushort tileId) {
        float x = tileId % this.gridWidth;
        float z = (tileId - x) / this.gridWidth;

        return new Vector2(this.StartPoint.x + (x - 1) * this.tileSize.x, this.StartPoint.z + (z - 1) * this.tileSize.y);
    }

}
