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
    public Dictionary<Vector2, TowerType> TileTowerInfos { get; private set; } = new Dictionary<Vector2, TowerType>();
    public Dictionary<Vector2, TrapType> TileTrapInfos { get; private set; } = new Dictionary<Vector2, TrapType>();


    private MapInfoPck() {
        this.gameVariables = GameObject.Find("MainEntry").GetComponent<GameVariables>();
        SetBoundsGrid(this.gameVariables.mapGridWidth, this.gameVariables.mapGridHeight);
    }

    private void SetBoundsGrid(ushort width, ushort height) {
        this.GridWidth = width;
        this.GridHeight = height;
    }

    private void DeleteItemIfExist(Vector2 tileCoords) {
        if (this.TileTowerInfos.ContainsKey(tileCoords))
            this.TileTowerInfos.Remove(tileCoords);
        else if (this.TileTrapInfos.ContainsKey(tileCoords))
            this.TileTrapInfos.Remove(tileCoords);
    }

    public void AddTower(Vector2 tileCoord, TowerType type) {
        DeleteItemIfExist(tileCoord);
        this.TileTowerInfos.Add(tileCoord, type);
    }
    public void AddTrap(Vector2 tileCoord, TrapType type) {
        DeleteItemIfExist(tileCoord);
        this.TileTrapInfos.Add(tileCoord, type);
    }
}
