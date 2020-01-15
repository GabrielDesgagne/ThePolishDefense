using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapName
{
    SPIKE,
    MINE,
    GLUE
}
public abstract class Trap : MonoBehaviour
{
    public float detonate = 2f;
    public float currentTime;
    /* to ADD
graphics
sound
     */
    const string SPIKE = "Prefabs/Spikes";
    const string MINE = "Prefabs/Mine";
    const string GLUE = "Prefabs/Glue";

    //public AudioClip triggerTrapClick;


    public GameObject prefab;
    protected AudioSource audioSource;
    public TrapName nameTrap;
    [TextArea(15, 20)]
    public string description;
    public Vector3 trapPosition;
    public float attackDamage;
    public float lifeSpawn;
    public float price;
    public float trapRangeEffect;
    public float coldownEffect;
    public bool isInTrap = false;
    public bool isOutTrap = false;
    public bool timerOn = false;
    public bool inDetonate = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void onTrigger();
   
    public abstract void onExitTrigger();

    public abstract void onAction();

    public abstract void onRemove();

    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnTriggerStay(Collider other);

    protected abstract void OnTriggerExit(Collider other);
    protected void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
