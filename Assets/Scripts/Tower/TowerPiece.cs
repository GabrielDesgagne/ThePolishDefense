using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPiece : GrabbableObject
{

    public TowerType currentType;
    [HideInInspector] public bool itemWasPlacedOnMap;
    [HideInInspector] public Vector2 positionOnMap;

    HandType handType;

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