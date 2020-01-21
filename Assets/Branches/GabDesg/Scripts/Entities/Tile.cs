using UnityEngine;


public enum TileType { NONE, PATH, MAP, INACTIVE }

public class Tile
{
    private static ulong nextTileId = 0;


    public ulong Id { get; private set; }
    public TileType Type { get; private set; }
    public Vector3 TileCenter { get; private set; }
    public Quaternion TileRotation { get; private set; }
    public Vector3 TileScale { get; private set; }

    private GameObject tileContour;
    private LineRenderer meshes;

    public Tile(TileType type, Vector3 position, Quaternion rotation, Vector3 scale, GameObject prefab, Transform parent)
    {
        this.Id = GetNextTileId();
        this.Type = type;
        this.TileCenter = position;
        this.TileRotation = rotation;
        this.TileScale = scale;

        InitPrefab(prefab, parent);
        InitColor(this.Type);
    }

    private void InitPrefab(GameObject prefab, Transform parent)
    {
        this.tileContour = GameObject.Instantiate<GameObject>(prefab, parent);
        this.tileContour.transform.position = this.TileCenter;
        //this.tileContour.transform.rotation = this.TileRotation;
        this.tileContour.transform.localScale = this.TileScale;

        this.meshes = this.tileContour.GetComponentInChildren<LineRenderer>();
    }

    private void InitColor(TileType type)
    {
        switch (type)
        {
            case TileType.MAP:
                ChangeContourColor(new Color(255, 255, 255));
                break;
            case TileType.PATH:
                ChangeContourColor(new Color(255, 0, 0));
                break;
            case TileType.INACTIVE:
                this.meshes.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void ChangeContourColor(Color color)
    {
        this.meshes.startColor = color;
        this.meshes.endColor = color;
    }

    private ulong GetNextTileId()
    {
        nextTileId++;
        return nextTileId;
    }
}
