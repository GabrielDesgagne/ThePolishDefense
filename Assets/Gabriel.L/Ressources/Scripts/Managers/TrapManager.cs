using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager
{
    public float timerStart = 2f;
    public float currentTime;
    GameObject newTrap;
    public GameObject trapHolder;
    public List<Trap> listTrap;

    private static TrapManager instance;

    private TrapManager() { }

    public static TrapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TrapManager();
            }
            return instance;
        }
    }
    public void InitTrap()
    {
        newTrap = Resources.Load<GameObject>("Prefabs/AsterBig3");
    }

    public void Refresh()
    {

    }

    public void PhysicRefresh()
    {

    }

}
