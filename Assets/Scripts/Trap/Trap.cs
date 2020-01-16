using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    SPIKE,
    MINE,
    GLUE
}
public abstract class Trap : MonoBehaviour
{
    /* TODO ADD
graphics
 */

    //Timer
    public float currentTime;
    public float detonate = 2f;

    public GameObject prefab;
    protected AudioSource audioSource;

    public TrapType type { get; protected set; }
    [TextArea(15, 20)]
    public string description;

    public Vector3 TrapPosition { get; protected set; }
    public float attackDamage { get; protected set; }
    public float lifeSpawn { get; protected set; }
    public float price { get; protected set; }
    public float trapRadius { get; protected set; }
    public float coldownEffect { get; protected set; }

    public bool isInTrap = false;
    public bool isOutTrap = false;
    public bool timerOn = false;
    public bool inDetonate = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void PreInitialize();
    public abstract void Initialize();
    public abstract void Refresh();
    public abstract void PhysicsRefresh();

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
