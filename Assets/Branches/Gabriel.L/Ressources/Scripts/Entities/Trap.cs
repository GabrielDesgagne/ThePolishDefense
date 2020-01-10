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

    public GameObject prefab;
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
    bool inDetonate = false;
    private void Start()
    {
       
        //var boxCollider = gameObject.AddComponent<BoxCollider>();
        //boxCollider.isTrigger = true;
    }
    private void Update()
    {
        if (inDetonate)
        {
            if(currentTime <= 0)
            {

            }
            else
            {

        currentTime -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        currentTime = detonate;
        timerOn = true;
        Debug.Log("something in : " + other.name);
        if (other)
        {
            isInTrap = true;
            Debug.Log("trigger timer");
            if (currentTime == 0)
            {
                Detonate();
                currentTime = detonate;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isInTrap)
        {
            Debug.Log("exit :" + other.name);
        }
    }

    void Detonate()
    {
        Debug.Log("Boom");
    }

    public abstract void onTrigger();
   
    public abstract void onExitTrigger();

    public abstract void onAction();

    public abstract void onRemove();

}
