using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{

    private float startTime;
    public float countdown = 5f;
    private float time = 0;


    private bool showTime = true;

    [SerializeField]
    private Text waveCountdownTimer;

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
            time = 0;
            showTime = true;

        }

        if (showTime)
        {
            //play beep sound
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            waveCountdownTimer.text = string.Format("{0:00.00}", Mathf.Round(countdown - 1));
        }
        showTime = false;
    }
}
