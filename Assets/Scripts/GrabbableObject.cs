using OVRTouchSample;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrabbableObject : InteractObject
{
    /* Var in inspector */
    [SerializeField] private bool distanceGrab = true;
    [SerializeField] private HandPose handPose;
    [SerializeField] private Transform[] grabPoints = null;
    [SerializeField] private bool allowOffhandGrab = true;


    private List<Collider> colliders;
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
    public bool UseGrabPoint { get; private set; }

    /// The Rigidbody of the collider that was used to grab this object.
    public Rigidbody GrabbedRigidbody => GrabbedCollider.attachedRigidbody;

    /// The contact point(s) where the object was grabbed.
    public Transform[] GrabPoints => grabPoints;

    public bool GrabbedKinematic { get; set; } = false;
    public Collider GrabbedCollider { get; set; } = null;

    #endregion

    override protected void Awake()
    {
        base.Awake();
        //Cache Values
        GrabbableRigidBody = GetComponent<Rigidbody>();
        ThisCollider = GetComponent<Collider>(); // Can't be null since it is required in parent class
        UseGrabPoint = true;
        if (grabPoints.Length == 0)
        {
            UseGrabPoint = false;
            // Create a default grab point for the Distance Grab
            grabPoints = new Transform[1] { transform };
        }
        colliders = GetComponentsInChildren<Collider>().ToList();

    }
    //Toggles all colliders on the object. 
    public void ToggleColliders(bool enable)
    {
        foreach(Collider coll in colliders)
        {
            coll.enabled = enable;
        }
    }

    /// Notifies the object that it has been grabbed.
    public void GrabBegin(Grabber hand, Transform grabPoint) 
    {

        ToggleColliders(false);
        GrabbedBy = hand;
        GrabbedTransform = grabPoint;
        GrabbableRigidBody.isKinematic = true;

        Grabbed(hand);
    }

    /// Notifies the object that it has been released.
    public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        ToggleColliders(true);
        GrabbableRigidBody.isKinematic = GrabbedKinematic;
        GrabbableRigidBody.velocity = linearVelocity;
        GrabbableRigidBody.angularVelocity = angularVelocity;
        GrabbedBy = null;
        GrabbedCollider = null;

        Released(linearVelocity, angularVelocity);
    }

    override protected void Start()
    {
        base.Start();
        GrabbedKinematic = GrabbableRigidBody.isKinematic;
        Main.Instance.grabbableObjects.Add(this.gameObject, this);
    }

    /// <summary>
    /// Function to override. Happens before the grab occurs.
    /// Return true makes the hand grab the object. False prevents the default behaviour.
    /// </summary>
    virtual public bool WillBeGrabbed(Grabber grabber)
    {
        return true;
    }

    /// <summary>
    /// Function to override. Happens once the grab occured.
    /// </summary>
    virtual public void Grabbed(Grabber grabber)
    {

    }

    /// <summary>
    /// Function to override. Happens before the release occurs.
    /// Return true makes the hand release the object. False prevents the default behaviour.
    /// </summary>
    virtual public bool WillBeReleased(Grabber grabber)
    {
        return true;
    }


    /// <summary>
    /// Function to override. Happens once the release occured.
    /// </summary>
    virtual public void Released(Vector3 linearVelocity, Vector3 angularVelocity)
    {

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
