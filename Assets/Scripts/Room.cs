using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : Flow
{

    #region Singleton
    static private Room instance = null;

    static public Room Instance
    {
        get {
            return instance ?? (instance = new Room());
        }
    }
    #endregion

    PlayerManager playerManager;
    GrabbableManager grabbableManager;
    ShopManager shopManager;
    GridManager gridManager;
    TimeManager timeManager;

    override public void PreInitialize()
    {
        //Grab instances
        playerManager = PlayerManager.Instance;
        grabbableManager = GrabbableManager.Instance;
        shopManager = ShopManager.Instance;
        gridManager = GridManager.Instance;
        timeManager = TimeManager.Instance;

        //Setup Variables

        //First Initialize
        gridManager.PreInitialize();
        playerManager.PreInitialize();
        grabbableManager.PreInitialize();
        shopManager.PreInitialize();
        timeManager.PreInitialize();
    }

    override public void Initialize()
    {
        gridManager.Initialize();
        playerManager.Initialize();
        grabbableManager.Initialize();
        shopManager.Initialize();
        timeManager.Initialize();
    }

    override public void Refresh()
    {
        gridManager.Refresh();
        playerManager.Refresh();
        grabbableManager.Refresh();
        shopManager.Refresh();
        timeManager.Refresh();
    }

    override public void PhysicsRefresh()
    {
        gridManager.PhysicsRefresh();
        playerManager.PhysicsRefresh();
        grabbableManager.PhysicsRefresh();
        shopManager.PhysicsRefresh();
        timeManager.PhysicsRefresh();
    }

    override public void EndFlow()
    {
        //GameObject.Destroy(roomSetup);
    }
}
