using OVRTouchSample;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Extension de la classe Hand. 
public class Grabber : Hand
{
    private Rigidbody rb;
    private Collider[] handCollider;
    private Collider playerCollider;
    private GrabbableObject pointedObject;
    private List<Renderer> m_showAfterInputFocusAcquired;

    public Transform IndexTransform;
    public float grabberRadius;
    private bool m_collisionEnabled = true;

    static private float m_collisionScaleCurrent = 0.0f;
    public const float COLLIDER_SCALE_MIN = 0.01f;
    public const float COLLIDER_SCALE_MAX = 1.0f;
    public const float COLLIDER_SCALE_PER_SECOND = 1.0f;
    public const float DISTANCE_GRAB_RANGE_MIN = 0.0f;
    public const float THRESH_COLLISION_FLEX = 0.9f;


    //Values of input Hand Trigger that is consider a grab
    public float grabBegin = 0.55f;
    public float grabEnd = 0.35f;


    bool alreadyUpdated = false;

    // Child/attached transforms of the grabber, indicating where to snap held objects to (if you snap them).
    // Also used for ranking grab targets in case of multiple candidates.
    [SerializeField]
    protected Transform m_gripTransform = null;
    // Child/attached Colliders to detect candidate grabbable objects.
    [SerializeField]
    protected Collider[] m_grabVolumes = null;

    [SerializeField]
    protected Transform m_parentTransform;

    [SerializeField]
    protected GameObject m_player;
    [SerializeField]
    protected GameObject headAnchor;
    Renderer[] renderers;
    protected bool m_grabVolumeEnabled = true;
    protected Vector3 m_lastPos;
    protected Quaternion m_lastRot;
    protected Quaternion m_anchorOffsetRotation;
    protected Vector3 m_anchorOffsetPosition;
    protected float m_prevFlex;
    public GrabbableObject grabbedObj = null;
    public Vector3 m_grabbedObjectPosOff;
    public Quaternion m_grabbedObjectRotOff;
    protected List<GrabbableObject> grabCandidates;


    /// The currently grabbed object.
    public GrabbableObject GrabbedObject
    {
        get { return grabbedObj; }
    }

    public void ForceRelease(GrabbableObject grabbable)
    {
        bool canRelease = (
            (grabbedObj != null) &&
            (grabbedObj == grabbable)
        );
        if (canRelease)
        {
            GrabEnd();
        }
    }

    public virtual void PreInitialize()
    {
        grabCandidates = new List<GrabbableObject>();
        m_showAfterInputFocusAcquired = new List<Renderer>();
    }

    override public void Initialize()
    {
        base.Initialize();

        #region Awake
        controller = (OVRInput.Controller)handside;
        inputs = InputManager.Instance.inputs.Touch;

        m_anchorOffsetPosition = transform.localPosition;
        m_anchorOffsetRotation = transform.localRotation;

        // If we are being used with an OVRCameraRig, let it drive input updates, which may come from Update or FixedUpdate.

        handCollider = this.GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        OVRCameraRig rig = transform.GetComponentInParent<OVRCameraRig>();
        renderers = GetComponentsInChildren<Renderer>();
        rb = GetComponent<Rigidbody>();
        playerCollider = m_player.GetComponent<Collider>();


        if (rig != null)
        {
            rig.UpdatedAnchors += (r) => { OnUpdatedAnchors(); };
        }


        #endregion


        m_lastPos = transform.position;
        m_lastRot = transform.rotation;

        #region Start


        //Get/Set
        CollisionEnable(false);

        //Events
        OVRManager.InputFocusAcquired += OnInputFocusAcquired;
        OVRManager.InputFocusLost += OnInputFocusLost;
        #endregion

    }

    override public void Refresh()
    {
        base.Refresh();
        alreadyUpdated = false;
        //TODO variable Grabber grabbedObject
        bool collisionEnabled = grabbedObj == null && flex >= THRESH_COLLISION_FLEX;

        CollisionEnable(collisionEnabled);
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


    public Transform FindClosestGrabPoint(GrabbableObject grabbable)
    {
        float closestDistance = float.MaxValue;
        Transform closestGrabbableTransform = null;
        for (int j = 0; j < grabbable.GrabPoints.Length; ++j)
        {
            Transform grabbableSnap = grabbable.GrabPoints[j];
            // Store the closest grabbable
            float distance = Vector3.Distance(m_gripTransform.position, grabbableSnap.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGrabbableTransform = grabbableSnap;
            }
        }
        return closestGrabbableTransform;
    }

    public void DistanceGrabBegin()
    {
        if (pointedObject)
        {
            Debug.Log("distance grab begin");

            //Check if the interact object is also a grabbable Object
            if (!Main.Instance.grabbableObjects.ContainsKey(pointedObject.gameObject)) return;

            //Check if the gameObject wants to be distance grabbed
            if (Main.Instance.grabbableObjects[pointedObject.transform.gameObject].DistanceGrab != true) return;

            //Check if the gameObject is not in the distance grab range
            if (Vector3.Distance(headAnchor.transform.position, pointedObject.transform.position) < DISTANCE_GRAB_RANGE_MIN) return;

            //Check if the gameObject is too far
            if (Vector3.Distance(headAnchor.transform.position, pointedObject.transform.position) > Main.Instance.grabbableObjects[pointedObject.transform.gameObject].DistanceRange) return;
            //Add Object in the hand

            GrabbableObject grabbable = Main.Instance.grabbableObjects[pointedObject.transform.gameObject];
            if (grabbable.WillBeGrabbed(this))
            {
                grabbedObj = grabbable;

                //Deactivate collider in hand (not the hand collider) to prevent some overlap
                GrabVolumeEnable(false);

                //If the grabbed Object has a custom pose, use it
                currentPose = grabbedObj.CustomHandPose ?? defaultHandPose;

                Transform closestGrabbableTransform = FindClosestGrabPoint(grabbable);

                /*Calcul l'endroit ou l'object va suivre la main, en utilisant l'orientation et position d'un grab Point
                /************/

                grabbedObj.transform.rotation = Quaternion.Inverse(closestGrabbableTransform.transform.rotation) * grabbedObj.transform.rotation;

                Vector3 relPos = grabbedObj.transform.position - closestGrabbableTransform.transform.position;
                m_grabbedObjectPosOff = relPos;

                Quaternion relOri = Quaternion.Inverse(closestGrabbableTransform.transform.localRotation);
                m_grabbedObjectRotOff = relOri;

                /************/

                grabbedObj.GrabBegin(this, closestGrabbableTransform);
                SetPlayerIgnoreCollision(grabbedObj.gameObject, true);
            }
        }
    }

    /// <summary>
    /// Check if there is something colliding with the hand. If not, then calls CastRay().
    /// Used to prevent ray casting when not needed.
    /// </summary>
    public void CheckForPointedObject()
    {
        if (grabCandidates.Count != 0) return;
        CastRay();
    }

    /// <summary>
    /// Throw a raycast every frame. 
    /// </summary>
    public void CastRay()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 1000, LayerMask.GetMask("Interact"))) //||
                                                                                                                     //  Physics.SphereCast(transform.position, 0.2f, transform.forward, out rayHit, 1000, LayerMask.GetMask("Interact"))) //TODO Change layer to fit name
        {
            if (!Main.Instance.interactObjects.ContainsKey(rayHit.transform.gameObject)) return;

            //Tells the previous ponted object that it is not pointed anymore
            if (pointedObject != null)
                pointedObject.Pointed(false, this, rayHit);

            //Check if the pointed Object contains a grabbable Object
            pointedObject = Main.Instance.grabbableObjects[rayHit.transform.gameObject];

            // Will call this every frame.
            pointedObject.Pointed(true, this, rayHit);

        }
        else if (pointedObject != null)
        {
            pointedObject.Pointed(false, this, rayHit);
            pointedObject = null;
        }
    }
    // Hands follow the touch anchors by calling MovePosition each frame to reach the anchor. 
    // This is call every update by an Event in OVR Camera Rig
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
        MoveGrabbedObject(destPos, destRot);


        m_lastPos = transform.position;
        m_lastRot = transform.rotation;

        float prevFlex = m_prevFlex;
        // Update values from inputs
        m_prevFlex = inputs[controller].HandTrigger;
        CheckForPointedObject();
        CheckForGrabOrRelease(prevFlex);
    }

    void OnDestroy()
    {
        if (grabbedObj != null)
        {
            GrabEnd();
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        // Check if the collided object is a GrabbableObject.
        // If so, check if it is already a grab candidate.
        // Add the GrabbableObject to the grab Candidate.

        if (!Main.Instance.grabbableObjects.ContainsKey(otherCollider.gameObject)) return;
        if (grabCandidates.Contains(Main.Instance.grabbableObjects[otherCollider.gameObject])) return;
        grabCandidates.Add(Main.Instance.grabbableObjects[otherCollider.gameObject]);
    }

    void OnTriggerExit(Collider otherCollider)
    {
        // Check if the collided object is a GrabbableObject.
        // If so, check if it is a grab candidate.
        // Remove the GrabbableObject to the grab Candidate.
        if (!Main.Instance.grabbableObjects.ContainsKey(otherCollider.gameObject)) return;
        if (!grabCandidates.Contains(Main.Instance.grabbableObjects[otherCollider.gameObject])) return;
        grabCandidates.Remove(Main.Instance.grabbableObjects[otherCollider.gameObject]);
        SetPlayerIgnoreCollision(otherCollider.gameObject, false);
    }

    //Called each update
    protected void CheckForGrabOrRelease(float prevFlex)
    {

        //Check if the trigger just past the grab threshold to begin grabbing
        if ((m_prevFlex >= grabBegin) && (prevFlex < grabBegin))
        {
            if (grabCandidates.Count != 0)
                GrabBegin();
            else
                //CastRay();
                DistanceGrabBegin();
        }
        //Check if the trigger just past the grab threshold to end grabbing
        else if ((m_prevFlex <= grabEnd) && (prevFlex > grabEnd))
        {
            GrabEnd();
        }
    }

    protected virtual void GrabBegin()
    {
        Debug.Log("grab begin");
        float closestDistance = float.MaxValue;
        GrabbableObject closestGrabbable = null;
        Transform closestGrabbableTransform = null;
        Transform temp = null;
        // Iterate grab candidates and find the closest grabbable candidate
        foreach (GrabbableObject grabbable in grabCandidates)
        {
            bool canGrab = !(grabbable.IsGrabbed && !grabbable.AllowOffhandGrab);
            if (!canGrab)
            {
                continue;
            }

            for (int j = 0; j < grabbable.GrabPoints.Length; ++j)
            {
                Transform grabbableSnap = grabbable.GrabPoints[j];
                // Store the closest grabbable
                float distance = Vector3.Distance(m_gripTransform.position, grabbableSnap.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestGrabbable = grabbable;
                    closestGrabbableTransform = grabbableSnap;
                }
            }

        }

        if (closestGrabbable == null) return;

        #region Start Grab

        if (closestGrabbable.WillBeGrabbed(this))
        {

            // Disable grab volumes to prevent overlaps
            GrabVolumeEnable(false);

            if (closestGrabbable.IsGrabbed)
                closestGrabbable.GrabbedBy.OffhandGrabbed(closestGrabbable);


            grabbedObj = closestGrabbable;
            grabbedObj.GrabBegin(this, closestGrabbableTransform);

            currentPose = grabbedObj.CustomHandPose ?? defaultHandPose;

            m_lastPos = transform.position;
            m_lastRot = transform.rotation;

            if(closestGrabbable.UseGrabPoint)
            {
                /************/
                grabbedObj.transform.rotation = Quaternion.Inverse(closestGrabbableTransform.transform.rotation) * grabbedObj.transform.rotation;

                Vector3 relPos = grabbedObj.transform.position - closestGrabbableTransform.transform.position;
                m_grabbedObjectPosOff = relPos;

                Quaternion relOri = Quaternion.Inverse(closestGrabbableTransform.transform.localRotation);
                m_grabbedObjectRotOff = relOri;
                /************/
            }
            else
            {
                Vector3 relPos = closestGrabbableTransform.position - transform.position;
                relPos = Quaternion.Inverse(transform.rotation) * relPos;
                m_grabbedObjectPosOff = relPos;

                Quaternion relOri = Quaternion.Inverse(transform.rotation) * grabbedObj.transform.rotation;
                m_grabbedObjectRotOff = relOri;
            }


            // Note: force teleport on grab, to avoid high-speed travel to dest which hits a lot of other objects at high
            // speed and sends them flying. The grabbed object may still teleport inside of other objects, but fixing that
            // is beyond the scope of this demo.
            MoveGrabbedObject(m_lastPos, m_lastRot, true);
            SetPlayerIgnoreCollision(grabbedObj.gameObject, true);
        }

        #endregion

    }

    protected virtual void MoveGrabbedObject(Vector3 pos, Quaternion rot, bool forceTeleport = false)
    {
        if (grabbedObj == null)
        {
            return;
        }
        Rigidbody grabbedRigidbody = grabbedObj.GrabbableRigidBody;
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
        if (grabbedObj != null)
        {
            OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(controller), orientation = OVRInput.GetLocalControllerRotation(controller) };
            OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
            localPose = localPose * offsetPose;

            OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
            Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(controller);
            Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(controller);

            GrabbableRelease(linearVelocity, angularVelocity);
            currentPose = defaultHandPose;
        }

        // Re-enable grab volumes to allow overlap events
        GrabVolumeEnable(true);
    }


    protected void GrabbableRelease(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        if (grabbedObj.WillBeReleased(this))
        {
            grabbedObj.GrabEnd(linearVelocity, angularVelocity);
            grabbedObj = null;
        }
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
            grabCandidates.Clear();
        }
    }

    protected virtual void OffhandGrabbed(GrabbableObject grabbable)
    {
        if (grabbedObj == grabbable)
        {
            GrabbableRelease(Vector3.zero, Vector3.zero);
        }
    }

    protected void SetPlayerIgnoreCollision(GameObject grabbable, bool ignore)
    {

        if (playerCollider != null)
        {
            Collider[] colliders = grabbable.GetComponents<Collider>();
            foreach (Collider c in colliders)
            {
                Physics.IgnoreCollision(c, playerCollider, ignore);
            }
        }
    }

    //Activate the renderers of the hand if the hand is back in Oculus' focus
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

    //Disable the all renderer of the hand if the hand is not found by the oculus
    private void OnInputFocusLost()
    {
        if (gameObject.activeInHierarchy)
        {
            m_showAfterInputFocusAcquired.Clear();

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

//if (enabled)
//{
//    m_collisionScaleCurrent = COLLIDER_SCALE_MIN;
//    for (int i = 0; i < handCollider.Length; ++i)
//    {
//        Collider collider = handCollider[i];
//        collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
//        collider.enabled = true;
//    }
//}
//else
//{
//    m_collisionScaleCurrent = COLLIDER_SCALE_MAX;
//    for (int i = 0; i < handCollider.Length; ++i)
//    {
//        Collider collider = handCollider[i];
//        collider.enabled = false;
//        collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
//    }
//}


///*Calcul l'endroit ou l'object va suivre la main, en utilisant l'orientation et position d'un grab Point
///************/
//grabbedObj.transform.rotation = Quaternion.Inverse(closestGrabbableTransform.transform.rotation) * grabbedObj.transform.rotation;

//Vector3 relPos = grabbedObj.transform.position - closestGrabbableTransform.transform.position;
//m_grabbedObjectPosOff = relPos;

//                Quaternion relOri = Quaternion.Inverse(closestGrabbableTransform.transform.localRotation);
//m_grabbedObjectRotOff = relOri;
//                /************/