using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod : MonoBehaviour
{
    public GameObject prefabBow;

    public SkinnedMeshRenderer render;
    public void activateRender()
    {
        render.enabled = true;
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            if (other.CompareTag("LeftHand"))
            {
                CreateBow(true, prefabBow, this);
                render.enabled = false;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            if (other.CompareTag("RightHand"))
            {
                CreateBow(false, prefabBow, this);
                render.enabled = false;
            }
        }
    }
    public void CreateBow(bool isLeft, GameObject prefabBow, Pod pod)
    {
        if (isLeft && PodManager.Instance.leftHandHaveBow == false)
        {
            PodManager.Instance .bowLeftHand = Instantiate(prefabBow, GameObject.FindGameObjectWithTag("LeftHand").transform);
            PodManager.Instance .leftHandHaveBow = true;
            PodManager.Instance .attachedLeftHandPod = pod;
            GetComponent<Collider>().enabled = false;
        }
        else if (!isLeft && PodManager.Instance .rightHandHaveBow == false)
        {
            PodManager.Instance .bowRightHand = Instantiate(prefabBow, GameObject.FindGameObjectWithTag("RightHand").transform);
            PodManager.Instance .rightHandHaveBow = true;
            PodManager.Instance .attachedRightHandPod = pod;
            GetComponent<Collider>().enabled = false;
        }
    }
        
}
