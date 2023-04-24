using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Setting up the variables required to control my vehicles movement. The public prefix simply makes it so the variables
    can be called upon outside of the class. For example by Unity or other scripts. */
    [SerializeField] float horsePower = 50.0f;
    [SerializeField] float turnSpeed = 35.0f;
    private float horizontalInput;
    private float forwardInput;
    private float rotation;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input class calls to the input manager in Unity. I then grab the axis i need for the variable from that manager and bind it to the variable
        horizontalInput = Input.GetAxis("HorizontalOne");
        forwardInput = Input.GetAxis("VerticalOne");
        //We'll move the vehicle forward. A Vector3 simply points towards a set of coordinates in a 3d plane. A Vector2 would do the same for a 2D plane
        playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
        //Rotate it, the time equation makes it so the update happens per second instead of per frame
        if (forwardInput < 0)
        {
            // rotating while vehicle moves backwards e.g. reverse
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput * -1);

        }
        else
        {
            // rotating while vehicle moves forward e.g. drive
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        }
    }
}


