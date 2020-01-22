using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mine : Trap {
    //AUDIO
    public AudioClip triggerTrapClick;
    public AudioClip boomSound;

    //Particul Effect Setting
    public GameObject explosionEffect;
    private GameObject explosionObject;

    //Explosion Setting
    public float radius = 5f;
    public float damage = 100f;

    public Mine(GameObject gameObject) {}

    public override void onAction() {}

    public override void onRemove()
    {
        GameObject.Destroy(gameObject, 0);
        GameObject.Destroy(explosionObject, 2f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlaySound(triggerTrapClick);
        TimeManager.Instance.AddTimedAction(new TimedAction(() => {
            PlaySound(boomSound);
            explosionObject = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
            EnemyManager.Instance.DamageEnemiesInRange(transform.position, radius, (int)damage);
            onRemove();
        }, 0.5f));
    }

    protected override void OnTriggerStay(Collider other) { }

    protected override void OnTriggerExit(Collider other) { }

    public override void PreInitialize() { }

    public override void Initialize() {}

    public override void Refresh() {}

    public override void PhysicsRefresh() {}

    public override void onTrigger()
    {
    }

    public override void onExitTrigger()
    {
    }
}
