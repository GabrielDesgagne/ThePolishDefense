using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : Trap
{

    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;

    //Vector3 startPosDoor1 = new Vector3(, 25, 25);
    Vector3 endPosDoor1 = new Vector3(-2.25f, 0, -2f);
    Vector3 startPosDoor2 = new Vector3(-2.25f, 0, 0);
    Vector3 endPosDoor2 = new Vector3(-2.25f, 0, 1);

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
        Debug.Log(door2.transform.localPosition);
        door1.transform.localPosition = new Vector3(-2.3f, 0.1f, -2);
        door2.transform.localPosition = new Vector3(-2.3f, 0.1f, 1);
        Debug.Log(door2.transform.localPosition);
    }

    protected override void OnTriggerExit(Collider other)
    {
        door1.transform.localPosition = new Vector3(-2.3f, 0.1f, -1.2f);
        door2.transform.localPosition = new Vector3(-2.3f, 0.1f, 0);
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
