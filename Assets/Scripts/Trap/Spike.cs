using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Trap
{
    public override void PreInitialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }

    public override void PhysicsRefresh()
    {
        throw new System.NotImplementedException();
    }

    public override void onAction()
    {
        throw new System.NotImplementedException();
    }

    public override void onExitTrigger()
    {
        throw new System.NotImplementedException();
    }

    public override void onRemove()
    {
        throw new System.NotImplementedException();
    }

    public override void onTrigger()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerEnter(Collider other)
    {

        Debug.Log("do i trigger ?");
        isInTrap = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        throw new System.NotImplementedException();
        isInTrap = false;
    }

    protected override void OnTriggerStay(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
