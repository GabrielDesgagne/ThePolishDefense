using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Trap
{
    //AUDIO
    public AudioSource slashAudio;

    //Particul Effect Setting
    public GameObject slashParticul;
    GameObject slashRef;

    public Spike(GameObject gameObject)
    {
        this.prefab = gameObject;
    }

    public override void PreInitialize()
    {
    }

    public override void Initialize()
    {
    }

    public override void Refresh()
    {
    }

    public override void PhysicsRefresh()
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

    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("do i trigger ?");
        isInTrap = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        isInTrap = false;
    }

    protected override void OnTriggerStay(Collider other)
    {
    }
}
