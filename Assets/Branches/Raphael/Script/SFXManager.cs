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

    public void PlaySound(string name)
    {
        if (name == "skyrim")
        {
            sfx.skyrim.Play();
        }
        else if (name == "attack by assassin")
        {
            sfx.attackByAssassin.Play();
        }
        else if (name == "grab1")
        {
            sfx.grab1.Play();
        }
        else if (name == "grab2")
        {
            sfx.grab2.Play();
        }
    }
}
