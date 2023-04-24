using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Make an array of game objects called animal prefabs
    public GameObject[] animalPrefabs;

    // Spawn ranges for animals coming from above
    private float spawnRangeX = 20;
    private float spawnPosZ = 30;

    // Spawn ranges for animals coming from the sides
    private float spawnUpZ = 18;
    private float spawnDownZ = 3;
    private float spawnPosX = 30;

    // Start statement variables
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating selects a method to execute at the start and then repeat at a set interval
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        /* When S is pressed the SpawnRandomAnimal method is executed
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnRandomAnimal();
        }*/
    }

    void SpawnRandomAnimal()
    {
        // Generate a random spawn position and number between 0 and the size of the array and spawn that animal
        int animalIndexTop = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosTop = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Instantiate(animalPrefabs[animalIndexTop], spawnPosTop, animalPrefabs[animalIndexTop].transform.rotation);

        // Spawning in animals from the sides
        int animalIndexSide = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosSide = new Vector3(spawnPosX, 0, Random.Range(spawnDownZ, spawnUpZ));
        Instantiate(animalPrefabs[animalIndexSide], spawnPosSide, Quaternion.Euler(0, -90, 0));
    }
}

//3-18 Z range

