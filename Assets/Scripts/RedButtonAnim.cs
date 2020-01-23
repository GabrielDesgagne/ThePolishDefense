using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonAnim : MonoBehaviour
{
    public Transform beginButton;
    public Transform toMove;
    public Transform buttonBegin;
    private Vector3 tmpPos;
    private float maxPressedButton;
    private float maxDistance;
    
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        tmpPos = beginButton.localPosition;
        
        tmpPos.y -= Vector3.Distance(other.transform.position, beginButton.position);
        float pourcent = (100 * Vector3.Distance(other.transform.position, beginButton.position)) / 0.1f;
        float push = (1.5f * pourcent) / 100f;
        Debug.Log(pourcent);
        Vector3 bob = new Vector3(toMove.localPosition.x,(buttonBegin.localPosition.y - 0.7f) - push, toMove.localPosition.z);
        if(pourcent <= 100)
            toMove.localPosition = bob;
        if (pourcent >= 90f)
        {
            Main.Instance.ChangeCurrentFlow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 bob = new Vector3(toMove.localPosition.x,(buttonBegin.localPosition.y - 1f), toMove.localPosition.z);
        toMove.localPosition = bob;

    }
}
