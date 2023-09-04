using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShipAudio : MonoBehaviour
{
    
    
    AudioSource sourceAudio;

    public AudioClip woodCrackClip;
  //  public AudioClip woodCrackSFX;


    void Start()
    {
     sourceAudio = GetComponent<AudioSource>();
    // sourceAudio.volume = 0.3f; //changing volume  0.0 - 1.0

    }


    void Update()
    {

        woodCrack();
   
            
            
    }


    void woodCrack()
    {
        ShipMovementScript shipMovementScript = GetComponent<ShipMovementScript>();
        if (shipMovementScript.isBulletEntered == true)
        {
            
            sourceAudio.PlayOneShot(woodCrackClip);

        }
        shipMovementScript.isBulletEntered = false;

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{



    //    if (collision.gameObject.CompareTag("EnemyBullet"))
    //    {

    //        sourceAudio.PlayOneShot(woodCrackSFX);
    //    }


    //}




}
