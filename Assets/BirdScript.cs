using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{

    public LogicScript logic;

    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public AudioSource deadSFX;
    public AudioSource flapSFX;
    public bool birdIsAlive = true;
    public bool deadSFXPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
        AudioSource[] audioSources = GetComponents<AudioSource>();
        deadSFX = audioSources[0];  // Second AudioSource for death
        flapSFX = audioSources[1];  // First AudioSource for flapping
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
            flapSFX.Play();
        }

        if( birdIsAlive == false){
            
            myRigidbody.velocity = Vector2.up * -50;
        }

        if (transform.position.y > 9.62 || transform.position.y < -9.62)
        {
            
            if (birdIsAlive) // Trigger game over only once
            {
                birdIsAlive = false;
                logic.gameOver();

                if (!deadSFXPlayed) // Play the death sound only once
                {
                    deadSFX.Play();
                    deadSFXPlayed = true;
                    Debug.Log("yo");
                }
            }
            

        }
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        logic.gameOver();
    
        birdIsAlive = false;
        deadSFX.Play();
    }
}
