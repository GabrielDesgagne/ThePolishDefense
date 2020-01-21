using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RoundOver : MonoBehaviour
{
   
   
    public SFXManager sfx;

    private bool win = false;
    private bool lose = false;
    public GameObject canvas;
    public GameObject youWin;
    public GameObject youLose;
    public GameObject fireworks;
    public GameObject explosion;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Refresh();
    }

    public void Initialize()
    {
        
    }

    public void Refresh()
    {
       
    }
    public void ShowVictory()
    {
        canvas.SetActive(true);
        youLose.SetActive(false);
        youWin.SetActive(true);
        fireworks.SetActive(true);
        explosion.SetActive(false);
        
    }
    public void ShowDefeat()
    {
        canvas.SetActive(true);
        youWin.SetActive(false);
        youLose.SetActive(true);
        explosion.SetActive(true);
        fireworks.SetActive(false);
    }
    public void HideUI()
    {
        canvas.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        fireworks.SetActive(false);
        explosion.SetActive(false);
    }
    
}
