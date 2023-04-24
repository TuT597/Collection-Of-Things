using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject HoodCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            CameraOne ();
        }

        if (Input.GetKeyDown("2"))
        {
            CameraTwo ();
        }
        
    }

    void CameraOne()
    {
        MainCamera.SetActive(true);
        HoodCamera.SetActive(false);
    }

    void CameraTwo()
    {
        MainCamera.SetActive(false);
        HoodCamera.SetActive(true);
    }
}
