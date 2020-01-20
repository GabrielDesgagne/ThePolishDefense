using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Flow
{
    private static TowerManager instance = null;
    public static TowerManager Instance { get { return instance ?? (instance = new TowerManager()); } }

    public Dictionary<TowerType, GameObject> prefabs = new Dictionary<TowerType, GameObject>();
    public Dictionary<TowerType, GameObject> towerParent = new Dictionary<TowerType, GameObject>();

    List<Tower> towerList = new List<Tower>();
    public GameObject cannonPrefab;
    public GameObject feederPrefab;

    override public void PreInitialize()
    {
        prefabs.Add(TowerType.BASIC, Resources.Load<GameObject>("Prefabs/Tower/Basic_Tower"));
        prefabs.Add(TowerType.HEAVY, Resources.Load<GameObject>("Prefabs/Tower/Heavy_Tower"));
        prefabs.Add(TowerType.ICE, Resources.Load<GameObject>("Prefabs/Tower/Ice_Tower"));

        towerParent.Add(TowerType.BASIC, new GameObject("BasicTowerParent"));
        towerParent.Add(TowerType.HEAVY, new GameObject("HeavyTowerParent"));
        towerParent.Add(TowerType.ICE, new GameObject("IceTowerParent"));

        GameObject parent = new GameObject("Towers");
        towerParent[TowerType.BASIC].transform.parent = parent.transform;
        towerParent[TowerType.HEAVY].transform.parent = parent.transform;
        towerParent[TowerType.ICE].transform.parent = parent.transform;

        cannonPrefab = Resources.Load<GameObject>("Prefabs/Tower/Cannon");
        feederPrefab = Resources.Load<GameObject>("Prefabs/Tower/BombFeeder");

        foreach (Tower tower in towerList)
            tower.PreInitialize();
    }

    override public void Initialize()
    {
        TowerLink.tl = GameObject.FindObjectOfType<TowerLink>();

        foreach (Tower tower in towerList)
            tower.Initialize();
    }

    override public void PhysicsRefresh()
    {
        foreach (Tower tower in towerList)
            tower.PhysicsRefresh();
    }

    override public void Refresh()
    {
        foreach (Tower tower in towerList)
            tower.Refresh();
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
            case TowerType.BASIC:
                tower = new BasicTower(position, 5, 50, 5);
                break;
            case TowerType.HEAVY:
                tower = new HeavyTower(position, TowerLink.tl.heavyTowerDamage, TowerLink.tl.heavyTowerRange, TowerLink.tl.heavyTowerAttackCooldown);
                break;
            case TowerType.ICE:
                tower = new IceTower(position, TowerLink.tl.iceTowerDamage, TowerLink.tl.iceTowerRange, TowerLink.tl.iceTowerAttackCooldown);
                break;
        }
        tower.Initialize();
        towerList.Add(tower);
        return tower;
    }

    public Vector3? target;
    public Vector3 GetTarget()
    {
        return new Vector3();
    }
}
