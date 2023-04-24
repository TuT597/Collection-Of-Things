using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectColossions : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            gameManager.AddLives(-1);
            Destroy(gameObject); 
        } 
        
        else if (other.CompareTag("Animal"))
        { 
            other.GetComponent<AnimalHunger>().FeedAnimal(1); 
            Destroy(gameObject);
        } 
    }
}
