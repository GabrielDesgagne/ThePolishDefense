using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] SFXVariables sfx;

    public void StopSoundtrack1()
    {
        sfx.attackByAssassin.Stop();
    }
    public void StopSoundtrack2()
    {
        sfx.skyrim.Stop();
       
    }

    public void Soundtrack(string name)
    {
        if (name == "skyrim")
        {
            sfx.skyrim.Play();
        }
        else if (name == "attack by assassin")
        {
            sfx.attackByAssassin.Play();
        }
        
    }

    public void PlayerSound(string name)
    {
        if (name == "grab1")
        {
            sfx.grab1.Play();
        }
        else if (name == "grab2")
        {
            sfx.grab2.Play();
        }
        else if (name == "coin1")
        {
            sfx.coin1.Play();
        }
        else if (name == "coin2")
        {
            sfx.coin2.Play();
        }
        else if (name == "coin3")
        {
            sfx.coin3.Play();
        }
        else if (name == "force1")
        {
            sfx.force1.Play();
        }
        else if (name == "moveWood")
        {
            sfx.moveWood.Play();
        }
    }
    public void BowSound(string name)
    {

    }
    public void EnemiesSound(string name)
    {

    }
    public void RoomSound(string name)
    {

    }
    public void TowerSound(string name)
    {

    }
    public void WaveEndSound(string name)
    {

    }


}
