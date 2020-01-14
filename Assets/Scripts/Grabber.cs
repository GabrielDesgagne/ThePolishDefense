using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Extension de la classe Hand. 
public class Grabber : OVRTouchSample.Hand
{
    [SerializeField] public float Range;
    public OVRInput.Controller controller;
    private InteractObject curentPointed;
    private GameObject currentPointedObject;
    private GrabbableObject currentGrabbable;
    private GameObject currentGrabbableObject;
    public Vector3 GetPosition;
    public Vector3 GetVelocity;
    public Vector3 GetAngularVelocity;
    public Vector3 GetAcceleration;
    public Vector3 GetAngularAcceleration;
    public Quaternion GetRotation;
    private Ray ray;
    private void Start()
    {
        base.Start();
        m_controller = controller;
        currentPointedObject = null;
        curentPointed = null;
        currentGrabbableObject = null;
    }
    private void Update()
    {
        base.Update();
        
    }
    private void FixedUpdate()
    {
        if(!currentGrabbableObject)
            castRay();
        else
        {

            if(Vector3.Distance(currentGrabbable.transform.position, transform.position) < 0.1f)
            {
            }
            else
            {
                currentGrabbable.MoveToHand(transform.position);

            }
        }
    }
    private void LateUpdate()
    {
        base.LateUpdate();
    }
    public void castRay()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
        RaycastHit rayHit;
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out rayHit, Range, 1 << 8)) //TODO Change layer to fit name
        {

            if (currentPointedObject != rayHit.transform.gameObject)
            {
                if(curentPointed != null) curentPointed.Pointed = false;
                currentPointedObject = rayHit.transform.gameObject;
                curentPointed = rayHit.transform.GetComponent<InteractObject>();
                if(curentPointed)
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
        

        Debug.DrawLine(transform.position, transform.position + (transform.forward * Range), Color.red);
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