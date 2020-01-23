using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : MonoBehaviour {
    //AUDIO
    public AudioClip triggerTrapClick;
    public AudioClip boomSound;

    //Particul Effect Setting
    public GameObject explosionEffect;
    private GameObject explosionObject;

    //Explosion Setting
    public float radius = 5f;
    public float damage = 100f;

    public void OnTriggerEnter(Collider other)
    {
        //PlaySound(triggerTrapClick);
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            //PlaySound(boomSound);
            explosionObject = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation, transform);
            EnemyManager.Instance.DamageEnemiesInRange(transform.position, radius, (int)damage);
            GameObject.Destroy(gameObject, 0.55f);
        }, 0.5f));
    }
}