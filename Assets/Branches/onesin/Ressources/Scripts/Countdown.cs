using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{
    
    private float startTime;
    [SerializeField]
    public float countdown = 5f;
    private float time = 0;
    [SerializeField]
    private AudioSource countdownSound;
    public AudioClip soundTimer;

    [SerializeField]
    private Text countdownTimer;

    private bool showTime=true;

    private void Start() { Initialize(); }
    public void Initialize()
    {
        startTime = countdown;
    }
    public void Deduct()
    {
       
        countdown -= Time.deltaTime;

        time += Time.deltaTime;

        if (time >= 1)
        {
            countdownSound.PlayOneShot(soundTimer);
            time = 0;
            showTime = true;
            
        }

        if (showTime)
        {
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            countdownTimer.text = string.Format("{0:00}", countdown-1);

        }
        showTime = false ;


    }
}
