using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Flow {
    #region Singleton
    static private PlayerManager instance = null;

    static public PlayerManager Instance {
        get {
            return instance ?? (instance = new PlayerManager());
        }
    }

    #endregion

    public Player player;
    public bool changeScene;
    override public void PreInitialize()
    {
        changeScene = false;
        player = (GameObject.Instantiate(Resources.Load("Prefabs/Player/Player")) as GameObject).GetComponent<Player>();
        if (Main.Instance.isInRoomScene) {
            player.transform.position = player.startingRoomPos;
        }
        else {
            player.transform.position = player.startingMapPos;
            this.player.GetComponent<OVRPlayerController>().enabled = false;

        }

        Main.Instance.VRPlayerCharacter = player.gameObject;
        player.PreInitialize();
    }

    override public void Initialize() {
        //mainPlayerController.Initialize();
        player.Initialize();
    }

    override public void Refresh() {
        //mainPlayerController.Refresh();

        //TODO Remove this in build version
        //Switch Scene. 
        if (Input.GetKeyDown(KeyCode.P) || changeScene) {
            Main.Instance.ChangeCurrentFlow();
        }
        player.Refresh();
    }

    override public void PhysicsRefresh() {
        player.PhysicsRefresh();
        //mainPlayerController.PhysicsRefresh();
    }

    override public void EndFlow() {
        player.EndFlow();
        instance = null;
    }

}
