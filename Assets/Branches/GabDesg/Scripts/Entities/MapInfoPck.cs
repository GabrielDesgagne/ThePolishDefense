using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoPck {
    //Moves only the infos on the map (not any other grid)

    #region Singleton
    private static MapInfoPck instance;
    public static MapInfoPck Instance {
        get {
            return instance ?? (instance = new MapInfoPck());
        }
    }
    #endregion

    public GameVariables gameVariables;

    //Variables
    public ushort GridWidth { get; private set; }
    public ushort GridHeight { get; private set; }
    public Dictionary<Vector2, ItemValue> TileInfos { get; private set; }


    private MapInfoPck() {
        this.gameVariables = GameObject.Find("MainEntry").GetComponent<GameVariables>();
        SetBoundsGrid(this.gameVariables.mapGridWidth, this.gameVariables.mapGridHeight);
    }

    private void SetBoundsGrid(ushort width, ushort height) {
        this.GridWidth = width;
        this.GridHeight = height;
    }

    private void DeleteItemIfExist(Vector2 tileCoords) {
        if (this.TileInfos.ContainsKey(tileCoords))
            this.TileInfos.Remove(tileCoords);
    }

    public void SetTileInfo(Vector2 tileCoords, ItemValue itemValue) {
        DeleteItemIfExist(tileCoords);
        this.TileInfos.Add(tileCoords, itemValue);
    }
}
