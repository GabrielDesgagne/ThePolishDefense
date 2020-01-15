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
    ArrowManager arrowManager;
    PodManager podManager;

    override public void PreInitialize()
    {
        //Grab instances
        playerManager = PlayerManager.Instance;
        boardManager = BoardManager.Instance;
        grabbableManager = GrabbableManager.Instance;
        arrowManager = ArrowManager.Instance;
        podManager = PodManager.Instance;

        //Setup Variables

        //First Initialize
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
        arrowManager.Refresh();
        podManager.Refresh();
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
        //GameObject.Destroy(roomSetup);
    }
}
