﻿using System.Collections;
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
    InputManager inputManager;
    ShopManager shopManager;
    TimeManager timeManager;
    ArrowManager arrowManager;
    PodManager podManager;

    public Dictionary<GameObject, IGrabbable> roomGrabbablesDict;

    override public void PreInitialize()
    {
        //Grab instances
        inputManager = InputManager.Instance;
        playerManager = PlayerManager.Instance;
        grabbableManager = GrabbableManager.Instance;
        shopManager = ShopManager.Instance;
        timeManager = TimeManager.Instance;
        arrowManager = ArrowManager.Instance;
        podManager = PodManager.Instance;

        //Setup Variables
        roomGrabbablesDict = new Dictionary<GameObject, IGrabbable>();

        //First Initialize
        inputManager.PreInitialize();
        playerManager.PreInitialize();
        grabbableManager.PreInitialize();
        shopManager.PreInitialize();
        timeManager.PreInitialize();
    }

    override public void Initialize()
    {
        inputManager.Initialize();
        playerManager.Initialize();
        grabbableManager.Initialize();
        shopManager.Initialize();
        timeManager.Initialize();
    }

    override public void Refresh()
    {
        inputManager.Refresh();
        playerManager.Refresh();
        podManager.Refresh();
        arrowManager.Refresh();
        
        grabbableManager.Refresh();
        shopManager.Refresh();
        timeManager.Refresh();
    }

    override public void PhysicsRefresh()
    {
        inputManager.PhysicsRefresh();
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
