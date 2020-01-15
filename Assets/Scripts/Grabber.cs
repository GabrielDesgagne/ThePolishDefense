using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum Handside
{
    Left = OVRPlugin.Controller.LTouch,
    Right = OVRPlugin.Controller.RTouch
}
//Extension de la classe Hand. 
public class Grabber : MonoBehaviour
{
    public Dictionary<OVRInput.Controller, TouchController> inputs;

    [Header("Hand Vars")]
    [SerializeField] private Handside handside;
    public Hand hand;
    public Animator handAnimator;
    public HandPose handPose;//What is this?
    private Rigidbody rb;
    [HideInInspector] public OVRInput.Controller controller;
    private InteractObject curentPointed;
    private GameObject currentPointedObject;
    private GrabbableObject currentGrabbable;
    private GameObject currentGrabbableObject;
    private Collider[] handCollider;
    public const float THRESH_COLLISION_FLEX = 0.9f;

    //Sketch
    List<Renderer> m_showAfterInputFocusAcquired;

    public float grabberRadius;
    private bool m_collisionEnabled = true;
    private bool m_restoreOnInputAcquired = false;

    static private float m_collisionScaleCurrent = 0.0f;

    public const float COLLIDER_SCALE_MIN = 0.01f;
    public const float COLLIDER_SCALE_MAX = 1.0f;
    public const float COLLIDER_SCALE_PER_SECOND = 1.0f;

    private Ray ray;

    #region OVRGrabber
    //Values of input Hand Trigger that is consider a grab
    public float grabBegin = 0.55f;
    public float grabEnd = 0.35f;


    bool alreadyUpdated = false;

    // Demonstrates parenting the held object to the hand's transform when grabbed.
    // When false, the grabbed object is moved every FixedUpdate using MovePosition.
    // Note that MovePosition is required for proper physics simulation. If you set this to true, you can
    // easily observe broken physics simulation by, for example, moving the bottom cube of a stacked
    // tower and noting a complete loss of friction.
    [SerializeField]
    protected bool m_parentHeldObject = false;

    // Child/attached transforms of the grabber, indicating where to snap held objects to (if you snap them).
    // Also used for ranking grab targets in case of multiple candidates.
    [SerializeField]
    protected Transform m_gripTransform = null;
    // Child/attached Colliders to detect candidate grabbable objects.
    [SerializeField]
    protected Collider[] m_grabVolumes = null;

    // Should be OVRInput.Controller.LTouch or OVRInput.Controller.RTouch.
    [SerializeField]
    protected Transform m_parentTransform;

    [SerializeField]
    protected GameObject m_player;

    protected bool m_grabVolumeEnabled = true;
    protected Vector3 m_lastPos;
    protected Quaternion m_lastRot;
    protected Quaternion m_anchorOffsetRotation;
    protected Vector3 m_anchorOffsetPosition;
    protected float m_prevFlex;
    public GrabbableObject m_grabbedObj = null;
    protected Vector3 m_grabbedObjectPosOff;
    protected Quaternion m_grabbedObjectRotOff;
    protected Dictionary<GrabbableObject, int> m_grabCandidates = new Dictionary<GrabbableObject, int>();
    protected bool m_operatingWithoutOVRCameraRig = true;

    /// <summary>
    /// The currently grabbed object.
    /// </summary>
    public GrabbableObject grabbedObject
    {
        get { return m_grabbedObj; }
    }

    public void ForceRelease(GrabbableObject grabbable)
    {
        bool canRelease = (
            (m_grabbedObj != null) &&
            (m_grabbedObj == grabbable)
        );
        if (canRelease)
        {
            GrabEnd();
        }
    }

    protected virtual void Awake()
    {
        

}


protected virtual void Start()
    {
        #region Awake
        controller = (OVRInput.Controller)handside;
        inputs = InputManager.Instance.inputs.Touch;
        handCollider = this.GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();

        m_anchorOffsetPosition = transform.localPosition;
        m_anchorOffsetRotation = transform.localRotation;
        // If we are being used with an OVRCameraRig, let it drive input updates, which may come from Update or FixedUpdate.
        OVRCameraRig rig = transform.GetComponentInParent<OVRCameraRig>();
        if (rig != null)
        {
            rig.UpdatedAnchors += (r) => { OnUpdatedAnchors(); };
            m_operatingWithoutOVRCameraRig = false;
        }
        rb = GetComponent<Rigidbody>();
        #endregion


        m_lastPos = transform.position;
        m_lastRot = transform.rotation;
        if (m_parentTransform == null)
        {
            if (gameObject.transform.parent != null)
            {
                m_parentTransform = gameObject.transform.parent.transform;
            }
            else
            {
                m_parentTransform = new GameObject().transform;
                m_parentTransform.position = Vector3.zero;
                m_parentTransform.rotation = Quaternion.identity;
            }
        }
        #region Start
        //Sanity Checks
        if (!handAnimator) { Debug.LogError("No Hand animator in Grabber Script."); return; }
        if (!handPose) { Debug.LogError("No Hand pose in Grabber Script."); return; }
        //Instantiation
        hand = new Hand(handAnimator, handPose, this);

        //Vars
        currentPointedObject = null;
        curentPointed = null;
        currentGrabbableObject = null;

        //Get/Set
        //CollisionEnable(false);

        //Events
        OVRManager.InputFocusAcquired += OnInputFocusAcquired;
        OVRManager.InputFocusLost += OnInputFocusLost;
        #endregion
    }

    public void Update()
    {
        alreadyUpdated = false;
        float flex = inputs[controller].HandTrigger;

        //TODO variable Grabber grabbedObject
        bool collisionEnabled = GetGrabbedObject() == null && flex >= THRESH_COLLISION_FLEX;
        CollisionEnable(collisionEnabled);
    }

    virtual public void FixedUpdate()
    {
        if (m_operatingWithoutOVRCameraRig)
        {
            OnUpdatedAnchors();
        }

        #region MAYBE TO CHANGE
        if (!currentGrabbableObject)
            castRay();
        else
        {

            if (Vector3.Distance(currentGrabbable.transform.position, transform.position) < 0.1f)
            {
            }
            else
            {
                currentGrabbable.MoveToHand(transform.position);

            }
        }
        #endregion
    }

    // Hands follow the touch anchors by calling MovePosition each frame to reach the anchor.
    // This is done instead of parenting to achieve workable physics. If you don't require physics on
    // your hands or held objects, you may wish to switch to parenting.
    void OnUpdatedAnchors()
    {
        // Don't want to MovePosition multiple times in a frame, as it causes high judder in conjunction
        // with the hand position prediction in the runtime.
        if (alreadyUpdated) return;
        alreadyUpdated = true;

        Vector3 destPos = m_parentTransform.TransformPoint(m_anchorOffsetPosition);
        Quaternion destRot = m_parentTransform.rotation * m_anchorOffsetRotation;

        rb.MovePosition(destPos);
        rb.MoveRotation(destRot);

        if (!m_parentHeldObject)
        {
            MoveGrabbedObject(destPos, destRot);
        }

        m_lastPos = transform.position;
        m_lastRot = transform.rotation;

        float prevFlex = m_prevFlex;
        // Update values from inputs
        m_prevFlex = inputs[controller].HandTrigger;

        CheckForGrabOrRelease(prevFlex);
    }

    void OnDestroy()
    {
        if (m_grabbedObj != null)
        {
            GrabEnd();
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        // Get the grab trigger
        GrabbableObject grabbable = otherCollider.GetComponent<GrabbableObject>() ?? otherCollider.GetComponentInParent<GrabbableObject>();
        if (grabbable == null) return;

        // Add the grabbable
        int refCount = 0;
        m_grabCandidates.TryGetValue(grabbable, out refCount);
        m_grabCandidates[grabbable] = refCount + 1;
    }

    void OnTriggerExit(Collider otherCollider)
    {
        GrabbableObject grabbable = otherCollider.GetComponent<GrabbableObject>() ?? otherCollider.GetComponentInParent<GrabbableObject>();
        if (grabbable == null) return;

        // Remove the grabbable
        int refCount = 0;
        bool found = m_grabCandidates.TryGetValue(grabbable, out refCount);
        if (!found)
        {
            return;
        }

        if (refCount > 1)
        {
            m_grabCandidates[grabbable] = refCount - 1;
        }
        else
        {
            m_grabCandidates.Remove(grabbable);
        }
    }

    protected void CheckForGrabOrRelease(float prevFlex)
    {
        if ((m_prevFlex >= grabBegin) && (prevFlex < grabBegin))
        {
            GrabBegin();
        }
        else if ((m_prevFlex <= grabEnd) && (prevFlex > grabEnd))
        {
            GrabEnd();
        }
    }

    protected virtual void GrabBegin()
    {
        float closestMagSq = float.MaxValue;
        GrabbableObject closestGrabbable = null;
        Collider closestGrabbableCollider = null;

        // Iterate grab candidates and find the closest grabbable candidate
        foreach (GrabbableObject grabbable in m_grabCandidates.Keys)
        {
            bool canGrab = !(grabbable.isGrabbed && !grabbable.allowOffhandGrab);
            if (!canGrab)
            {
                continue;
            }

            for (int j = 0; j < grabbable.grabPoints.Length; ++j)
            {
                Collider grabbableCollider = grabbable.grabPoints[j];
                // Store the closest grabbable
                Vector3 closestPointOnBounds = grabbableCollider.ClosestPointOnBounds(m_gripTransform.position);
                float grabbableMagSq = (m_gripTransform.position - closestPointOnBounds).sqrMagnitude;
                if (grabbableMagSq < closestMagSq)
                {
                    closestMagSq = grabbableMagSq;
                    closestGrabbable = grabbable;
                    closestGrabbableCollider = grabbableCollider;
                }
            }
        }

        // Disable grab volumes to prevent overlaps
        GrabVolumeEnable(false);

        if (closestGrabbable != null)
        {
            //if (closestGrabbable.isGrabbed)
            //{
            //    closestGrabbable.grabbedBy.OffhandGrabbed(closestGrabbable);
            //}

            m_grabbedObj = closestGrabbable;
            m_grabbedObj.GrabBegin(this, closestGrabbableCollider);

            m_lastPos = transform.position;
            m_lastRot = transform.rotation;

            // Set up offsets for grabbed object desired position relative to hand.
            if (m_grabbedObj.snapPosition)
            {
                m_grabbedObjectPosOff = m_gripTransform.localPosition;
                if (m_grabbedObj.snapOffset)
                {
                    Vector3 snapOffset = m_grabbedObj.snapOffset.position;
                    if (controller == OVRInput.Controller.LTouch) snapOffset.x = -snapOffset.x;
                    m_grabbedObjectPosOff += snapOffset;
                }
            }
            else
            {
                Vector3 relPos = m_grabbedObj.transform.position - transform.position;
                relPos = Quaternion.Inverse(transform.rotation) * relPos;
                m_grabbedObjectPosOff = relPos;
            }

            if (m_grabbedObj.snapOrientation)
            {
                m_grabbedObjectRotOff = m_gripTransform.localRotation;
                if (m_grabbedObj.snapOffset)
                {
                    m_grabbedObjectRotOff = m_grabbedObj.snapOffset.rotation * m_grabbedObjectRotOff;
                }
            }
            else
            {
                Quaternion relOri = Quaternion.Inverse(transform.rotation) * m_grabbedObj.transform.rotation;
                m_grabbedObjectRotOff = relOri;
            }

            // Note: force teleport on grab, to avoid high-speed travel to dest which hits a lot of other objects at high
            // speed and sends them flying. The grabbed object may still teleport inside of other objects, but fixing that
            // is beyond the scope of this demo.
            MoveGrabbedObject(m_lastPos, m_lastRot, true);
            SetPlayerIgnoreCollision(m_grabbedObj.gameObject, true);
            if (m_parentHeldObject)
            {
                m_grabbedObj.transform.parent = transform;
            }

        }


    }

    protected virtual void MoveGrabbedObject(Vector3 pos, Quaternion rot, bool forceTeleport = false)
    {
        if (m_grabbedObj == null)
        {
            return;
        }

        Rigidbody grabbedRigidbody = m_grabbedObj.grabbedRigidbody;
        Vector3 grabbablePosition = pos + rot * m_grabbedObjectPosOff;
        Quaternion grabbableRotation = rot * m_grabbedObjectRotOff;

        if (forceTeleport)
        {
            grabbedRigidbody.transform.position = grabbablePosition;
            grabbedRigidbody.transform.rotation = grabbableRotation;
        }
        else
        {
            grabbedRigidbody.MovePosition(grabbablePosition);
            grabbedRigidbody.MoveRotation(grabbableRotation);
        }
    }

    protected void GrabEnd()
    {
        if (m_grabbedObj != null)
        {
            OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(controller), orientation = OVRInput.GetLocalControllerRotation(controller) };
            OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
            localPose = localPose * offsetPose;

            OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
            Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(controller);
            Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(controller);

            GrabbableRelease(linearVelocity, angularVelocity);
        }

        // Re-enable grab volumes to allow overlap events
        GrabVolumeEnable(true);
    }

    protected void GrabbableRelease(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        m_grabbedObj.GrabEnd(linearVelocity, angularVelocity);
        if (m_parentHeldObject) m_grabbedObj.transform.parent = null;
        SetPlayerIgnoreCollision(m_grabbedObj.gameObject, false);
        m_grabbedObj = null;
    }

    protected virtual void GrabVolumeEnable(bool enabled)
    {
        if (m_grabVolumeEnabled == enabled)
        {
            return;
        }

        m_grabVolumeEnabled = enabled;
        for (int i = 0; i < m_grabVolumes.Length; ++i)
        {
            Collider grabVolume = m_grabVolumes[i];
            grabVolume.enabled = m_grabVolumeEnabled;
        }

        if (!m_grabVolumeEnabled)
        {
            m_grabCandidates.Clear();
        }
    }

    //protected virtual void OffhandGrabbed(GrabbableObject grabbable)
    //   {
    //       if (m_grabbedObj == grabbable)
    //       {
    //           GrabbableRelease(Vector3.zero, Vector3.zero);
    //       }
    //   }

    protected void SetPlayerIgnoreCollision(GameObject grabbable, bool ignore)
    {
        if (m_player != null)
        {
            Collider playerCollider = m_player.GetComponent<Collider>();
            if (playerCollider != null)
            {
                Collider[] colliders = grabbable.GetComponents<Collider>();
                foreach (Collider c in colliders)
                {
                    Physics.IgnoreCollision(c, playerCollider, ignore);
                }
            }
        }
    }
    #endregion





    public GameObject GetGrabbedObject()
    {
        return currentGrabbableObject;
    }

    private void UpdateCapTouchStates()
    {
        //TODO Near touch inputPkg
        //Pointing is always false. Because no implementation in Grabber.
        //Same thing for GivingThumbsUp
        //m_isPointing = !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, m_controller);
        //m_isGivingThumbsUp = !OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, m_controller);

        //inputPkg.Touch[controller].IndexTrigger;
    }

    private void OnInputFocusAcquired()
    {
        if (m_restoreOnInputAcquired)
        {
            for (int i = 0; i < m_showAfterInputFocusAcquired.Count; ++i)
            {
                if (m_showAfterInputFocusAcquired[i])
                {
                    m_showAfterInputFocusAcquired[i].enabled = true;
                }
            }
            m_showAfterInputFocusAcquired.Clear();

            // Update function will update this flag appropriately. Do not set it to a potentially incorrect value here.
            //CollisionEnable(true);

            m_restoreOnInputAcquired = false;
        }
    }
    protected void LateUpdate()
    {
        // Hand's collision grows over a short amount of time on enable, rather than snapping to on, to help somewhat with interpenetration issues.
        if (m_collisionEnabled && m_collisionScaleCurrent + Mathf.Epsilon < COLLIDER_SCALE_MAX)
        {
            m_collisionScaleCurrent = Mathf.Min(COLLIDER_SCALE_MAX, m_collisionScaleCurrent + Time.deltaTime * COLLIDER_SCALE_PER_SECOND);
            for (int i = 0; i < handCollider.Length; ++i)
            {
                Collider collider = handCollider[i];
                collider.transform.localScale = new Vector3(m_collisionScaleCurrent, m_collisionScaleCurrent, m_collisionScaleCurrent);
            }
        }
    }

    public void castRay()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
        RaycastHit rayHit;
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out rayHit, grabberRadius, 1 << 8)) //TODO Change layer to fit name
        {

            if (currentPointedObject != rayHit.transform.gameObject)
            {
                if (curentPointed != null) curentPointed.Pointed = false;
                currentPointedObject = rayHit.transform.gameObject;
                curentPointed = rayHit.transform.GetComponent<InteractObject>();
                if (curentPointed)
                    curentPointed.Pointed = true;
            }
            if (InputManager.Instance.inputs.Touch[controller].HandTrigger > 0.5f)
            {
                currentGrabbable = rayHit.transform.GetComponent<GrabbableObject>();
                currentGrabbableObject = rayHit.transform.gameObject;
                currentGrabbable.Selected = true;
            }


        }
        else if (curentPointed)
        {
            curentPointed.Pointed = false;
            currentPointedObject = null;
            curentPointed = null;
        }


        Debug.DrawLine(transform.position, transform.position + (transform.forward * grabberRadius), Color.red);
    }
    public void CollisionEnable(bool enabled)
    {
        if (m_collisionEnabled == enabled)
        {
            return;
        }
        m_collisionEnabled = enabled;

        if (enabled)
        {
            m_collisionScaleCurrent = COLLIDER_SCALE_MIN;
            for (int i = 0; i < handCollider.Length; ++i)
            {
                Collider collider = handCollider[i];
                collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
                collider.enabled = true;
            }
        }
        else
        {
            m_collisionScaleCurrent = COLLIDER_SCALE_MAX;
            for (int i = 0; i < handCollider.Length; ++i)
            {
                Collider collider = handCollider[i];
                collider.enabled = false;
                collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
            }
        }
    }

    private void OnInputFocusLost()
    {
        if (gameObject.activeInHierarchy)
        {
            m_showAfterInputFocusAcquired.Clear();
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; ++i)
            {
                if (renderers[i].enabled)
                {
                    renderers[i].enabled = false;
                    m_showAfterInputFocusAcquired.Add(renderers[i]);
                }
            }

            CollisionEnable(false);

            m_restoreOnInputAcquired = true;
        }
    }
}






//public Grabber UpdateHand()
//{
//    //Position = OVRInput.GetLocalControllerPosition(controller);
//    //Velocity = OVRInput.GetLocalControllerVelocity(controller);
//    //AngularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
//    //Acceleration = OVRInput.GetLocalControllerAcceleration(controller);
//    //AngularAcceleration = OVRInput.GetLocalControllerAngularAcceleration(controller);
//    //Rotation = OVRInput.GetLocalControllerRotation(controller);
//    //return this;
//}

//public Vector3 GetPosition;
//public Vector3 GetVelocity;
//public Vector3 GetAngularVelocity;
//public Vector3 GetAcceleration;
//public Vector3 GetAngularAcceleration;
//public Quaternion GetRotation;