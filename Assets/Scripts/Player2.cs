using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public Rigidbody playerRB;
    public float bounceForce = 6;

    public static bool taskCompleted;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        taskCompleted = false;
    }

    //public AudioSource bounceAudio;
    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");
        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);
        string materialName = collision.transform.GetComponent<Renderer>().material.name;

        if (materialName == "Safe (Instance)")
        {

        }
        else if (materialName == "Unsafe (Instance)")
        {
            GameManager.gameOver = true;
            audioManager.Play("game over");
        }
        else if (materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
        {
            taskCompleted = true;
            //GameManager.levelCompleted = true;
            //audioManager.Play("win level");
        }
    }
}
