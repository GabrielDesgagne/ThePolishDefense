using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private OVRInput.Controller controllerUseToTp;
    private bool teleportBegin;
    public GameObject teleportZonePrefab;
    public LineRenderer lineRender;
    public GameObject tpZone;
    public float offSet;
    
    public PlayerStats playerStat;
    //public MainPlayerController playerController;
    public Grabber LeftHand;
    public Grabber RightHand;
  //  public HeadInfo Head { get; set; }
    //Use this range for distance Grab
    public float range = 1;
    public void PreInitialize()
    {
        //playerStat = gameObject.AddComponent<PlayerStats>();
       // Head = new HeadInfo(OVRManager.tracker.GetPose());
        
        LeftHand.PreInitialize();
        RightHand.PreInitialize();
    }
    public void Initialize()
    {
        LeftHand.Initialize();
        RightHand.Initialize();
        //playerController.Initialize();
    }

    public void Refresh()
    {
        LeftHand.Refresh();
        RightHand.Refresh();
        //playerController.Refresh();
    }

    public void PhysicsRefresh()
    {
        
        if (InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].ButtonOne)
        {
            controllerUseToTp = OVRInput.Controller.LTouch;
        }
        else if(InputManager.Instance.inputs.Touch[OVRInput.Controller.RTouch].ButtonOne)
        {
            controllerUseToTp = OVRInput.Controller.RTouch;
        }
        else
        {
            controllerUseToTp = 0;
            teleportZonePrefab.SetActive(false);
        }
        if (controllerUseToTp != 0)
        {
            beginTeleport();
        }
    }

    public void EndFlow()
    {

    }

    private void beginTeleport()
    {
        RaycastHit bob;
        if (controllerUseToTp == OVRInput.Controller.LTouch)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(LeftHand.transform.position, LeftHand.transform.forward, out rayHit, 100,1 << 10))
            {
                //lineRender.gameObject.SetActive(true);
                //lineRender.SetPosition(0, LeftHand.transform.position);
                //lineRender.SetPosition(1, rayHit.point);
                teleportZonePrefab.SetActive(true);
                Vector3 bob2 = new Vector3(rayHit.point.x, rayHit.point.y + offSet, rayHit.point.z);
                teleportZonePrefab.transform.position = bob2;

            }
            else
            {
                lineRender.gameObject.SetActive(false);
                teleportZonePrefab.SetActive(false);
            }
            Debug.DrawRay(LeftHand.transform.position, LeftHand.transform.forward, Color.red);
        }
        //teleportZonePrefab.SetActive(true);
    }
   /* public class HeadInfo
    {
        public HeadInfo(OVRPose headInfo)
        {

        }


    }*/

}
