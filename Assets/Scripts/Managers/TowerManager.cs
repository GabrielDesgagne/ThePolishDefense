using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Flow {
    private static TowerManager instance = null;
    public static TowerManager Instance { get { return instance ?? (instance = new TowerManager()); } }

    public Dictionary<TowerType, GameObject> prefabs = new Dictionary<TowerType, GameObject>();

    List<Tower> towerList = new List<Tower>();

    override public void PreInitialize()
    {
        prefabs.Add(TowerType.BASIC, Resources.Load<GameObject>("Prefabs/Tower/Basic_Tower"));
        prefabs.Add(TowerType.HEAVY, Resources.Load<GameObject>("Prefabs/Tower/Heavy_Tower"));
        prefabs.Add(TowerType.ICE, Resources.Load<GameObject>("Prefabs/Tower/Ice_Tower"));


        foreach (Tower tower in towerList)
        {
            tower.PreInitialize();
        }
    }

    override public void Initialize()
    {

        //testing
        towerList.Add(new BasicTower(new Vector3(10, 0, 10), 5, 50, 3));
        towerList.Add(new HeavyTower(new Vector3(60, 0, 10), 5, 50, 3));
        towerList.Add(new IceTower(new Vector3(110, 0, 10), 5, 50, 3));

        foreach (Tower tower in towerList)
        {
            tower.Initialize();
        }
    }

    override public void PhysicsRefresh()
    {
        foreach (Tower tower in towerList)
        {
            tower.PhysicsRefresh();
        }
    }

    override public void Refresh()
    {
        foreach (Tower tower in towerList)
        {
            tower.Refresh();
        }
    }

    override public void EndFlow()
    {
        towerList.Clear();
    }

    public Tower CreateTower(TowerType type, Vector3 position)
    {
        Tower tower = null;
        switch (type)
        {
            //magic values 5, 50, 5 for testing
            case TowerType.BASIC:
                tower = new BasicTower(position, 5, 50, 5);
                break;
            case TowerType.HEAVY:
                tower = new HeavyTower(position, 5, 50, 5);
                break;
            case TowerType.ICE:
                tower = new IceTower(position, 5, 50, 5);
                break;
        }
        towerList.Add(tower);
        return tower;
    }

    public Vector3? target;
    public Vector3 GetTarget()
    {
        GridManager.Instance.LookForHitOnTables(out target);
        return target.GetValueOrDefault();
    }
}
