using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : Flow
{
    private static AmbianceManager instance = null;
    public static AmbianceManager Instance { get { return instance ?? (instance = new AmbianceManager()); } }
    AudioClip fireSound;
    AudioClip musicRoom;

    GameObject fireplace;
    GameObject bgMusicRoom;


    public override void PreInitialize()
    {
        fireplace = new GameObject();
        fireplace.AddComponent<AudioSource>();
        fireSound = Resources.Load<AudioClip>("SFX/RoomSound/Fireplace1");
        bgMusicRoom = new GameObject();
        bgMusicRoom.AddComponent<AudioSource>();
        
        base.PreInitialize();
    }
    public override void Initialize()
    {
        base.Initialize();
        playSoundsRoom();
    }
    public override void Refresh()
    {
        base.Refresh();
    }
    public override void PhysicsRefresh()
    {
        base.PhysicsRefresh();
    }
    public override void EndFlow()
    {
        base.EndFlow();
    }

    public void playSoundsRoom()
    {
        fireplace.GetComponent<AudioSource>().clip = fireSound;
        fireplace.GetComponent<AudioSource>().Play();
        // music.Play();
        Debug.Log("hello");
    }

}
