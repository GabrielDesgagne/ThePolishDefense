using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Flow {
    private static TowerManager instance = null;
    public static TowerManager Instance { get { return instance ?? (instance = new TowerManager()); } }

    public Dictionary<TowerType, GameObject> prefabs = new Dictionary<TowerType, GameObject>();

    List<Tower> towerList = new List<Tower>();

    override public void PreInitialize() {
        prefabs.Add(TowerType.BASIC, Resources.Load<GameObject>("Prefabs/Basic_Tower"));
        prefabs.Add(TowerType.HEAVY, Resources.Load<GameObject>("Prefabs/Heavy_Tower"));
        prefabs.Add(TowerType.ICE, Resources.Load<GameObject>("Prefabs/Ice_Tower"));


        foreach (Tower tower in towerList) {
            tower.PreInitialize();
        }
    }

    override public void Initialize() {

        //testing
        towerList.Add(new BasicTower(new Vector3(10, 0, 10), 5, 50, 3));
        towerList.Add(new HeavyTower(new Vector3(60, 0, 10), 5, 50, 3));
        towerList.Add(new IceTower(new Vector3(110, 0, 10), 5, 50, 3));

        foreach (Tower tower in towerList) {
            tower.Initialize();
        }
    }

    override public void PhysicsRefresh() {
        foreach (Tower tower in towerList) {
            tower.PhysicsRefresh();
        }
    }

    override public void Refresh() {
        foreach (Tower tower in towerList) {
            tower.Refresh();
        }
    }

    override public void EndFlow() {
        towerList.Clear();
    }

    //Test fonctions that need to be moved into EnemyManager
    List<Vector3> enemyList = new List<Vector3>();
    /* Retrieve the closest enemy from the base within range of a position */
    public Vector3 FindFirstTargetInRange(Vector3 position, float range) {
        Vector3 enemy = new Vector3(0, 0, 0);
        if (enemyList.Count > 0)
            enemy = enemyList[0];
        return enemy;
    }

    /* Retrieve all enemies that are within range of a position */
    public List<Vector3> EnemiesInRange(Vector3 position, float range) {
        List<Vector3> enemiesInRange = new List<Vector3>();

        foreach (Vector3 vec in enemyList) {
            if (range >= Vector3.Distance(position, vec))
                enemiesInRange.Add(vec);
        }

        return enemiesInRange;
    }
    //---------------------------//
}
