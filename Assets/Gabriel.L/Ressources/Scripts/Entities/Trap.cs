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
    //timer setup
    public float triggerTrap = 2f;
    public float currentTime;
    /* to ADD
graphics
sound
     */

    //path to prefabs
    const string SPIKE = "Prefabs/Spikes";
    public const string MINE = "Prefabs/Mine";
    const string GLUE = "Prefabs/Glue";

    //trap stats
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

    //enemy detection in and out the trap
    public bool isInTrap = false;
    public bool isOutTrap = false;
    public bool timerOn = false;

    //methode to define in specific class
    public abstract void onTrigger();

    public abstract void onExitTrigger();

    public abstract void onAction();

    public abstract void onRemove();

}
