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

    public void ClearLists()
    {
        this.TileTowerInfos.Clear();
        this.TileTrapInfos.Clear();
    }

    public void TestPopulate()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                //                 if (j != 14) {
                //                     if (j % 3 == 0) {
                //                         AddTower(new Vector2(i, j), TowerType.BASIC);
                //                     }
                //                     else if (j % 3 == 1) {
                //                         AddTower(new Vector2(i, j), TowerType.HEAVY);
                //                     }
                //                     else if (j % 3 == 2) {
                //                         AddTower(new Vector2(i, j), TowerType.ICE);
                //                     }
                //                 }
                //                 else {
                //                     if (j % 3 == 0) {
                //                         AddTrap(new Vector2(i, j), TrapType.GLUE);
                //                     }
                //                     else if (j % 3 == 1) {
                //                         AddTrap(new Vector2(i, j), TrapType.MINE);
                //                     }
                //                     else if (j % 3 == 2) {
                //                         AddTrap(new Vector2(i, j), TrapType.SPIKE);
                //                     }
                //                 }
            }
        }
        AddTower(new Vector2(2, 2), TowerType.HEAVY);
        AddTower(new Vector2(4, 12), TowerType.HEAVY);
        AddTower(new Vector2(6, 6), TowerType.HEAVY);
        AddTower(new Vector2(12, 8), TowerType.HEAVY);
        AddTower(new Vector2(2, 6), TowerType.ICE);
        AddTower(new Vector2(8, 9), TowerType.ICE);
        AddTower(new Vector2(4, 8), TowerType.BASIC);
        AddTower(new Vector2(2, 10), TowerType.BASIC);
    }
}
