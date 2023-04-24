using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private float powerupStrength = 15.0f;
    private float rotation = 50.0f;

    public bool hasPowerUp;

    public GameObject powerupIndicator;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    // Calling on the enum we made for powerups
    public PowerUpType currentPowerUp = PowerUpType.None;

    // Rocket powerup variables
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    public GameObject projectilePrefab;

    // Smash powerup variables
    public float hangTime = 0.3f;
    public float smashSpeed = 28.3f;
    public float explosionForce = 50.0f;
    public float explosionRadius = 6;
    bool smashing = true;
    float floorY;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Up and down movement, by using focalPoint.transform.forward it will tie where up and down is to where the focalpoint (the camera) is pointing
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // Make the powerupt effect rotate and track the player
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
        powerupIndicator.transform.Rotate(Vector3.up * Time.deltaTime * rotation);

        if (currentPowerUp == PowerUpType.Missiles && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchMissiles();
        }

        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Powerup activation here
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<Powerup>().PowerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            StartCoroutine(PowerupCountdownRoutine());
        }

    }

    private void OnCollisionEnter(Collision collision) 
    {
        // Make something special happen when Player has powerup and collides with an enemy
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback) 
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }

    // Finding all the enemies and launching a missile at them
    void LaunchMissiles()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<Missile>().Fire(enemy.transform);
        }
    }

    //Simple countdown coroutine to end the powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        // Store the y position before taking off
        floorY = transform.position.y;

        // Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;

        // Move the player up while still keeping their x velocity
        while(Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        // Now move the player down
        while(transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        // Cycle through all enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            // Apply an explosion force that originates from our position
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }

            // We are no longer smashing, so set the boolean to false
            smashing = false;
        }
    }
}
