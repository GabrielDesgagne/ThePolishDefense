using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : Trap
{

    //AUDIO
    public AudioClip triggerTrapClick;
    public AudioClip boomSound;

    //Particul Effect Setting
    public GameObject explosionEffect;
    GameObject explosionRef;
    public float explosionDuration;
    public float boomLength;

    //Explosion Setting
    public float radius = 5f;
    public float force = 700;

    //Bool toggle Action
    bool canDetonate = false;
    bool isTrigger = false;

    public Mine(TrapType t)
    {
        this.type = t; //remove
        this.type = TrapType.MINE;
        this.TrapPosition = new Vector3(0, 0, 0);
        this.attackDamage = 50;
        this.lifeSpawn = 1;
        this.price = 100;
        this.trapRadius = 7;
        this.coldownEffect = 2;
        this.force = 700;
    }

    public Mine(Vector3 position, float damage,float radius, float coldown, float force)
    {
        this.TrapPosition = position;
        this.type = TrapType.MINE;
        this.attackDamage = damage;
        this.trapRadius = radius;
        this.coldownEffect = coldown;
        this.force = force;
    }



    private void Start()
    {
        //NULL CHECK
        if (audioSource == null)
        {
            //Debug.Log("audio source not loaded");
        }

        //Set action time--- use as a timer to delete object
        boomLength = boomSound.length;
        explosionDuration = explosionEffect.GetComponent<ParticleSystem>().main.duration;
    }

    private void Update()
    {
        //Toggle mine Action
        if (inDetonate)
        {
            currentTime -= Time.deltaTime;
            canDetonate = true;
        }
        if (canDetonate)
        {
            onAction();
            canDetonate = false;
        }
    }

    public override void onAction()
    {
        if (currentTime <= 0)
        {
            //Debug.Log("boom");
            //TODO explosion move enemy qui va perturber le chmin des enemy
            gameObject.AddComponent<Rigidbody>();
            explosionRef = Instantiate(explosionEffect, transform.position, transform.rotation);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearByGO in colliders)
            {
                Rigidbody rb = nearByGO.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }
                //add damage
            }

            PlaySound(boomSound);
            inDetonate = false;

            onRemove();
        }
    }

    public override void onExitTrigger()
    {

    }

    public override void onRemove()
    {
        GameObject.Destroy(gameObject, boomLength);
        GameObject.Destroy(explosionRef, explosionDuration);
    }

    public override void onTrigger()
    {
        //Debug.Log("on trigger trigger timer");
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
                // Debug.Log("name of the collision : " + other.name);
                onTrigger();
            }
        }

    }

    protected override void OnTriggerStay(Collider other)
    {
        //  Debug.Log("this gameobject : " + other.name + " still in the range of the trap");
    }

    protected override void OnTriggerExit(Collider other)
    {
        //  Debug.Log(other.name + " left the range of the trap");
    }

    public override void PreInitialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        this.prefab = GameObject.Instantiate(TrapManager.Instance.trapPrefabs[TrapType.MINE]);
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }

    public override void PhysicsRefresh()
    {
        throw new System.NotImplementedException();
    }
}
