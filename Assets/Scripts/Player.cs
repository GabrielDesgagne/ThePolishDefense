using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MainPlayerController playerController;
    public Grabber LeftHand { get; set; }
    public Grabber RightHand { get; set; }
    public HeadInfo Head { get; set; }
    //Use this range for distance Grab
    [SerializeField] private float range;
    public float Range { get; set; }
    public Player()
    {
        Head = new HeadInfo(OVRManager.tracker.GetPose());
        LeftHand = new Grabber();
        RightHand = new Grabber();
    }
    public void Initialize()
    {
        playerController.Initialize();
    }

    public void Refresh()
    {
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
