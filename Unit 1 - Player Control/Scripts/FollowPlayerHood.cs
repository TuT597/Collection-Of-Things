using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerHood : MonoBehaviour
{
    /*Setting up the player variable. In Unity i can assign what object i want to be treated as the player under this variable.
    In this instance i have selected the car to be the player*/
    public GameObject playerhood;
    private Vector3 offset = new Vector3(0, 2, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called after the normal Update
    void LateUpdate()
    {
        /*Over here i make it so the transform (position) of the camera (the object the script is assigned to) is the same as the
        car i have assigned as the player. And then fix the position of the camera to be behind and above the player object.
        I use new vector in the variable as a vector already exists on the player object*/
        transform.position = playerhood.transform.position + offset;
        transform.rotation = playerhood.transform.rotation;
    }
}
