using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum TileType { PATH, MAP, SHOP }

public class Tile {
    public ushort Id { get; private set; }
    public Vector3 CenterPosition { get; private set; }

    public TileType Type { get; private set; }

    private GameObject prefab;

    private GameObject tileContour;
    private List<MeshRenderer> tileContourMeshs;

    public Tile(ushort id, Vector3 centerPosition, TileType type, GameObject _prefab) {
        this.Id = id;
        this.CenterPosition = centerPosition;
        this.Type = type;
        this.prefab = _prefab;
    }

    public void Initialize(Transform parent, Vector2 tileSize) {
        //Update prefab size
        Vector3 scale = new Vector3(tileSize.x, 0.5f, tileSize.y);
        this.prefab.transform.localScale = scale;

        //Instantiate and add to parent grid
        this.tileContour = GameObject.Instantiate<GameObject>(this.prefab);
        this.tileContour.transform.SetParent(parent);
        this.tileContour.transform.position = this.CenterPosition;
        this.tileContourMeshs = this.tileContour.GetComponentsInChildren<MeshRenderer>().ToList();
        
        //Set color depending on type
        if (this.Type == TileType.PATH)
            ChangeContourColor(new Color(255, 0, 0, 1));
        else if (this.Type == TileType.SHOP)
            ChangeContourColor(new Color(0, 0, 255, 1));
    }

    private void ChangeContourColor(Color color) {
        foreach (MeshRenderer mesh in this.tileContourMeshs) {
            mesh.material.color = color;
        }
    }
}
