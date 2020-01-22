using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHitBox : InteractObject
{

    HandType handType;
    bool value;
    RaycastHit ray;

    override public void OnPointExit(Grabber grabber, RaycastHit rayInfo)
    {
        ShopManager.Instance.OnExitBoard(this.handType);
        this.value = false;
    }

    override public void OnPointEnter(Grabber grabber, RaycastHit rayInfo)
    {
        handType = grabber.handside == Hand.Handside.Left ? HandType.LEFT : HandType.RIGHT;
        this.value = true;
    }

    public override void IsPointed(Grabber grabber, RaycastHit ray)
    {
        if (ray.point != Vector3.zero) {
            ShopManager.Instance.OnEnterBoard(this.handType, ray.point);

            //Check input to buy
            if (InputManager.Instance.inputs.Touch[grabber.controller].IndexTrigger >= 0.5f) {
                ShopManager.Instance.BuyItem(this.handType);
            }
        }
    }
}
