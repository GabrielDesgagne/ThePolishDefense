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
    public Dictionary<ushort, System.Enum> TileInfos { get; private set; }
    public KeyValuePair<GameObject, System.Enum> test;


    private MapInfoPck() {
        this.gameVariables = GameObject.Find("GameLogic").GetComponent<GameVariables>();
        SetBoundsGrid(this.gameVariables.gridWidth, this.gameVariables.gridHeight);
    }

    private void SetBoundsGrid(ushort width, ushort height) {
        this.GridWidth = width;
        this.GridHeight = height;
    }

    private void DeleteItemIfExist(ushort tileId) {
        if (this.TileInfos.ContainsKey(tileId))
            this.TileInfos.Remove(tileId);
    }

    public void SetTileInfo(ushort tileId, System.Enum itemType) {
        DeleteItemIfExist(tileId);
        this.TileInfos.Add(tileId, itemType);
    }
}
