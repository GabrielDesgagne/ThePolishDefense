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

    //spike reference
    [SerializeField] GameObject spikeRef;

    //passing setting to enemy manager

    public Spike(GameObject gameObject)
    {
        this.prefab = gameObject;
    }
    private void Start()
    {
        isOutTrap = true;
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
        throw new System.NotImplementedException();
    }

    public override void PhysicsRefresh()
    {
        throw new System.NotImplementedException();
    }

    public override void onTrigger()
    {
        throw new System.NotImplementedException();
    }

    public override void onExitTrigger()
    {
        throw new System.NotImplementedException();
    }

    public override void onAction()
    {
        throw new System.NotImplementedException();
    }

    public override void onRemove()
    {
        throw new System.NotImplementedException();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("do i trigger ?");
        currentTime = detonate;
        //spikeRef.transform.position = new Vector3(0, 0.5f, 0);
        //Debug.Log("do i trigger ?");
        isInTrap = true;
        isOutTrap = false;
    }

    protected override void OnTriggerExit(Collider other)
    {

        isInTrap = false;
        isOutTrap = true;
        spikeRef.transform.localPosition = new Vector3(0, 0.01f, 0);
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

