using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownText;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public float spawnCountdown = 0.5f;

    private int waveIndex = 1;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }
    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnCountdown);
        }
        waveIndex++;      
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab,spawnPoint.position,spawnPoint.rotation);      
    }
}
