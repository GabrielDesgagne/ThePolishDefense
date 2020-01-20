using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPiece : MonoBehaviour, IGrabbable {

    public TrapType currentType;
    [HideInInspector]
    public bool itemWasPlacedOnMap;
    public Vector2 positionOnMap;

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
            case TrapType.GLUE:
                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.R)) {
                    Dropped();
                }
                break;
            case TrapType.MINE:
                if (Input.GetKeyDown(KeyCode.Alpha5)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.T)) {
                    Dropped();
                }
                break;
            case TrapType.SPIKE:
                if (Input.GetKeyDown(KeyCode.Alpha6)) {
                    Grabbed();
                }
                if (Input.GetKeyDown(KeyCode.Y)) {
                    Dropped();
                }
                break;
        }

        //Get mouse position for now
        Vector3? fakePoisition;
        if (LookForHitOnTables(out fakePoisition))
            ShopManager.Instance.OnEnterBoard(HandType.LEFT, (Vector3)fakePoisition);
        else
            ShopManager.Instance.OnExitBoard(HandType.LEFT);
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
            //Debug.Log("Closest Hit : " + closestHit.collider.gameObject.name);
        }

        return tableHasBeenHit;
    }
}
