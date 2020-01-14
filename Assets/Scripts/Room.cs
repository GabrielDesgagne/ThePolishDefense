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
    BoardManager boardManager;
    GrabbableManager grabbableManager;
    ShopManager shopManager;

    override public void PreInitialize()
    {
        //Grab instances
        playerManager = PlayerManager.Instance;
        boardManager = BoardManager.Instance;
        grabbableManager = GrabbableManager.Instance;
        shopManager = ShopManager.Instance;

        //Setup Variables

        //First Initialize
        playerManager.PreInitialize();
        boardManager.PreInitialize();
        grabbableManager.PreInitialize();
        shopManager.PreInitialize();
    }

    override public void Initialize()
    {
        playerManager.Initialize();
        boardManager.Initialize();
        grabbableManager.Initialize();
        shopManager.Initialize();
    }

    override public void Refresh()
    {
        playerManager.Refresh();
        boardManager.Refresh();
        grabbableManager.Refresh();
        shopManager.Refresh();
    }

    override public void PhysicsRefresh()
    {
        playerManager.PhysicsRefresh();
        boardManager.PhysicsRefresh();
        grabbableManager.PhysicsRefresh();
        shopManager.PhysicsRefresh();
    }

    override public void EndFlow()
    {
        //GameObject.Destroy(roomSetup);
    }
}
