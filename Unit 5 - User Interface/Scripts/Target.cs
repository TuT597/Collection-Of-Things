using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    /* -------------------------------- Variables ------------------------------- */
    public int pointValue;
    public ParticleSystem explosionParticle;
    
    private GameManager gameManager; 
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -4;
    /* -------------------------------------------------------------------------- */


    /* ---------------------------------- Start --------------------------------- */
    void Start()
    {
        // Link to the game manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Grab the body of the target
        targetRb = GetComponent<Rigidbody>();
        // Apply upward force to the target so it flies into the scene
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // Torque adds rotation force im adding random force to each axis so the item will spin randomly
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // Randomly spread the targets around the bottom side of the scene
        transform.position = RandomSpawnPos();
    }
    /* -------------------------------------------------------------------------- */


    /* --------------------------------- Update --------------------------------- */
    void Update()
    {

    }
    /* -------------------------------------------------------------------------- */


    /* --------------------------------- Methods -------------------------------- */
    // Generate random force amount
    Vector3 RandomForce() 
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Generate random torque amount
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Generate random spawn position on X axis
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown() 
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }    
    }

    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
        // Check if the destroyed target does NOT have the bad tag and trigger the game over state
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
    /* -------------------------------------------------------------------------- */
}
