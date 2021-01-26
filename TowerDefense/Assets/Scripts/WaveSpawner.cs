using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;
    private int enemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;

    public Text waveCountdownText;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public float spawnCountdown = 0.5f;

    public GameManager gm;

    private int waveIndex = 0;

    private void Start()
    {
        EnemiesAlive = enemiesAlive;
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gm.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    private IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;
         
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        waveIndex++;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
        Debug.Log("Enemies Alive: " + EnemiesAlive);
    }
}
