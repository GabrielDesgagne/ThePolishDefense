using OVRTouchSample;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : InteractObject
{
    //TODO  make serialize field, and set to private. Add getters
    public bool distanceGrab;
    public bool flip;
    public Transform snap;
    public Collider thisCollider;
    public HandPose pose;
    public float distanceRange = 10;
    public Rigidbody rigidBody;

    [SerializeField]
    protected Collider[] m_grabPoints = null;
    public bool allowOffhandGrab = true;
    protected bool m_grabbedKinematic = false;
    protected Collider m_grabbedCollider = null;
    protected Grabber m_grabbedBy = null;

    #region Facebook Nightmare
    /// <summary>
    /// If true, the object is currently grabbed.
    /// </summary>
    public bool isGrabbed
    {
        get { return m_grabbedBy != null; }
    }

    /// Returns the Grabber currently grabbing this object.
    public Grabber grabbedBy
    {
        get { return m_grabbedBy; }
    }

    /// The transform at which this object was grabbed.
    public Transform grabbedTransform
    {
        get { return m_grabbedCollider.transform; }
    }

    /// The Rigidbody of the collider that was used to grab this object.
    public Rigidbody grabbedRigidbody
    {
        get { return m_grabbedCollider.attachedRigidbody; }
    }

    /// The contact point(s) where the object was grabbed.
    public Collider[] grabPoints
    {
        get { return m_grabPoints; }
    }
    #endregion

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        if (m_grabPoints.Length == 0)
        {
            // Get the collider from the grabbable
            thisCollider = this.GetComponent<Collider>();
            if (thisCollider == null)
            {
                throw new ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
            }

            // Create a default grab point
            m_grabPoints = new Collider[1] { thisCollider };
        }
        
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }
    private void Update()
    {
    }
    private bool selected;
    public bool Selected { get { return selected; }
        set
        {
            //collider.isTrigger = true;
            rigidBody.useGravity = false;
            selected = value;
        }
    }

    //public void MoveToHand(Vector3 hand)
    //{

    //    if (Selected)
    //    {
    //        transform.position = Vector3.MoveTowards(snap.position, hand, Time.deltaTime * 20); //TODO
    //    }

    //}


    virtual public void GrabBegin(Grabber hand, Collider grabPoint)
    {
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        rigidBody.isKinematic = true;
    }

    /// <summary>
    /// Notifies the object that it has been released.
    /// </summary>
    virtual public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        rigidBody.isKinematic = m_grabbedKinematic;
        rigidBody.velocity = linearVelocity;
        rigidBody.angularVelocity = angularVelocity;
        m_grabbedBy = null;
        m_grabbedCollider = null;
    }

    protected virtual void Start()
    {
        m_grabbedKinematic = rigidBody.isKinematic;
        Main.Instance.grabbableObjects.Add(this.gameObject, this);

    }

    void OnDestroy()
    {
        if (m_grabbedBy != null)
        {
            // Notify the hand to release destroyed grabbables
            m_grabbedBy.ForceRelease(this);
        }
    }
}
