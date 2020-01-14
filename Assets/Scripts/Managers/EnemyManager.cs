using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { FAST, SIMPLE, SLOW }
public class EnemyManager : Flow
{
    #region Singleton
    static private EnemyManager instance = null;

    static public EnemyManager Instance
    {
        get {
            return instance ?? (instance = new EnemyManager());
        }
    }

    #endregion


    /*Transform spawnPoint;
    public Transform rootNodeParent;
    public HashSet<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public List<Enemy> toAdd;
    readonly float initialEggSpawnHeight = 50;
    public static GameObject rootPrefab;

    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new HashSet<Enemy>();
        rootPrefab = Resources.Load<GameObject>("Prefabs/RootNode");
        spawnPoint = new GameObject("EnemyParent").transform;
        rootNodeParent = new GameObject("RootNodeParent").transform;
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }

        SpawnInitialSkyEggs();

    }

    override public void Refresh()
    {
        foreach (Enemy e in enemies)
            if (e.isAlive)
                e.PhysicRefresh();
    }

    override public void PhysicsRefresh()
    {
        foreach (Enemy e in enemies)
            if (e.isAlive)
                e.Refresh();


        while (toRemove.Count > 0) //remove all dead ones
        {
            try
            {
                Enemy e = toRemove.Pop();
                enemies.Remove(e);
                GameObject.Destroy(e.gameObject);
            }
            catch
            {
                Debug.Log("hey this happened");
            }
        }

        while (toAdd.Count > 0) //Add new ones
            enemies.Add(toAdd.Pop());
    }

    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);

    }

    public Enemy SpawnEnemy(EnemyType eType, Vector3 spawnLoc, float startingEnergy)
    {
        if (eType == EnemyType.Egg)
        {
            Debug.LogError("Do not use SpawnEnemy to spawn an egg, use CreateEnemyEgg instead, eggs require more parmeters");
            return CreateEnemyEgg(spawnLoc, new Vector3(), 0, startingEnergy);
        }

        GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[eType], spawnPoint);       //create from prefab
        newEnemy.transform.position = spawnLoc;     //set the position
        Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
        e.Initialize(startingEnergy);               //initialize the enemy
        toAdd.Push(e);                              //add to update list
        return e;
    }
    override public void EndFlow()
    {

    }*/
}
