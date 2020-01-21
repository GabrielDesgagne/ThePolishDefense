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
    public Arrow currentArrowLeft;
    public Arrow currentArrowRight;

    public Vector3 ArrowPos = new Vector3(0f, 0f, 0.330f);

    public float scale = 1f;
    public override void Refresh()
    {
        if (currentArrowLeft != null)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) < 0.2f)
            {
                currentArrowLeft.setOffHand();
                currentArrowLeft = null;
            }
        }

        if (currentArrowRight != null)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) < 0.2f)
            {
                currentArrowRight.setOffHand();
                currentArrowRight = null;
            }
        }
    }
    
    public void AttachArrowToHand(Arrow arrow, bool right) {
        if (right)
        {
            //if (currentArrowRight == null) {
                currentArrowRight = arrow;
                currentArrowRight.transform.localPosition = ArrowPos;
            //}
        }
        else
        {
           // if (currentArrowLeft == null)
           // {
                currentArrowLeft = arrow;
                currentArrowLeft.transform.localPosition = ArrowPos;
           // }
        }
        
    }
}
