using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStats playerStat;
    MainPlayerController playerController;
    public Grabber LeftHand { get; set; }
    public Grabber RightHand { get; set; }
    public HeadInfo Head { get; set; }
    //Use this range for distance Grab
    [SerializeField] private float range;
    public float Range { get; set; }
    public Player()
    {
        playerStat = new PlayerStats();
        Head = new HeadInfo(OVRManager.tracker.GetPose());
        LeftHand = new Grabber();
        RightHand = new Grabber();
    }

    public void PreInitialize()
    {
        LeftHand.PreInitialize();
        RightHand.PreInitialize();
    }
    public void Initialize()
    {
        LeftHand.Initialize();
        RightHand.Initialize();
        playerController.Initialize();
    }

    public void Refresh()
    {
        LeftHand.Refresh();
        RightHand.Refresh();
        playerController.Refresh();
    }

    public void PhysicsRefresh()
    {
    }

    public void EndFlow()
    {

    }
 
    
    public class HeadInfo
    {
        public HeadInfo(OVRPose headInfo)
        {

        }


    }



    void Update()
    {

    }

    

}
