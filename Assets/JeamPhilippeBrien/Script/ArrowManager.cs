using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    private bool leftHand;
    public static ArrowManager Instance;
    private OVRGrabbable hand;
    private GameObject currentArrowLeft;
    private GameObject currentArrowRight;
    
    public GameObject stringAttachPoint;
    public GameObject stringStartPoint;

    public GameObject arrowPrefab;

    private Arrow currentOnBow = null;
    // Start is called before the first frame update
    void Awake() {
        if (Instance == null)
            Instance = this;
    }
    void OnDestroy() {
        if (Instance == this)
            Instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentArrowLeft != null)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) == 0)
            {
                currentArrowLeft.transform.parent = null;
                currentArrowLeft.GetComponent<Rigidbody>().useGravity = true;
                currentArrowLeft.GetComponent<Rigidbody>().isKinematic = false;
                currentArrowLeft.GetComponent<Arrow>().startDisapearTime = Time.time;
                currentArrowLeft = null;
            }
        }

        if (currentArrowRight != null)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0)
            {
                currentArrowRight.transform.parent = null;
                currentArrowRight.GetComponent<Rigidbody>().useGravity = true;
                currentArrowRight.GetComponent<Rigidbody>().isKinematic = false;
                currentArrowRight.GetComponent<Arrow>().startDisapearTime = Time.time;
                currentArrowRight = null;
            }
        }
    }
    private void AttachArrow(Transform other, bool right) {
        if (right)
        {
            if (currentArrowLeft == null) {
                currentArrowLeft = Instantiate (arrowPrefab, other.parent.parent.parent.transform);
                currentArrowLeft.transform.localPosition = new Vector3 (0f, 0f, .342f);
                currentArrowLeft.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                currentArrowLeft.GetComponent<Arrow>().isLeft = true;
                
            }
        }
        else
        {
            if (currentArrowRight == null) {
                currentArrowRight = Instantiate (arrowPrefab, other.parent.parent.parent.transform);
                currentArrowRight.transform.localPosition = new Vector3 (0f, 0f, .342f);
                currentArrowRight.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
                currentArrowRight.GetComponent<Arrow>().isLeft = false;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftHand")
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) != 0)
            {
                AttachArrow(other.transform, true);
            }
        }
        else if (other.tag == "RightHand")
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) != 0)
            {
                AttachArrow(other.transform, false);
            }
        }
    }
}
