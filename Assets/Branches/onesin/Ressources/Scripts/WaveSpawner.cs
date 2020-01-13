using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;


    public Wave[] waves;

    // 
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    private float countdown = 5f;

    [SerializeField]

    private int waveIndex = 0;

    private void Start() { Initialize(); }
    private void Update() { Refresh(); }
   
    void Initialize()
    {
        EnemiesAlive = 0;

    }

    void Refresh() {

        if(EnemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

	}

    IEnumerator SpawnWave()
    {
        
        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}

[System.Serializable]
public class Wave
{

    public GameObject enemy;
    public int count;
    public float rate;

}
