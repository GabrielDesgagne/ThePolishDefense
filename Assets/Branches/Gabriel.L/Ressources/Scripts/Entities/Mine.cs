using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : Trap
{
    //public GameObject mineTest;
    GameObject mine;
    public MineBehaviour mineB;
    UnityAction action;
    UnityAction actionOnAction;

    public AudioClip triggerTrapClick;
    public AudioClip boomSound;

    public GameObject explosionEffect;
    public float radius = 5f;
    public float force = 700;

    bool canDetonate = false;
    public bool canRemove = false;

    public float boomLength;
    public float currentExplosionTime;
    bool isTrigger = false;

    private void Start()
    {
        // mineTest = Instantiate(mineTest);
        //mineB = mineTest.GetComponent<MineBehaviour>();
        //action += onTrigger;
        //actionOnAction += onAction;
        //mineB.onTrigger(action);
        //mineB.onAction(actionOnAction);
        if (audioSource == null)
        {
            Debug.Log("audio source not loaded");
        }
        boomLength = boomSound.length;
    }

    private void Update()
    {
        if (inDetonate)
        {
            currentTime -= Time.deltaTime;
            canDetonate = true;
        }
        if (canDetonate)
        {
            onAction();
        }
    }

    public override void onAction()
    {
        if (currentTime <= 0)
        {
            //Debug.Log("boom");
            gameObject.AddComponent<Rigidbody>();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Collider[] colliders =  Physics.OverlapSphere(transform.position, radius);

            foreach(Collider nearByGO in colliders)
            {
                Rigidbody rb = nearByGO.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }
                //add damage
            }

            PlaySound(boomSound);
            inDetonate = false;
            canDetonate = false;
            onRemove();
        }
    }

    public override void onExitTrigger()
    {

    }

    public override void onRemove()
    {
        GameObject.Destroy(gameObject, boomLength);

        //mineTest.SetActive(false);
    }

    public override void onTrigger()
    {
        Debug.Log("on trigger trigger timer");
        inDetonate = true;
        currentTime = detonate;
        PlaySound(triggerTrapClick);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!isTrigger)
        {
            isTrigger = true;
            if (other)
            {
                isTrigger = true;
                Debug.Log("name of the collision : " + other.name);
                onTrigger();
            }
        }
        
    }

    protected override void OnTriggerStay(Collider other)
    {
        Debug.Log("this gameobject : " + other.name + " still in the range of the trap");
    }

    protected override void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " left the range of the trap");
    }
}
