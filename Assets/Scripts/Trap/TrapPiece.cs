using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPiece : GrabbableObject {

    public TrapType currentType;
    [HideInInspector] public bool itemWasPlacedOnMap;
    [HideInInspector] public Vector2 positionOnMap;

    HandType handType;

    override public void Grabbed(Grabber hand) {
        handType = hand.handside == Hand.Handside.Left ? HandType.LEFT : HandType.RIGHT;
        ShopManager.Instance.ObjGrabbed(gameObject, handType, this);
    }

    override public void Released(Vector3 linearVelocity, Vector3 angularVelocity) {
        ShopManager.Instance.ObjDropped(handType);
    }

}
