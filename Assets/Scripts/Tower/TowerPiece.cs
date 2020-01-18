using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPiece : MonoBehaviour, IGrabbable {

    public TowerType currentType;
    [HideInInspector] public bool itemWasPlacedOnMap;
    [HideInInspector] public Vector2 positionOnMap;

    public void Dropped() {
        ShopManager.Instance.ObjDropped(HandType.LEFT);
    }

    public void Grabbed() {
        ShopManager.Instance.ObjGrabbed(gameObject, HandType.LEFT, this);
    }

    public void Highlight() {

    }

    public void UnHighlight() {

    }

    private void Update() {
        switch (currentType) {
            case TowerType.BASIC:
                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.Q)) {
                    Dropped();
                }
                break;
            case TowerType.HEAVY:
                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.W)) {
                    Dropped();
                }
                break;
            case TowerType.ICE:
                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.E)) {
                    Dropped();
                }
                break;
        }

        //Get mouse position for now
        Vector3? fakePoisition;
        if (LookForHitOnTables(out fakePoisition)) {
            ShopManager.Instance.OnEnterBoard(HandType.LEFT, (Vector3)fakePoisition);
        }
        else {
            ShopManager.Instance.OnExitBoard(HandType.LEFT);
        }
    }

    public bool LookForHitOnTables(out Vector3? hitPointInWorld) {
        bool tableHasBeenHit = false;
        hitPointInWorld = null;

        //Create a ray from Camera -> Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 300, 1 << LayerMask.NameToLayer("GameBoard"));

        RaycastHit closestHit;
        if (hits.Length > 0) {
            tableHasBeenHit = true;

            closestHit = hits[0];

            if (hits.Length >= 2) {
                //Find which table has been hit
                //TODO Change Camera position to Player position
                closestHit = hits.GetClosestHit(Camera.main.transform.position);
            }

            hitPointInWorld = closestHit.point;
        }

        return tableHasBeenHit;
    }
}