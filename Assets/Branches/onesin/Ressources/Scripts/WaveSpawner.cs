using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;
    public Logic gameLogic;

    Wave[] waves;

    //[SerializeField]
    //public Transform spawnPoint;

    private float timeBetweenWaves ;

    [SerializeField]
    private int waveIndex = 0;

    [SerializeField]
    private GameObject countdown;
    private Countdown waveCountdownTimer;
    //just for test
    //Dictionary<EnemyType, GameObject> enemyPrefab;
   
    public void Initialize()
    { 
        EnemiesAlive = 0;
        waveCountdownTimer = countdown.GetComponent<Countdown>();
        waveCountdownTimer.Initialize();
        //Instantiate(countdown, gameObject.transform.position, Quaternion.identity);
        LevelSystem levelSystem = GameVariables.instance.levelSystem;
        waves = levelSystem.levels[(int)levelSystem.currentLevel].waves;
        timeBetweenWaves= levelSystem.levels[(int)levelSystem.currentLevel].timeBetweenWaves;
        waveCountdownTimer.countdown = timeBetweenWaves;
        /*foreach (Wave w in waves)
        {
            w.Load();
        }*/

    }

    public void Refresh(Dictionary<EnemyType, GameObject> enemyPrefab) {
        //condition to restart countdown
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            //gameLogic.WinLevel();
            this.enabled = false;
        }

        if (waveCountdownTimer.countdown <= 0f)
        {

            StartCoroutine(SpawnWave(enemyPrefab));
            waveCountdownTimer.countdown = timeBetweenWaves;
            return;
        }
        waveCountdownTimer.Deduct();
        

    }

    IEnumerator SpawnWave(Dictionary<EnemyType, GameObject> enemyPrefab)
    {
        PlayerStats.nextLevel();


        Wave wave = waves[waveIndex];

        //EnemiesAlive = wave.enemy.Count;
        for (int i = 0; i < wave.types.Length; i++)
        {
            for (int j = 0; j < wave.types[i].number; j++)
            {
                GameObject newEnemy = SpawnEnemy(enemyPrefab[wave.types[i].type]);
                Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
                e.Initialize();
                EnemyManager.Instance.toAdd.Push(e);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        EnemiesAlive = EnemyManager.Instance.enemies.Count;
        /*for (int i = 0; i < wave.enemy.Count; i++)
        {
            GameObject newEnemy = SpawnEnemy(wave.enemy[i]);
            Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
            EnemyManager.Instance.toAdd.Push(e);
            yield return new WaitForSeconds(1f / wave.rate);
        }*/

        waveIndex++;

    }

    GameObject SpawnEnemy(GameObject enemy)
    {
        Transform enemyStart = GameVariables.instance.enemyStart.transform;
        return Instantiate(enemy, enemyStart.position, enemyStart.rotation);
    }
}

/*[System.Serializable]
public class Wave
{

    public GameObject enemy;
    public int count;
    public float rate;

}*/
