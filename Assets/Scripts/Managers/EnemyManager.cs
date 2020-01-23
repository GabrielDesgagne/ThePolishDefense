using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { FAST, SIMPLE, SLOW,BASIC_1, BASIC_2, BASIC_3, BONUS_1, BONUS_2, BOSSCOW,BOSSPUG, FAST_1, FAST_2 , FAST_3,
    FRIENDLY_1, FRIENDLY_2, HEAVY_1 , HEAVY_2 , HEAVY_3 }
public class EnemyManager : Flow
{
    #region Singleton
    static private EnemyManager instance = null;

    static public EnemyManager Instance
    {
        get
        {
            return instance ?? (instance = new EnemyManager());
        }
    }

    #endregion


    private GameObject enemyParent;
    public List<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;

    public Transform[] waypoints;

    public Dictionary<EnemyType, GameObject> enemyPrefabDict;

    override public void PreInitialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new List<Enemy>();
        enemyPrefabDict = new Dictionary<EnemyType, GameObject>();
    }

    override public void Initialize()
    {
        SetPoints(MapVariables.instance.enemyParentPoint.transform);
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
    }

    override public void Refresh()
    {
        foreach (Enemy e in enemies)
            e.Refresh();
    }

    override public void PhysicsRefresh()
    {

        while (toRemove.Count > 0)
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

        while (toAdd.Count > 0)
            enemies.Add(toAdd.Pop());


        foreach (Enemy enemy in enemies)
            enemy.PhysicsRefresh();
    }

    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);
    }

    public GameObject SpawnEnemy(GameObject enemy)
    {
        Transform enemyStart = MapVariables.instance.enemyStart.transform;
        return GameObject.Instantiate(enemy, enemyStart.position, enemyStart.rotation);
    }

    public Enemy FindFirstTargetInRange(Vector3 position, float range)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        Enemy enemy = null;
        if (enemyInRange.Count > 0)
            enemy = enemyInRange[0];
        return enemy;
    }

    public List<Enemy> EnemiesInRange(Vector3 position, float range)
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        for (int i = 0; i < enemies.Count; i++)
        {
            if (range >= Vector3.Distance(position, enemies[i].transform.position))
                enemiesInRange.Add(enemies[i]);
        }
        return enemiesInRange;
    }

    public void DamageEnemiesInRange(Vector3 position, float range, int damage)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        foreach (Enemy enemy in enemyInRange)
            enemy.TakeDamage(damage);
    }

    //decimalSpeed, has to be a decimal 0.01 - 0.99
    public void SlowEnemiesInRange(Vector3 position, float range, float decimalSpeed , float duration)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        foreach (Enemy enemy in enemyInRange)
            enemy.Slow(decimalSpeed, duration);
    }

    override public void EndFlow()
    {
        instance = null;
    }

    public void SetPoints(Transform transform)
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
