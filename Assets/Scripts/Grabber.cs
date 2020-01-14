using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using OVRTouchSample;


//Extension de la classe Hand. 
public class Grabber : MonoBehaviour
{
    public InputManager.InputPkg inputPkg;
    
    [Header("Hand Vars")]
    public Hand hand;
    public Animator handAnimator;
    public HandPose handPose;//What is this?

    public OVRInput.Controller controller;
    private InteractObject curentPointed;
    private GameObject currentPointedObject;
    private GrabbableObject currentGrabbable;
    private GameObject currentGrabbableObject;
    private Collider[] handCollider;
    public const float THRESH_COLLISION_FLEX = 0.9f;

    //Sketch
    List<Renderer> m_showAfterInputFocusAcquired;

    public float grabberRadius;
    static private bool m_collisionEnabled = true;
    private bool m_restoreOnInputAcquired = false;

    static private float m_collisionScaleCurrent = 0.0f;

    public const float COLLIDER_SCALE_MIN = 0.01f;
    public const float COLLIDER_SCALE_MAX = 1.0f;
    public const float COLLIDER_SCALE_PER_SECOND = 1.0f;

    private Ray ray;
    private void Start()
    {
        //Sanity Checks
        if (!handAnimator) { Debug.LogError("No Hand animator in Grabber Script."); return; }
        if (!handPose) { Debug.LogError("No Hand pose in Grabber Script."); return; }
        //Instantiation
        inputPkg = InputManager.Instance.inputs;
        hand = new Hand(handAnimator, handPose, this);

        //Vars
        currentPointedObject = null;
        curentPointed = null;
        currentGrabbableObject = null;

        //Get/Set
        handCollider = this.GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        CollisionEnable(false);

        //Events
        OVRManager.InputFocusAcquired += OnInputFocusAcquired;
        OVRManager.InputFocusLost += OnInputFocusLost;
    }

    private void Update()
    {
        float flex = inputPkg.Touch[controller].HandTrigger;

        //TODO variable Grabber grabbedObject
        bool collisionEnabled = GetGrabbedObject() == null && flex >= THRESH_COLLISION_FLEX;
        CollisionEnable(collisionEnabled);
    }

    public GameObject GetGrabbedObject()
    {
        return currentGrabbableObject;
    }
    private void FixedUpdate()
    {
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