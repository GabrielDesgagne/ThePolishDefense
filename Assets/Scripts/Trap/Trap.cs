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

    //Gameobject component
    public GameObject prefab;
    protected AudioSource audioSource;

    //Trap Setting
    public TrapType type;
    [TextArea(15, 20)]
    public string description;

    public Vector3 TrapPosition { get; protected set; }
    public float attackDamage;
    public float lifeSpawn;
    public float price;
    public float trapRadius;
    public float coldownEffect;

    //toggle set Action
    public bool isInTrap = false;
    public bool isOutTrap = false;
    public bool timerOn = false;
    public bool inDetonate = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    //use to test if my manager create trap and trap have is behaviours!!
    /*private void Start()
    {
        TrapManager.Instance.PreInitialize();
        TrapManager.Instance.CreateTrap(TrapType.MINE, new Vector3(0,10,0));
    }*/

    //Flow methode
    public abstract void PreInitialize();
    public abstract void Initialize();
    public abstract void Refresh();
    public abstract void PhysicsRefresh();

    //Action Methode
    public abstract void onAction();
    public abstract void onRemove();
    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnTriggerStay(Collider other);

    protected abstract void OnTriggerExit(Collider other);

    //Audio Setup
    protected void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
