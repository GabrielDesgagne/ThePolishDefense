using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sound
{
    public enum Sounds
    {
        CLICK,
        BOOM,

    }

    private static Dictionary<Sounds, float> soundTimerDictionnary;

    public static void PlaySound(Sounds sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sounds sounds)
    {
        //boucler sur une list de sound
        Debug.Log("Sound" + sounds + "Not found");
        return null;
    }
}
