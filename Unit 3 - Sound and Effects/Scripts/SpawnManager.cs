using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Create a reference to a game object you can asign in the unity editor and call it obstaclePrefab
    public GameObject[] obstaclePrefabs;
    // Create a new spawnposition use new vector instead of just vector because it gives it a completely new set of coordinates instead of adjustment
    private Vector3 spawnPos = new Vector3(35,0,0);

    // For explanation about the playercontroller lines check the MoveLeft script
    private PlayerController playerControllerScript;

    private float startDelay = 2;
    private float repeatDelay = 2;
    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating invokes a method on a delay you can put int he command
        InvokeRepeating("SpawnObstacle", startDelay, repeatDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Instantiate (create) the GameObject we referenced earlier in our variables give it a spawn position and choose its rotation
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }
}
