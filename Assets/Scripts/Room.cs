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

    override public void PreInitialize()
    {

        playerManager = PlayerManager.Instance;
        boardManager = BoardManager.Instance;
        grabbableManager = GrabbableManager.Instance;

        playerManager.PreInitialize();
        boardManager.PreInitialize();
        grabbableManager.PreInitialize();

    }

    override public void Initialize()
    {
        playerManager.Initialize();
        boardManager.Initialize();
        grabbableManager.Initialize();
    }

    override public void Refresh()
    {
        playerManager.Refresh();
        boardManager.Refresh();
        grabbableManager.Refresh();
    }

    override public void PhysicsRefresh()
    {
        playerManager.PhysicsRefresh();
        boardManager.PhysicsRefresh();
        grabbableManager.PhysicsRefresh();
    }

    override public void EndFlow()
    {

    }

}
