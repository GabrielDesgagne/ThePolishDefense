using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType { NONE, MAP, SHOP }

public class GridEntity {
    private static ushort nextId = 1;

    public ushort Id { get; private set; }
    public GridType Type { get; private set; }

    //Keep start point to remember the Y
    public Vector3 StartPoint { get; private set; }

    private Dictionary<Vector3, Tile> tiles = new Dictionary<Vector3, Tile>();
    private List<Vector3> pathCorners = new List<Vector3>();

    private GameObject tilesHolder;

    public GridEntity(string name, GridType type, Vector3 _StartPoint, ushort width, ushort height, Vector2 _TileSize) {
        this.Id = nextId;
        nextId++;
        this.Type = type;
        this.StartPoint = _StartPoint;
        CreateTilesHolder(name);
        InitGridHitBox(_StartPoint, _TileSize, width, height);
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
        //Create path here (hardcode for now)
        //TODO make path easier with a view tool (not hardcoded)
        this.pathCorners.Add(new Vector3(3 * tileSize.x + tileSize.x / 2, 0, 0 * tileSize.y + tileSize.y / 2));
        this.pathCorners.Add(new Vector3(3 * tileSize.x + tileSize.x / 2, 0, 9 * tileSize.y + tileSize.y / 2));
        this.pathCorners.Add(new Vector3(7 * tileSize.x + tileSize.x / 2, 0, 9 * tileSize.y + tileSize.y / 2));
        this.pathCorners.Add(new Vector3(7 * tileSize.x + tileSize.x / 2, 0, 3 * tileSize.y + tileSize.y / 2));
        this.pathCorners.Add(new Vector3(11 * tileSize.x + tileSize.x / 2, 0, 3 * tileSize.y + tileSize.y / 2));
        this.pathCorners.Add(new Vector3(11 * tileSize.x + tileSize.x / 2, 0, 14 * tileSize.y + tileSize.y / 2));
    }

    private void InitTiles(Vector3 startPoint, ushort width, ushort height, Vector2 tileSize) {
        //Init tile into array

        Vector3 tilePosition;
        float x = startPoint.x + tileSize.x / 2;
        float z = startPoint.z + tileSize.y / 2;

        for (ushort i = 0; i < width; i++) {
            for (ushort j = 0; j < height; j++) {
                tilePosition = new Vector3(x, StartPoint.y, z);

                //If grid isnt a shop, check what tile it is (path or terrain)
                if (this.Type == GridType.SHOP)
                    this.tiles.Add(tilePosition, new Tile(tilePosition, TileType.SHOP, GridManager.Instance.gridVisualSides));
                else
                    this.tiles.Add(tilePosition, new Tile(tilePosition, GetTileTypeAtPositionToCreatePath(tilePosition), GridManager.Instance.gridVisualSides));

                this.tiles[tilePosition].Initialize(this.tilesHolder.transform);

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
        return isPathTile ? TileType.PATH : TileType.EMPTY;
    }

    public TileType GetTileTypeAtPosition(Vector3 position) {
        TileType type = TileType.EMPTY;

        if (IsTileInGrid(position)) {
            type = this.tiles[position].Type;
        }

        return type;
    }

    public Tile GetTileAtPosition(Vector3 position) {
        Tile tile = null;

        if (IsTileInGrid(position)) {
            tile = this.tiles[position];
        }

        return tile;
    }

    private bool IsVectorBetweenBounds(Vector3 point, Vector3 a, Vector3 b) {
        if (a.x == b.x) {
            if (point.x == a.x) {
                return IsNumberBetweenBounds(point.z, a.z, b.z);
            }
        }
        else if (a.z == b.z) {
            if (point.z == a.z) {
                return IsNumberBetweenBounds(point.x, a.x, b.x);
            }
        }

        return false;
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
        return this.tiles.ContainsKey(tileCenter);
    }
}
