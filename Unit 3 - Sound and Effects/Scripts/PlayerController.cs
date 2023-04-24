using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private Animator playerAnim;

    private  MoveLeft moveLeftScript;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public float jumpForce;
    public float gravityModifier;
    public int score;

    public bool isOnGround = true;
    public bool usedDoubleJump = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Grabs the rigidbody component from our player object
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        // Read as: Call upon physics in unity select gravity. *= means variable = variable * ... (just a shorter way to write it)
        Physics.gravity *= gravityModifier;
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true && !gameOver)
        {
            // When spacebar is hit our upwards force is multiplied by 100. Forcemode chooses the way the force is applied, impulse makes the force immediate
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // Going into the animator component and checking the Jump trigger box while isonground is set to false
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        // Implementing a double jump, check is space bar is pressed the jump variable is already used and the double jump one is not used
        else if (Input.GetKeyDown(KeyCode.Space) && isOnGround == false && usedDoubleJump == false && !gameOver) {
            // Resets momentum so you dont accelerate too fast mid air
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            // Forces the jump animation to play again you it doesnt buffer and finish the first jumps animation first
            playerAnim.Play("Running_Jump", -1, 0f);
            // Sets the double jump variable to true so i cant jump a third time
            usedDoubleJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        // Here we check if the collision with gameobject is the ground or an obstacle the objects have assigned tags int he unity editor
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isOnGround = true;
            usedDoubleJump = false;
            dirtParticle.Play();
        } 
        
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over! You score was: " + score);
            // In the animator set death to true and select the death animation 1
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
