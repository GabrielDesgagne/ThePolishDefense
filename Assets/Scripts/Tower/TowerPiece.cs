using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPiece : GrabbableObject
{

    public TowerType currentType;
    [HideInInspector] public bool itemWasPlacedOnMap;
    [HideInInspector] public Vector2 positionOnMap;

    HandType handType;


    public void Highlight()
    {

    }

    public void UnHighlight()
    {

    }

    //     private void Update() {
    //         switch (currentType) {
    //             case TowerType.BASIC:
    //                 if (Input.GetKeyDown(KeyCode.Alpha1)) {
    //                     Grabbed();
    //                 }
    //                 if (Input.GetKeyDown(KeyCode.Q)) {
    //                     Dropped();
    //                 }
    //                 break;
    //             case TowerType.HEAVY:
    //                 if (Input.GetKeyDown(KeyCode.Alpha2)) {
    //                     Grabbed();
    //                 }
    //                 if (Input.GetKeyDown(KeyCode.W)) {
    //                     Dropped();
    //                 }
    //                 break;
    //             case TowerType.ICE:
    //                 if (Input.GetKeyDown(KeyCode.Alpha3)) {
    //                     Grabbed();
    //                 }
    //                 if (Input.GetKeyDown(KeyCode.E)) {
    //                     Dropped();
    //                 }
    //                 break;
    //         }
    // 
    //         //Get mouse position for now
    //         Vector3? fakePoisition;
    //         if (LookForHitOnTables(out fakePoisition)) {
    //             ShopManager.Instance.OnEnterBoard(HandType.LEFT, (Vector3)fakePoisition);
    //         }
    //         else {
    //             ShopManager.Instance.OnExitBoard(HandType.LEFT);
    //         }
    //     }

    public bool LookForHitOnTables(out Vector3? hitPointInWorld)
    {
        bool tableHasBeenHit = false;
        hitPointInWorld = null;

        //Create a ray from Camera -> Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 300, 1 << LayerMask.NameToLayer("GameBoard"));

        RaycastHit closestHit;
        if (hits.Length > 0)
        {
            tableHasBeenHit = true;

            closestHit = hits[0];

            if (hits.Length >= 2)
            {
                //Find which table has been hit
                //TODO Change Camera position to Player position
                closestHit = hits.GetClosestHit(Camera.main.transform.position);
            }

            hitPointInWorld = closestHit.point;
        }

        return tableHasBeenHit;
    }

    override public void GrabBegin(Grabber hand, Transform grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        handType = hand.handside == Hand.Handside.Left ? HandType.LEFT : HandType.RIGHT;
        ShopManager.Instance.ObjGrabbed(gameObject, handType, this);
    }

    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        ShopManager.Instance.ObjDropped(handType);
    }
}