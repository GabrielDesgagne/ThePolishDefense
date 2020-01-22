using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { FAST, SIMPLE, SLOW, KNIGHT, BOSS }
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


    //public Transform spawnPoint;
    private GameObject enemyParent;
    public List<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;

    public Transform[] waypoints;

    public Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs


    //public static int EnemiesAlive = 0;//number of enemy alive

    private GameObject waveSpawner;
    public WaveSpawner spawner;

    //public Wave[] waves;
    //private float timeBetweenWaves = 5f;

    //private int waveIndex = 0;

    //private GameObject countdown;
    //private Countdown waveCountdownTimer;

    override public void PreInitialize()
    {

    }

    override public void Initialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new List<Enemy>();
        //spawnPoint =Resources.Load<GameObject>("Prefabs/START").transform;
        SetPoints(MapVariables.instance.enemyParentPoint.transform);
        waveSpawner = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/WaveSpawner"));
        spawner= waveSpawner.GetComponent<WaveSpawner>();
        //spawner.Initialize();
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/Prefabs/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
        //countdown= Resources.Load<GameObject>("Prefabs/TimerUI");
        //EnemiesAlive = 0;
        //waveCountdownTimer = countdown.GetComponent<Countdown>();
        //waveCountdownTimer.countdown = timeBetweenWaves;
    }

    override public void Refresh()
    {
        //spawner.Refresh(enemyPrefabDict);
        foreach (Enemy e in enemies)
            e.Refresh();

        
        /* if (EnemiesAlive > 0)
         {
             return;
         }

         if (waveCountdownTimer.countdown <= 0f)
         {
             MonoBehaviour mono = new MonoBehaviour();
             mono.StartCoroutine(SpawnWave());
             waveCountdownTimer.countdown = timeBetweenWaves;
             return;
         }
         waveCountdownTimer.Deduct();*/
    }

    override public void PhysicsRefresh()
    {

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

    /*GameObject SpawnEnemy(GameObject enemy)
    {
        return GameObject.Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }*/

    /* Retrieve the closest enemy from the base within range of a position */
    public Enemy FindFirstTargetInRange(Vector3 position, float range)
    {
        List<Enemy> enemyInRange = EnemiesInRange(position, range);
        Enemy enemy = null;
        if (enemyInRange.Count > 0)
            enemy = enemyInRange[0];
        return enemy;
    }

    /* Retrieve all enemies that are within range of a position */
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

    override public void EndFlow()
    {

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
