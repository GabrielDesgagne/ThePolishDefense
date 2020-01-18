using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType { LEFT, RIGHT }

public class HandShop {
    private static MapInfoPck mapPck;
    private static ShopManager shopManager;

    public HandType Type { get; private set; }

    private Vector3? hitPointOnBoard;
    private GameObject objGhost, objInHand;
    private bool onAvailableTile;

    private Vector2 tileCoordsSelected, oldTileCoordsSelected;
    private TowerPiece towerInfo;
    private TrapPiece trapInfo;

    private GameObject objHolder;






    public HandShop(HandType type) {
        if (mapPck == null)
            mapPck = MapInfoPck.Instance;
        if (shopManager == null)
            shopManager = ShopManager.Instance;
        this.Type = type;

        this.objHolder = new GameObject("Hand" + this.Type.ToString());
    }

    public void Refresh() {
        //Hand Logic
    }

    public bool GrabObj(GameObject obj, TowerPiece type) {
        bool objCanBeGrabbed = false;
        if (this.objInHand == null) {
            objCanBeGrabbed = true;
            this.objInHand = obj;
            this.towerInfo = type;

            //Create ghost item
            CreateGhostItem(type);
            Debug.Log("Object Grabbed");
        }
        else
            Debug.Log("Theres already an object in " + this.Type.ToString() + " hand...");

        return objCanBeGrabbed;
    }
    public bool GrabObj(GameObject obj, TrapPiece type) {
        bool objCanBeGrabbed = false;
        if (this.objInHand == null) {
            objCanBeGrabbed = true;
            this.objInHand = obj;
            this.trapInfo = type;

            //Create ghost item
            CreateGhostItem(type);
            Debug.Log("Object Grabbed");
        }
        else
            Debug.Log("Theres already an object in " + this.Type.ToString() + " hand...");

        return objCanBeGrabbed;
    }

    public void DropObj() {
        if (this.objInHand != null && this.objGhost != null) {
            this.objInHand = null;

            //Destroy ghost item
            GameObject.Destroy(this.objGhost);

            //Reset hand infos
            ResetHandInfo();
            Debug.Log("Object Dropped...");
        }
    }

    private void CreateGhostItem(TowerPiece type) {
        //Instantiate obj (prefab held in ShopManager)
        this.objGhost = GameObject.Instantiate<GameObject>(shopManager.towerSpawnInfo[type.currentType].objPrefab, this.objHolder.transform);
    }
    private void CreateGhostItem(TrapPiece type) {
        //Instantiate obj
        this.objGhost = GameObject.Instantiate<GameObject>(shopManager.trapSpawnInfo[type.currentType].objPrefab, this.objHolder.transform);
        this.objGhost.SetActive(false);
    }

    private void UpdateTileSelected() {
        if (this.hitPointOnBoard != null) {
            this.tileCoordsSelected = shopManager.Map.GetTileCoords((Vector3)this.hitPointOnBoard);
        }
    }

    private bool HasTileChanged() {
        bool tileHasChanged = false;

        if (this.tileCoordsSelected != this.oldTileCoordsSelected) {
            tileHasChanged = true;
            this.oldTileCoordsSelected = this.tileCoordsSelected;
        }

        return tileHasChanged;
    }

    public void BuyItem() {
        if (this.onAvailableTile) {

            if (this.towerInfo != null)
                SaveObjInPck(this.towerInfo);
            else if (this.trapInfo != null)
                SaveObjInPck(this.trapInfo);
        }
    }

    private void SaveObjInPck(TowerPiece type) {
        //Save obj
        mapPck.AddTower(this.tileCoordsSelected, type.currentType);

        //Remove obj in hand
        GameObject.Destroy(this.objInHand);

        //Reset all values
        ResetHandInfo();
    }
    private void SaveObjInPck(TrapPiece type) {
        //Save obj
        mapPck.AddTrap(this.tileCoordsSelected, type.currentType);

        //Remove obj in hand
        GameObject.Destroy(this.objInHand);

        //Reset all values
        ResetHandInfo();
    }

    public void SetHitPointOnBoard(Vector3? hitPoint) {
        this.hitPointOnBoard = hitPoint;

        if (this.objInHand != null && this.objGhost != null) {
            UpdateTileSelected();
            if (this.hitPointOnBoard == null)
                if (this.objGhost.activeSelf)
                    this.objGhost.SetActive(false);
            if (HasTileChanged() && this.hitPointOnBoard != null) {

                //Activated obj
                if (!this.objGhost.activeSelf)
                    this.objGhost.SetActive(true);

                //Move obj
                Vector3 newPosition = shopManager.Map.GetTileCenter(this.tileCoordsSelected);
                this.objGhost.transform.position = newPosition;

                //Check if item type can go on tile type
                bool objCanGoOnTileType = false;
                TileType tileType = shopManager.Map.GetTileType(this.tileCoordsSelected);
                if (this.towerInfo != null)
                    objCanGoOnTileType = ObjCanGoOnTileType(tileType, this.towerInfo.currentType);
                else if (this.trapInfo != null)
                    objCanGoOnTileType = ObjCanGoOnTileType(tileType, this.trapInfo.currentType);

                //Check if item already placed on tile
                bool tileIsEmpty = IsTileEmpty(this.tileCoordsSelected);

                if (objCanGoOnTileType && tileIsEmpty) {
                    this.onAvailableTile = true;
                }
                else {
                    this.onAvailableTile = false;
                    Debug.Log("U Cant place the item here...");
                    //CANT PLACE HERE
                }

            }
        }
    }

    private bool ObjCanGoOnTileType(TileType tileType, TowerType towerType) {
        bool objCanGoOnTileType = false;

        switch (tileType) {
            case TileType.MAP:
                objCanGoOnTileType = true;
                break;
            case TileType.NONE:
                objCanGoOnTileType = false;
                break;
            case TileType.PATH:
                objCanGoOnTileType = false;
                break;
        }

        return objCanGoOnTileType;
    }
    private bool ObjCanGoOnTileType(TileType tileType, TrapType trapType) {
        bool objCanGoOnTileType = false;

        switch (tileType) {
            case TileType.MAP:
                objCanGoOnTileType = false;
                break;
            case TileType.NONE:
                objCanGoOnTileType = false;
                break;
            case TileType.PATH:
                objCanGoOnTileType = true;
                break;
        }

        return objCanGoOnTileType;
    }
    private bool IsTileEmpty(Vector2 tileCoords) {
        bool tileIsEmpty = true;
        //Check if tile is saved in MapPck
        if (mapPck.TileTowerInfos.ContainsKey(tileCoords) || mapPck.TileTrapInfos.ContainsKey(tileCoords))
            tileIsEmpty = false;
        return tileIsEmpty;
    }
    private void ResetHandInfo() {
        this.objGhost = null;
        this.objInHand = null;
        this.towerInfo = null;
        this.trapInfo = null;
        this.onAvailableTile = false;
        this.hitPointOnBoard = null;
    }
}
