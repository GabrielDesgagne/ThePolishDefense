using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BowManager : MonoBehaviour
{
    private OVRGrabbable hand;
    public Vector3 rotOffSet;
    private float yHandOffset = 20;
    Vector3 rotation;
    // Start is called before the first frame update
    private void Awake()
    {
        rotation = Vector3.zero;
        hand = GetComponent<OVRGrabbable>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 temp;
        if (hand.grabbedBy != null)
        {
            if (rotation == Vector3.zero)
            {
                Vector3 rotation =  transform.GetChild(0).transform.localEulerAngles;
            }
            temp = rotation;
            temp += rotOffSet;
            if(hand.grabbedBy.m_controller == OVRInput.Controller.LTouch)
                temp.y -= yHandOffset;
            else
            { 
                temp.y += yHandOffset;
            }
            
            transform.GetChild(0).transform.localEulerAngles = temp;
        }
        else
        {
            rotation = Vector3.zero;
        }
    }
    
}
