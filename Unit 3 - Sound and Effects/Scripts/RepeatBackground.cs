using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Create a startPos variable that has a V3 coordinate tied to it
    private Vector3 startPos;
    private float xLimit = -11.3f;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the startPos coordinate to the transform position of the background object this script is attached to
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if the transform position on the x axis becomes smaller than the xLimit variable trigger this
        if (transform.position.x < xLimit)
        {
            transform.position = startPos;
        }
    }
}
