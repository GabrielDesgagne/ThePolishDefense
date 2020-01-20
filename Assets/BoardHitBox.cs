using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHitBox : GrabbableObject
{

    HandType handType;
    bool value;
    RaycastHit ray;

    override public void OnPointExit(Grabber grabber)
    {
        ShopManager.Instance.OnExitBoard(handType);
    }

    override public void OnPointEnter(Grabber grabber)
    {
        handType = grabber.handside == Hand.Handside.Left ? HandType.LEFT : HandType.RIGHT;
    }

    public override void Pointed(bool value, Grabber grabber, RaycastHit ray)
    {
        if(ray.point == Vector3.zero) { return; }
        base.Pointed(value, grabber, ray);
        //Debug.Log("Nom du fucking colider : " + ray.collider.name + "Pos du point muthafucka : " + ray.point);
        Debug.DrawLine(grabber.transform.position, ray.point, Color.red);
        ShopManager.Instance.OnEnterBoard(handType, ray.point);
    }
}
