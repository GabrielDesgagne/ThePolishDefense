using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using UnityEngine;

public class ArrowManager : Flow
{
    #region Singleton
    static private ArrowManager instance = null;

    static public ArrowManager Instance
    {
        get {
            return instance ?? (instance = new ArrowManager());
        }
    }

    #endregion
    private bool leftHand;
    private OVRGrabbable hand;
    public Arrow currentArrowLeft;
    public Arrow currentArrowRight;

    public Vector3 ArrowPos = new Vector3(0f, 0f, .342f);

    public float scale = 0.1f;
    //public GameObject arrowPrefab;
    
    // Start is called before the first frame update

    // Update is called once per frame
    public override void Refresh()
    {
        if (currentArrowLeft != null)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) == 0)
            {
                currentArrowLeft.setOffHand();
                currentArrowLeft = null;
            }
        }

        if (currentArrowRight != null)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0)
            {
                currentArrowRight.setOffHand();
                currentArrowRight = null;
            }
        }
    }
    
    public void AttachArrowToHand(Arrow arrow, bool right) {
        if (right)
        {
            if (currentArrowRight == null) {
                currentArrowRight = arrow;
                currentArrowRight.transform.localPosition = ArrowPos;
                currentArrowRight.transform.localScale = new Vector3(scale,scale,scale);

            }
        }
        else
        {
            if (currentArrowLeft == null)
            {
                currentArrowLeft = arrow;
                currentArrowLeft.transform.localPosition = ArrowPos;
                currentArrowLeft.transform.localScale = new Vector3(scale,scale,scale);
            }
        }
        
    }
}
