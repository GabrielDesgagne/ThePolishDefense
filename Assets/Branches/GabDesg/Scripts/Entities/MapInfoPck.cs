using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoPck
{
    //Moves only the infos on the map (not any other grid)

    #region Singleton
    private MapInfoPck() { }
    private static MapInfoPck instance;
    public static MapInfoPck Instance
    {
        get
        {
            return instance ?? (instance = new MapInfoPck());
        }
    }
    #endregion

    public Dictionary<Vector2, TowerType> TileTowerInfos { get; private set; } = new Dictionary<Vector2, TowerType>();
    public Dictionary<Vector2, TrapType> TileTrapInfos { get; private set; } = new Dictionary<Vector2, TrapType>();




    public void AddTower(Vector2 tileCoord, TowerType type)
    {
        if (this.TileTrapInfos.ContainsKey(tileCoord))
            Debug.Log("Tile coords is already saved... U shouldnt be able to place an item here (FIX CODE)");
        else
            this.TileTowerInfos.Add(tileCoord, type);
    }
    public void AddTrap(Vector2 tileCoord, TrapType type)
    {
        if (this.TileTrapInfos.ContainsKey(tileCoord))
            Debug.Log("Tile coords is already saved... U shouldnt be able to place an item here (FIX CODE)");
        else
            this.TileTrapInfos.Add(tileCoord, type);
    }

    public void Reset()
    {
        this.TileTowerInfos.Clear();
        this.TileTrapInfos.Clear();
    }

    public void TestPopulate()
    {
        AddTower(new Vector2(2, 2), TowerType.HEAVY);
        AddTower(new Vector2(6, 6), TowerType.HEAVY);
        AddTower(new Vector2(2, 6), TowerType.ICE);
        AddTower(new Vector2(8, 9), TowerType.ICE);
        AddTower(new Vector2(4, 8), TowerType.BASIC);
        AddTower(new Vector2(2, 10), TowerType.BASIC);
        //AddTrap(new Vector2(9, 5), TrapType.SPIKE);
        AddTrap(new Vector2(9, 5), TrapType.MINE);
    }
}
