using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Flow
{
    #region Singleton
    static private PlayerManager instance = null;

    static public PlayerManager Instance
    {
        get {
            return instance ?? (instance = new PlayerManager());
        }
    }

    #endregion

    RoomPrefabsHolder roomHolder;
    
    //TODO GamePrefabsHolder

    MainPlayerController mainPlayerController;
    CharacterController characterController;

    override public void PreInitialize()
    {
        if (Main.Instance.isInRoomScene)
        {
            roomHolder = Main.Instance.roomSetupPrefab.GetComponent<RoomPrefabsHolder>();
        }
        
        mainPlayerController = roomHolder.vrPlayerCharacterPrefab.GetComponent<MainPlayerController>();
        characterController = roomHolder.vrPlayerCharacterPrefab.GetComponent<CharacterController>();

        mainPlayerController.PreInitialize();
    }

    override public void Initialize()
    {
        mainPlayerController.Initialize();
    }

    override public void Refresh()
    {
        mainPlayerController.Refresh();

        //TODO Remove this in build version
        //Switch Scene. 
        if(Input.GetKeyDown(KeyCode.P))
        {
            Main.Instance.ChangeCurrentFlow();
        }
    }

    override public void PhysicsRefresh()
    {
        //mainPlayerController.PhysicsRefresh();
    }

    override public void EndFlow()
    {
       
    }

}
