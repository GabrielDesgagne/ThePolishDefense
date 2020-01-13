using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodManager : MonoBehaviour
{
    private GameObject bowLeft;
    private GameObject bowRight;
    public GameObject bowPrefab;

    public bool leftHandHaveBow;

    public bool rightHandHaveBow;
    void Update()
    {
        if (leftHandHaveBow)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) == 0)
            {
                Destroy(bowLeft.gameObject);
                bowLeft = null;
                leftHandHaveBow = false;
            }
        }
        if (rightHandHaveBow)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0)
            {
                rightHandHaveBow = false;
                Destroy(bowRight.gameObject);
                bowRight = null;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LeftHand") && leftHandHaveBow == false)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) != 0)
            {
                bowLeft = Instantiate(bowPrefab, GameObject.FindGameObjectWithTag("LeftHand").transform);
                bowLeft.GetComponent<BowManager>().isLeft = true;
                leftHandHaveBow = true;
            }
        }
        else if (other.CompareTag("RightHand") && rightHandHaveBow == false)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) != 0)
            {
                bowRight = Instantiate(bowPrefab, GameObject.FindGameObjectWithTag("RightHand").transform);
                bowRight.GetComponent<BowManager>().isLeft = false;
                rightHandHaveBow = true;
            }
        }
    }
}
