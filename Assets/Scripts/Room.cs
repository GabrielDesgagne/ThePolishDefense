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

    TimeManager timeManager;

    InputManager inputManager;
    GrabbableManager grabbableManager;

    ShopManager shopManager;

    PodManager podManager;
    ArrowManager arrowManager;

    AmbianceManager ambianceManager;

    private GameVariables gameVariables;
    private RoomVariables roomVariables;
    private bool sceneEnded;

    public Dictionary<GameObject, IGrabbable> roomGrabbablesDict;

    override public void PreInitialize()
    {
        //Grab instances
        playerManager = PlayerManager.Instance;

        timeManager = TimeManager.Instance;

        inputManager = InputManager.Instance;
        grabbableManager = GrabbableManager.Instance;

        shopManager = ShopManager.Instance;

        podManager = PodManager.Instance;
        arrowManager = ArrowManager.Instance;

        //Setup Variables
        roomGrabbablesDict = new Dictionary<GameObject, IGrabbable>();
        sceneEnded = false;
        ambianceManager = AmbianceManager.Instance;

        //First Initialize
        playerManager.PreInitialize();

        timeManager.PreInitialize();

        inputManager.PreInitialize();
        grabbableManager.PreInitialize();

        shopManager.PreInitialize();

        podManager.PreInitialize();
        arrowManager.PreInitialize();


        PreInitializeRoom();
    }

    override public void Initialize()
    {
        playerManager.Initialize();

        timeManager.Initialize();

        inputManager.Initialize();
        grabbableManager.Initialize();

        shopManager.Initialize();

        podManager.Initialize();
        arrowManager.Initialize();
        ambianceManager.Initialize();

        //Setup Variables
        gameVariables = GameVariables.instance;
        roomVariables = RoomVariables.instance;

        InitializeRoom();
    }

    override public void Refresh()
    {
        if (!sceneEnded) {
            playerManager.Refresh();

            timeManager.Refresh();

            inputManager.Refresh();
            grabbableManager.Refresh();

            shopManager.Refresh();

            podManager.Refresh();
            arrowManager.Refresh();
        }
    }

    override public void PhysicsRefresh()
    {
        if (!sceneEnded) {
            playerManager.PhysicsRefresh();

            timeManager.PhysicsRefresh();

            inputManager.PhysicsRefresh();
            grabbableManager.PhysicsRefresh();

            shopManager.PhysicsRefresh();

            podManager.PhysicsRefresh();
            arrowManager.PhysicsRefresh();
        }
    }

    override public void EndFlow()
    {
        sceneEnded = true;

        //playerManager.EndFlow();

        //timeManager.EndFlow();

        //inputManager.EndFlow();
        //grabbableManager.EndFlow();

        shopManager.EndFlow();

       // podManager.EndFlow();
       // arrowManager.EndFlow();

        DestroyRoomVariables();
    }

    private void PreInitializeRoom() {

    }

    private void InitializeRoom() {
        ambianceManager.playSoundsRoom();
    }

    private void DestroyRoomVariables() {
        
    }
}
