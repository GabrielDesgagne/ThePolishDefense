using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHitBox : GrabbableObject
{

    HandType handType;
    RaycastHit ray;

    override public void OnPointExit(Grabber grabber)
    {
        ShopManager.Instance.OnExitBoard(this.handType);
    }

    override public void OnPointEnter(Grabber grabber)
    {
        handType = grabber.handside == Hand.Handside.Left ? HandType.LEFT : HandType.RIGHT;
    }

    public override void Pointed(bool value, Grabber grabber, RaycastHit ray)
    {
        if (ray.point != Vector3.zero) {
            base.Pointed(value, grabber, ray);

            ShopManager.Instance.OnEnterBoard(this.handType, ray.point);

            //Check input to buy
            if (InputManager.Instance.inputs.Touch[grabber.controller].IndexTrigger >= 0.5f) {
                ShopManager.Instance.BuyItem(this.handType);
            }
        }
    }
}
