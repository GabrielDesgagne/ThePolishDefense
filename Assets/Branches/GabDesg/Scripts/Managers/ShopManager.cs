using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Flow {

    #region Singleton
    private static ShopManager instance;
    public static ShopManager Instance {
        get {
            return instance ?? (instance = new ShopManager());
        }
    }
    #endregion

    PlacingObjectManager placingObjectManager;

    public GameObject objSelected;
    public System.Enum objType;
    private bool itemWasAlreadyPlaced;


    private ShopManager() {
        //TODO add ListenForItemPickedUp to Anthony actions
        
    }

    public override void EndFlow() {
        base.EndFlow();
    }
    public override void PreInitialize() {
        base.PreInitialize();
        this.placingObjectManager = PlacingObjectManager.Instance;

        this.placingObjectManager.PreInitialize();
    }

    public override void Initialize() {
        base.Initialize();
        this.placingObjectManager.Initialize();
    }

    public override void PhysicsRefresh() {
        base.PhysicsRefresh();
        //this.placingObjectManager.PhysicsRefresh();
    }

    public override void Refresh() {
        base.Refresh();
        //this.placingObjectManager.Refresh();

        if (this.objSelected == null) {

        }
        else {
            ListenForBuyingInput();
            ListenForItemDropped();
            this.placingObjectManager.MoveObj(this.objSelected, this.objType);
        }
    }

    private void ListenForItemPickedUp(GameObject obj, System.Enum type) {
        //Figure out if the item was placed on the map

        //TODO implements objects respawn
    }

    private void ListenForItemDropped() {
        //TODO implement real inputs
        if (Input.GetButtonDown("Jump")) {
            if (this.itemWasAlreadyPlaced) {
                //Place back to where it was
            }
            else {
                //Remove from map
                RemoveObjWithAnimation(this.objSelected);

                this.objSelected = null;
            }
        }
    }

    private void ListenForBuyingInput() {
        //TODO implement real buying inputs
        if (Input.GetMouseButton(0)) {
            //Make sure tile is available
            if (this.placingObjectManager.IsObjectPlaceableThere()) {
                //Save obj in MapInfoPck
                SaveObjPositionInMapPck();
            }
        }
    }

    private void SaveObjPositionInMapPck() {

    }

    private void RemoveObjWithAnimation(GameObject obj) {
        //TODO implement animation here
        GameObject.Destroy(obj);
    }

    public void MoveSelectedObj(Vector3 newPosition) {
        this.objSelected.transform.position = newPosition;
    }

    private bool WasItemPlacedOnMap(GameObject obj) {
        return false;
    }
}
