﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : Flow
{
    private static AmbianceManager instance = null;
    public static AmbianceManager Instance { get { return instance ?? (instance = new AmbianceManager()); } }
    public Dictionary<soundTypes, AudioClip> sounds = new Dictionary<soundTypes, AudioClip>();
    GameObject AudioPlayerMusic;
    GameObject AudioPlayerSounds;


    public override void PreInitialize()
    {
        // prepares sounds for the room
        sounds.Add(soundTypes.RELEASE, Resources.Load<AudioClip>("SFX/BowSounds/Release1"));
        sounds.Add(soundTypes.SHOOT, Resources.Load<AudioClip>("SFX/BowSounds/Shooting1"));
        sounds.Add(soundTypes.ENEMY_ATTACK, Resources.Load<AudioClip>("SFX/EnemiesSounds/Attack1"));
        sounds.Add(soundTypes.ENEMY_DEAD, Resources.Load<AudioClip>("SFX/EnemiesSounds/dead1.output"));
        sounds.Add(soundTypes.ISHAH, Resources.Load<AudioClip>("SFX/EnemiesSounds/ishAh.output"));
        sounds.Add(soundTypes.ENEMY_MOVE, Resources.Load<AudioClip>("SFX/EnemiesSounds/Move1"));
        sounds.Add(soundTypes.OOH, Resources.Load<AudioClip>("SFX/EnemiesSounds/ooh.output"));
        sounds.Add(soundTypes.OUCH, Resources.Load<AudioClip>("SFX/EnemiesSounds/ouch.output"));
        sounds.Add(soundTypes.ENEMY_SPAWN, Resources.Load<AudioClip>("SFX/EnemiesSounds/spawn.output"));
        sounds.Add(soundTypes.COIN, Resources.Load<AudioClip>("SFX/PlayerSound/Coin1"));
        sounds.Add(soundTypes.FORCE, Resources.Load<AudioClip>("SFX/PlayerSound/Force1"));
        sounds.Add(soundTypes.GRAB, Resources.Load<AudioClip>("SFX/PlayerSound/Grab1"));
        sounds.Add(soundTypes.WALK, Resources.Load<AudioClip>("SFX/PlayerSound/Move_Wood"));
        sounds.Add(soundTypes.FIREPLACE, Resources.Load<AudioClip>("SFX/RoomSound/Fireplace1"));
        sounds.Add(soundTypes.GARBAGE, Resources.Load<AudioClip>("SFX/RoomSound/Garbage1"));
        sounds.Add(soundTypes.TILE, Resources.Load<AudioClip>("SFX/RoomSound/Tiles1"));
        sounds.Add(soundTypes.FIGHT_BG, Resources.Load<AudioClip>("SFX/Soundtrack/fight"));
        sounds.Add(soundTypes.TAVERN_BG, Resources.Load<AudioClip>("SFX/Soundtrack/tavernBg"));
        sounds.Add(soundTypes.TP, Resources.Load<AudioClip>("SFX/TeleportationSound/Teleportation1"));
        sounds.Add(soundTypes.CANNON, Resources.Load<AudioClip>("SFX/TowerSound/Cannon1"));
        sounds.Add(soundTypes.EXPLOSION, Resources.Load<AudioClip>("SFX/TowerSound/Release1"));
        sounds.Add(soundTypes.SPLASH, Resources.Load<AudioClip>("SFX/TowerSound/Splash2"));
        sounds.Add(soundTypes.POTION, Resources.Load<AudioClip>("SFX/TowerSound/PotionBreak1"));
        sounds.Add(soundTypes.TRAP_EXPLOSION, Resources.Load<AudioClip>("SFX/TrapSound/Explosion3"));
        sounds.Add(soundTypes.SPIKE, Resources.Load<AudioClip>("SFX/TrapSound/spike1"));
        sounds.Add(soundTypes.DEFENDERS, Resources.Load<AudioClip>("SFX/WaveEndSound/Defenders"));
        sounds.Add(soundTypes.FIREWORKS, Resources.Load<AudioClip>("SFX/WaveEndSound/Fireworks1"));
        sounds.Add(soundTypes.SCREAMS, Resources.Load<AudioClip>("SFX/WaveEndSound/ScreamingVillager"));
        sounds.Add(soundTypes.TRUMPET, Resources.Load<AudioClip>("SFX/WaveEndSound/Trumpet"));
        AudioPlayerMusic = new GameObject();
        AudioPlayerMusic.name = "Music";
        AudioPlayerSounds = new GameObject();
        AudioPlayerSounds.name = "Sounds";

        base.PreInitialize();
    }
    public override void Initialize()
    {
        base.Initialize();

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

    }

    public void playMapMusic()
    {

    }

}
