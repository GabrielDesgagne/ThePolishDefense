using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    public GameObject arrowPrefab;
    // Start is called before the first frame update
    private void CreateArrow(bool right, Transform hand)
    {
        if (right)
        {
            if(!ArrowManager.Instance.currentArrowRight)
                ArrowManager.Instance.AttachArrowToHand( Instantiate(arrowPrefab, hand.parent.parent.parent.transform).GetComponent<Arrow>(), true);
        }
        else
        {
            if(!ArrowManager.Instance.currentArrowLeft)
                ArrowManager.Instance.AttachArrowToHand( Instantiate(arrowPrefab, hand.parent.parent.parent.transform).GetComponent<Arrow>(), false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftHand")
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) > 0.1f)
            {
                CreateArrow(false, other.transform);
            }
        }
        else if (other.tag == "RightHand")
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0.1f)
            {
                CreateArrow(true, other.transform);
            }
        }
    }
}
