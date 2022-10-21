using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Transform player;
    private Transform player2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if(transform.position.y > player.position.y && transform.position.y > player2.position.y)
        if(transform.position.y > player.position.y)
        {
            FindObjectOfType<AudioManager>().Play("whoosh");
            GameManager.numberOfPassedRings++;
            Debug.Log("Ring passed = " + GameManager.numberOfPassedRings);
            GameManager.score++;
            Destroy(gameObject);
        }
    }
}
