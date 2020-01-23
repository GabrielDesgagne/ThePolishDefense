using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Spike : MonoBehaviour {
    //AUDIO
    public AudioClip slashAudio;

    //Particul Effect Setting
    public GameObject slashParticul;
    GameObject slashRef;

    //using Math.lerp setting
    public float minimum = -1.0F;
    public float maximum = 2.0F;
    public float speed;
    static float t = 0.01f;
    private float detonate = 1f;
    private bool isOutTrap = true;
    private float currentTime = 0;
    private bool isInTrap = false;

    //spike reference
    [SerializeField] private GameObject spikeRef;

    public void OnTriggerEnter(Collider other)
    {

    }

    public void OnTriggerExit(Collider other)
    {
        spikeRef.transform.localPosition = new Vector3(0, Mathf.Lerp(maximum, minimum, t), 0);
    }
}

