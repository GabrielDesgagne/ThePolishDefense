using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : Flow
{

    #region Singleton
    static private TrapManager instance = null;

    static public TrapManager Instance
    {
        get
        {
            return instance ?? (instance = new TrapManager());
        }
    }

    #endregion

    //Prefabs Paths
    const string SPIKE = "Prefabs/TrapsAndWeapons/Spiketrap";
    const string MINE = "Prefabs/TrapsAndWeapons/Mine";
    const string GLUE = "Prefabs/TrapsAndWeapons/GlueTrap";

    //GameObject List
    public GameObject trapHolder;
    public List<Trap> listTrap;
    public Dictionary<TrapType, GameObject> trapPrefabs;


    override public void PreInitialize()
    {
        //trap holder
        trapHolder = new GameObject("Trap Holder");

        //init dictionnary
        trapPrefabs = new Dictionary<TrapType, GameObject>();

        //add all trap in dictionnary
        trapPrefabs.Add(TrapType.MINE, Resources.Load<GameObject>(MINE));
        trapPrefabs.Add(TrapType.SPIKE, Resources.Load<GameObject>(SPIKE));
        trapPrefabs.Add(TrapType.GLUE, Resources.Load<GameObject>(GLUE));

        //init list of trap
        listTrap = new List<Trap>();

        foreach (Trap trap in listTrap)
        {
            trap.PreInitialize();
        }
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

    public Trap CreateTrap(TrapType type, Vector3 position)
    {
        //TODO
        Trap trap = null;
        switch (type)
        {
            case TrapType.MINE:
                trap = new Mine(GameObject.Instantiate(trapPrefabs[TrapType.MINE], trapHolder.transform));
                trap.transform.position = position;
                break;
            case TrapType.SPIKE:
                trap = new Spike(GameObject.Instantiate(trapPrefabs[TrapType.SPIKE], trapHolder.transform));
                trap.transform.position = position;

                break;
            case TrapType.GLUE:
                trap = new Glue(GameObject.Instantiate(trapPrefabs[TrapType.GLUE], trapHolder.transform));
                trap.transform.position = position;

                break;
        }
        return null;
    }

}
