using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodManager : Flow
{
    public GameObject bowLeftHand;
    public GameObject bowRightHand;
    public GameObject bowPrefab;
    public Pod attachedLeftHandPod;
    public Pod attachedRightHandPod;
    public bool leftHandHaveBow;

    public bool rightHandHaveBow;
    #region Singleton
    static private PodManager instance = null;

    static public PodManager Instance
    {
        get {
            return instance ?? (instance = new PodManager());
        }
    }

    #endregion

    public override void Refresh()
    {
        if (leftHandHaveBow)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) < 0.2f)
            {
                attachedLeftHandPod.activateRender();
                attachedLeftHandPod.GetComponent<Collider>().enabled = true;
                bowLeftHand.GetComponent<BowManager>().DestroyBow();
                bowLeftHand = null;
                leftHandHaveBow = false;
                
            }
        }
        if (rightHandHaveBow)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) < 0.2f)
            {
                attachedRightHandPod.activateRender();
                attachedRightHandPod.GetComponent<Collider>().enabled = true;
                rightHandHaveBow = false;
                bowRightHand.GetComponent<BowManager>().DestroyBow();
                bowRightHand = null;
            }
        }
    }
}
