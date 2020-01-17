using OVRTouchSample;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : InteractObject
{
    /* Var in inspector */
    [SerializeField] private bool distanceGrab;
    [SerializeField] private HandPose handPose;
    [SerializeField] private Transform[] grabPoints = null;
    [SerializeField] private bool allowOffhandGrab = true;

    #region Getters
    public bool AllowOffhandGrab => allowOffhandGrab;
    public bool DistanceGrab => distanceGrab;
    public Rigidbody GrabbableRigidBody { get; private set; }
    public Collider ThisCollider { get; private set; }
    /// If true, the object is currently grabbed.
    public bool IsGrabbed => GrabbedBy != null;
    /// Returns the custom hand
    public HandPose CustomHandPose => handPose;

    /// Returns the Grabber currently grabbing this object.
    public Grabber GrabbedBy { get; private set; } = null;

    /// The transform at which this object was grabbed.
    public Transform GrabbedTransform { get; private set; }

    /// The Rigidbody of the collider that was used to grab this object.
    public Rigidbody GrabbedRigidbody => GrabbedCollider.attachedRigidbody;

    /// The contact point(s) where the object was grabbed.
    public Transform[] GrabPoints => grabPoints;

    public bool GrabbedKinematic { get; set; } = false;
    public Collider GrabbedCollider { get; set; } = null;

    #endregion

    private void Awake()
    {
        //Cache Values
        GrabbableRigidBody = GetComponent<Rigidbody>();
        ThisCollider = GetComponent<Collider>(); // Can't be null since it is required in parent class
        
        if (grabPoints.Length == 0)
        {
            // Create a default grab point
            grabPoints = new Transform[1] { transform };
        }

        gameObject.layer = LayerMask.NameToLayer("Interact");
    }
    
    /// <summary>
    /// Notifies the object that it has been grabbed.
    /// </summary>
    virtual public void GrabBegin(Grabber hand, Transform grabPoint) 
    {
        GrabbedBy = hand;
        GrabbedTransform = grabPoint;
        GrabbableRigidBody.isKinematic = true;
    }

    /// <summary>
    /// Notifies the object that it has been released.
    /// </summary>
    virtual public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        GrabbableRigidBody.isKinematic = GrabbedKinematic;
        GrabbableRigidBody.velocity = linearVelocity;
        GrabbableRigidBody.angularVelocity = angularVelocity;
        GrabbedBy = null;
        GrabbedCollider = null;
    }

    protected virtual void Start()
    {
        GrabbedKinematic = GrabbableRigidBody.isKinematic;
        Main.Instance.grabbableObjects.Add(this.gameObject, this);
    }

    void OnDestroy()
    {
        if (GrabbedBy != null)
        {
            // Notify the hand to release destroyed grabbables
            GrabbedBy.ForceRelease(this);
        }
    }
}
