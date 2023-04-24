using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9;

    public int enemyCount;
    public int waveNumber = 1;

    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefabs[RandomPowerup()], GenerateSpawnPosition(), powerupPrefabs[0].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // Search for the amount of objects named enemy and return the amount as an int
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // When the returned int equals 0 trigger this script
        if (enemyCount == 0)
        {
            // The wave number goes up and a the spawnwave method is triggered. There is also a new powerup
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefabs[RandomPowerup()], GenerateSpawnPosition(), powerupPrefabs[0].transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Using a for loop i keep spawning more enemies, here i use the wavenumber we created earlier to decide how many enemies will spawn by inserting the number into a for loop
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            // Here i call upon the random spawn coordinate method instead of writing everything into the start method, this makes the code easier to read.
            Instantiate(enemyPrefabs[RandomEnemy()], GenerateSpawnPosition(), enemyPrefabs[0].transform.rotation);
        }
    }

    
    // Here i generate a bunch of numbers for the above code
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private int RandomPowerup()
    {
        int powerupIndex = Random.Range(0, powerupPrefabs.Length);
        return powerupIndex;
    }

     private int RandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyIndex;
    }
}
