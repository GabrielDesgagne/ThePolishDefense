using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : Flow
{
    private static AmbianceManager instance = null;
    public static AmbianceManager Instance { get { return instance ?? (instance = new AmbianceManager()); } }
    [Header("Room audioClip")]
    AudioClip fireSound;
    AudioClip musicRoom;
    AudioClip mapMusic;

    [Header("Room audioSource")]
    GameObject soundEffects;
    GameObject bgMusic;



    public override void PreInitialize()
    {
        // prepares sounds for the room
        soundEffects = new GameObject();
        bgMusic = new GameObject();
        bgMusic.name = "bgMusic";
        soundEffects.name = "soundEffects";
        soundEffects.AddComponent<AudioSource>();
        bgMusic.AddComponent<AudioSource>();
        fireSound = Resources.Load<AudioClip>("SFX/RoomSound/Fireplace1");

        musicRoom = Resources.Load<AudioClip>("SFX/Soundtrack/Wild_Boars_Inn");

        //prepares sounds for the map
        mapMusic = Resources.Load<AudioClip>("SFX/Soundtrack/fight");


        base.PreInitialize();
    }
    public override void Initialize()
    {
        base.Initialize();
         playMapMusic();
        //playSoundsRoom();
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
        soundEffects.GetComponent<AudioSource>().clip = fireSound;
        soundEffects.GetComponent<AudioSource>().volume = 0.04f;
        soundEffects.GetComponent<AudioSource>().Play();
        bgMusic.GetComponent<AudioSource>().clip = musicRoom;
        bgMusic.GetComponent<AudioSource>().volume = 0.04f;
        bgMusic.GetComponent<AudioSource>().Play();
    }

    public void playMapMusic()
    {
        bgMusic.GetComponent<AudioSource>().clip = mapMusic;
        bgMusic.GetComponent<AudioSource>().volume = 0.04f;
        bgMusic.GetComponent<AudioSource>().Play();
    }

}
