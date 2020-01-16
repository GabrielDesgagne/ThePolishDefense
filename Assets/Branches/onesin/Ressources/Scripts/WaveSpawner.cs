using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    //[SerializeField]
    public Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    [SerializeField]
    private int waveIndex = 0;

    [SerializeField]
    private GameObject countdown;
    private Countdown waveCountdownTimer;

    private void Start() { Initialize(); }
    private void Update() { Refresh(); }
   
    public void Initialize()
    {
        EnemiesAlive = 0;
        waveCountdownTimer = countdown.GetComponent<Countdown>();
        waveCountdownTimer.countdown = timeBetweenWaves;
        foreach (Wave w in waves)
        {
            w.Load();
        }

    }

    public void Refresh() {
        //condition to restart countdown
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveCountdownTimer.countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            waveCountdownTimer.countdown = timeBetweenWaves;
            return;
        }
        waveCountdownTimer.Deduct();
        

    }

    IEnumerator SpawnWave()
    {
        
        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.enemy.Count;

        for (int i = 0; i < wave.enemy.Count; i++)
        {
            SpawnEnemy(wave.enemy[i]);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}

/*[System.Serializable]
public class Wave
{

    public GameObject enemy;
    public int count;
    public float rate;

}*/
