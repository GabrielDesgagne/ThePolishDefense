using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : Flow
{

    #region Singleton
    static private TrapManager instance = null;

    static public TrapManager Instance
    {
        get {
            return instance ?? (instance = new TrapManager());
        }
    }

    #endregion

    public float timerStart = 2f;
    public float currentTime;
    GameObject newTrap;
    public GameObject trapHolder;
    public List<Trap> listTrap;

    override public void PreInitialize()
    {
        newTrap = Resources.Load<GameObject>("Prefabs/AsterBig3");
    }

    override public void Initialize()
    {

    }

    override public void Refresh()
    {

    }

    override public void PhysicsRefresh()
    {

    }

    override public void EndFlow()
    {

    }

}
