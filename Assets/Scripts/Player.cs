using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public float bounceForce = 6;

    private AudioManager audioManager;

    public static bool taskCompleted;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    //public AudioSource bounceAudio;
    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");
        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);
        string materialName = collision.transform.GetComponent<Renderer>().material.name;
        
        if(materialName == "Safe (Instance)")
        {

        }else if(materialName == "Unsafe (Instance)")
        {
            GameManager.gameOver = true;
            audioManager.Play("game over");
        }
        else if(materialName == "LastRing (Instance)" && !GameManager.levelCompleted)
        {
            if(GameManager.currentLevelIndex <= 3)
            {
                GameManager.levelCompleted = true;
                audioManager.Play("win level");
            }
            else
            taskCompleted = true;
        }
    }

    public void PlayAudio(string clipName)
    {
        audioManager.Play(clipName);
    }
}
