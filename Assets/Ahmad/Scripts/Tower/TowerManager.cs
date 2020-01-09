using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {
    private static TowerManager instance;
    private TowerManager() { }
    public static TowerManager Instance { get { return instance ?? (instance = new TowerManager()); } }

    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    List<Tower> towerList = new List<Tower>();

    public void PreInitialize() {
        GameObject basicTowerObject = Resources.Load<GameObject>("Prefabs/Basic_Tower");
        prefabs.Add("basic", GameObject.Instantiate<GameObject>(basicTowerObject));

        towerList.Add(new BasicTower(new Vector3(100, 0, 10), 5, 50, 3));

        foreach (Tower tower in towerList) {
            tower.PreInitialize();
        }
    }

    public void Initialize() {
        foreach (Tower tower in towerList) {
            tower.Initialize();
        }
    }

    public void PhysicsRefresh() {
        foreach (Tower tower in towerList) {
            tower.PhysicsRefresh();
        }
    }

    public void Refresh() {
        foreach (Tower tower in towerList) {
            tower.Refresh();
        }
    }

    void Awake() {
        PreInitialize();
    }

    void Start() {
        Initialize();
    }

    void Update() {
        Refresh();
    }

    void FixedUpdate() {
        PhysicsRefresh();
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
