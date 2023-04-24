using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private float leftBound = -15;

    public bool speedBoost = false;
    
    // Here i make a variable that will be holdng the reference to my playercontroller script
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Here i get the Player gameobject (GameObject.Find("Player")) then i grab its PlayerController component which is the playercontroller script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Here i grab the playercontroller script we saves in the variable then check for the gameOver variable
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // Adding the speedboost, while the left shift key is pressed the speed increases to 35 and the speedBoost variable is set to true (I will call upon this variable for the double score)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 35;
            speedBoost = true;
            playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 2.0f);
        }

        else
        {
            speed = 20;
            speedBoost = false;
            playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.5f);
        }

        // If the position x axis of the object becomes smaller than the leftbound variable and the object tag is obstacle trigger this 
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            if (speedBoost == true)
            {
                playerControllerScript.score += 2;
            }
            
            else
            {
                playerControllerScript.score += 1;
            }
        
            Debug.Log(playerControllerScript.score);
        }
    }
}
