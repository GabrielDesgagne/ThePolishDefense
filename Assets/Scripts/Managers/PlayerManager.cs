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

    public Player player;
    RoomPrefabsHolder roomHolder;
    
    //TODO GamePrefabsHolder

    //MainPlayerController mainPlayerController;
    //CharacterController characterController;

    override public void PreInitialize()
    {
        if (Main.Instance.isInRoomScene)
        {
            roomHolder = Main.Instance.RoomSetupPrefab.GetComponent<RoomPrefabsHolder>();
        }

        player = Main.Instance.VRPlayerCharacter.GetComponent<Player>();
        
        //mainPlayerController = roomHolder.vrPlayerCharacterPrefab.GetComponent<MainPlayerController>();
        //characterController = roomHolder.vrPlayerCharacterPrefab.GetComponent<CharacterController>();

        //mainPlayerController.PreInitialize();
        player.PreInitialize();
    }

    override public void Initialize()
    {
        //mainPlayerController.Initialize();
        player.Initialize();
    }

    override public void Refresh()
    {
        //mainPlayerController.Refresh();
        
        //TODO Remove this in build version
        //Switch Scene. 
        if(Input.GetKeyDown(KeyCode.P))
        {
            Main.Instance.ChangeCurrentFlow();
        }
        player.Refresh();
    }

    override public void PhysicsRefresh()
    {
        player.PhysicsRefresh();
        //mainPlayerController.PhysicsRefresh();
    }

    override public void EndFlow()
    {
       player.EndFlow();
    }

}
