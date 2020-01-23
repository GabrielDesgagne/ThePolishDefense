using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Spike : Trap
{
    //AUDIO
    public AudioClip slashAudio;

    //Particul Effect Setting
    public GameObject slashParticul;
    GameObject slashRef;

    //using Math.lerp setting
    public float minimum = -1.0F;
    public float maximum = 1.0F;
    public float speed;
    static float t = 0.01f;
    public float radius = 5f;
    public float damage = 100f;

    //spike reference
    [SerializeField] GameObject spikeRef;

    //passing setting to enemy manager

    public Spike(GameObject gameObject)
    {
    }

    public override void onTrigger()
    {

    }

    public override void onExitTrigger()
    {

    }

    public override void PreInitialize()
    {
        isOutTrap = true;
        detonate = 1f;
    }

    public override void Initialize()
    {

    }

    public override void Refresh()
    {

    }

    public override void PhysicsRefresh()
    {

    }

    //Action for the spike Methode
    public override void onAction()
    {
    }

    public override void onRemove()
    {
        GameObject.Destroy(gameObject, 0);
        GameObject.Destroy(slashRef, 2f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlaySound(slashAudio);
        TimeManager.Instance.AddTimedAction(new TimedAction(() =>
        {
            PlaySound(slashAudio);
            slashRef = GameObject.Instantiate(slashParticul, transform.position, transform.rotation);
            EnemyManager.Instance.DamageEnemiesInRange(transform.position, radius, (int)damage);
            onRemove();
        }, 0.5f));
    }

    protected override void OnTriggerExit(Collider other)
    {
    }

    protected override void OnTriggerStay(Collider other)
    {
        spikeRef.transform.localPosition = new Vector3(0, Mathf.Lerp(minimum, maximum, t), 0);
        t += speed * Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
            PlaySound(slashAudio);
        }
    }
}

