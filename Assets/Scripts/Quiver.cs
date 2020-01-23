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
            if (!ArrowManager.Instance.currentArrowRight)
            {
                if (PodManager.Instance.bowRightHand == null)
                {
                    ArrowManager.Instance.AttachArrowToHand(Instantiate(arrowPrefab, hand.parent.parent.parent.transform).GetComponent<Arrow>(), true);
                }
            }
        }
        else
        {
            if (!ArrowManager.Instance.currentArrowLeft)
            {
                if (PodManager.Instance.bowLeftHand == null)
                {
                    ArrowManager.Instance.AttachArrowToHand(
                    Instantiate(arrowPrefab, hand.parent.parent.parent.transform).GetComponent<Arrow>(), false);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LeftHand")
        {
            if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
            {
                CreateArrow(false, other.transform);
            }
        }
        else if (other.tag == "RightHand")
        {
            if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                CreateArrow(true, other.transform);
            }
        }
    }
}
