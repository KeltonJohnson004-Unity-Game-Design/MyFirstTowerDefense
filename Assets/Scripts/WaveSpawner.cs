using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive;

    public Wave[] waves;
    private int waveIndex = 0;
    public Transform SpawnPoint;
    public Button startWaveButton;
    public bool waveIncreased;

    private void Start()
    {
        startWaveButton.interactable = true;
        waveIncreased = true;
    }

    void Update()
    {
        if (enemiesAlive > 0)
            return;

        if(waveIndex == waves.Length)
        {
            Debug.Log("You Win!! TODO: display win screen");
            this.enabled = false;
        }


        if (waveIncreased)
            return;
        PlayerStats.Rounds++;
        PlayerStats.Money += PlayerStats.Rounds + 100;
        waveIncreased = true;
        startWaveButton.interactable = true;


    }

    public void StartWave()
    {
        waveIncreased = false;
        startWaveButton.interactable = false;
        StartCoroutine(SpawnWave());
        waveIndex++;
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        enemiesAlive = wave.numberOfEnemiesToSpawn.Sum();
        for(int i = 0; i < wave.enemiesTypeToSpawn.Length; i ++)
        {
            StartCoroutine(SpawnEnemyGroup(wave.enemiesTypeToSpawn[i], wave.numberOfEnemiesToSpawn[i], wave.timeBetweenEnemies[i]));
            yield return new WaitForSeconds((wave.numberOfEnemiesToSpawn[i] * wave.timeBetweenEnemies[i]) + wave.timeBetweenGroups[i]);
        }
    }

    private IEnumerator SpawnEnemyGroup(GameObject enemyTypeToSpawn, int numberOfEnemiesToSpawn, float timeBetweenEnemies )
    {
        for(int j = 0; j < numberOfEnemiesToSpawn; j++)
        {
            SpawnEnemy(enemyTypeToSpawn);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
    }
}
