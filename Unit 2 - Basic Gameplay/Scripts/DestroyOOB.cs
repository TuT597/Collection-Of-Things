using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -15;
    private float sideBound = 35.0f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        // When the position in the variable is reached it destroys the object this script is attached to
        if (transform.position.z > topBound || transform.position.z < lowerBound || transform.position.x > sideBound || transform.position.x < -sideBound)
        {
            Destroy(gameObject);
        }
    }
}
