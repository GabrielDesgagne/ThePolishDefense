using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : Trap
{
    public GameObject mineTest;
    GameObject mine;
    public MineBehaviour mineB;
    UnityAction action;
    UnityAction actionOnAction;

    public AudioClip triggerTrapClick;
    public AudioClip boomSound;


    bool canDetonate = false;

    private void Start()
    {
        mineTest = Instantiate(mineTest);
        mineB = mineTest.GetComponent<MineBehaviour>();
        action += onTrigger;
        //actionOnAction += onAction;
        mineB.onTrigger(action);
        //mineB.onAction(actionOnAction);
        
    }

    private void Update()
    {
        if (inDetonate)
        {
            currentTime -= Time.deltaTime;
            canDetonate = true;
        }
        if(canDetonate)
        {
            // currentTime = 0;
        }
    }

    public override void onAction()
    {
        if(currentTime <= 0)
        {
            Debug.Log("boom");
            PlaySound(boomSound);
        }
    }

    public override void onExitTrigger()
    {

    }

    public override void onRemove()
    {
        mineTest.SetActive(false);
    }

    public override void onTrigger()
    {
        Debug.Log("on trigger trigger timer");
        inDetonate = true;
        currentTime = detonate;
        PlaySound(triggerTrapClick);
    }

    public void PlaySound(AudioClip audio)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audio);
    }
}
