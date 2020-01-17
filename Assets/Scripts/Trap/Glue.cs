using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : Trap
{
    public Glue(GameObject gameObject)
    {
        this.prefab = gameObject;
    }

    public override void Initialize()
    {
    }

    public override void PreInitialize()
    {
    }

    public override void PhysicsRefresh()
    {
    }

    public override void Refresh()
    {
    }

    protected override void OnTriggerEnter(Collider other)
    {
    }

    protected override void OnTriggerExit(Collider other)
    {
    }

    protected override void OnTriggerStay(Collider other)
    {
    }

    public override void onAction()
    {
    }

    public override void onExitTrigger()
    {
    }

    public override void onRemove()
    {
    }

    public override void onTrigger()
    {
    }
}
