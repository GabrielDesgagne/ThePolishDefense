using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    }

    public void EndFlow()
    {

    }
 
    
   /* public class HeadInfo
    {
        public HeadInfo(OVRPose headInfo)
        {

        }


    }*/

}
