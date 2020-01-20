﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform camToTurn;
    private OVRInput.Controller controllerUseToTp;
    private bool teleportBegin;
    public GameObject teleportZonePrefab;
    public LineRenderer lineRender;
    public GameObject tpZone;
    public float offSet;
    private bool onTeleport;
    public PlayerStats playerStat;
    //public MainPlayerController playerController;
    public Grabber LeftHand;
    public Grabber RightHand;
    Vector3 positionTp;

    private CharacterController characterController;
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
        Vector3 bob = camToTurn.localEulerAngles;
        if(InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].JoystickLeft)
            bob.y += 1f;
        else if(InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].JoystickRight)
            bob.y -= 1f;
        camToTurn.localEulerAngles = bob;
        if (InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].ButtonOne)
        {
            controllerUseToTp = OVRInput.Controller.LTouch;
            onTeleport = true;
        }
        else if(InputManager.Instance.inputs.Touch[OVRInput.Controller.RTouch].ButtonOne)
        {
            controllerUseToTp = OVRInput.Controller.RTouch;
            onTeleport = true;
        }
        else
        {
            controllerUseToTp = 0;
            teleportZonePrefab.SetActive(false);
            endTeleport();
        }

        if (onTeleport)
        {
            beginTeleport();
        }

    }

    public void EndFlow()
    {

    }

    private void beginTeleport()
    {
        Ray ray = new Ray();
        if (controllerUseToTp == OVRInput.Controller.LTouch)
        {
            ray = new Ray(LeftHand.transform.position, LeftHand.transform.forward);
        }
        else if (controllerUseToTp == OVRInput.Controller.RTouch)
        {
            ray = new Ray(RightHand.transform.position, RightHand.transform.forward);
        }

        RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100,1 << 10))
            {
                //lineRender.gameObject.SetActive(true);
                //lineRender.SetPosition(0, LeftHand.transform.position);
                //lineRender.SetPosition(1, rayHit.point);
                positionTp = rayHit.point;
                teleportZonePrefab.SetActive(true);
                positionTp.y += offSet;
                teleportZonePrefab.transform.position = positionTp;

            }
            else
            {
                //lineRender.gameObject.SetActive(false);
                teleportZonePrefab.SetActive(false);
            }
            Debug.DrawRay(LeftHand.transform.position, LeftHand.transform.forward, Color.red);
        
        //teleportZonePrefab.SetActive(true);
    }

    private void endTeleport()
    {
        if (onTeleport && positionTp != Vector3.zero)
        {
            onTeleport = false;
            transform.position = positionTp;
            positionTp = Vector3.zero;
        }
    }
   /* public class HeadInfo
    {
        public HeadInfo(OVRPose headInfo)
        {

        }


    }*/

}
