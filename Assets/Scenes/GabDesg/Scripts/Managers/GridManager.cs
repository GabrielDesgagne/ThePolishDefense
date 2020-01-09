using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager {

    #region Singleton
    private static GridManager instance;
    public static GridManager Instance {
        get {
            return instance ?? (instance = new GridManager());
        }
    }
    #endregion

    public GameVariables gameVariables;

    private GridEntity map;
    private GridEntity shop;

    public void Initialise() {
        this.gameVariables = GameObject.Find("GameLogic").GetComponent<GameVariables>();
        this.map = new GridEntity("Map", GridType.MAP, new Vector3(0, 0, 0), 15, 15, new Vector2(10, 10));
        this.shop = new GridEntity("Shop", GridType.SHOP, new Vector3(-50, 0, 0), 2, 8, new Vector2(10, 10));
    }

    public void Refresh() {

    }

    public void PhysicRefresh() {

    }

}
