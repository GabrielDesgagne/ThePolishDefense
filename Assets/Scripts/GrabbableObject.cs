using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : InteractObject
{
    //TODO  make serialize field, and set to private. Add getters
    public bool distanceGrab;
    public bool flip;
    public Transform snap;
    OVRGrabbable grabbable;
    Collider collider;
    Rigidbody rb;

    private void Awake()
    {
        if (!snap)
        {
            snap = transform;
        }
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<OVRGrabbable>();
    }
    private void Update()
    {

    }
    private bool selected;
    public bool Selected { get { return selected; }
        set
        {
            collider.isTrigger = true;
            rb.useGravity = false;
            selected = value;
        }
    }

    private float startTime;
    public void MoveToHand(Vector3 hand)
    {

        if (Selected)
        {
            transform.position = Vector3.MoveTowards(snap.position, hand, Time.deltaTime * 20); //TODO
        }

    }
    public void grabBegin(OVRGrabber grabber)
    {
        grabbable.GrabBegin(grabber, collider);
    }
}
