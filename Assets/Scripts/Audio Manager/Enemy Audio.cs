using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{

    AudioSource sourceAudioEnemy;
    
    public AudioClip woodCrackClip;
    //  public AudioClip woodCrackSFX;


    void Start()
    {
        sourceAudioEnemy = gameObject.GetComponent<AudioSource>();
       // sourceAudioEnemy.volume = 0.3f; //changing volume  0.0 - 1.0

    }


    void Update()
    {

        woodCrack();



    }


    void woodCrack()
    {
        EnemyMovement enemyMovementScript = gameObject.GetComponent<EnemyMovement>();
        if (enemyMovementScript.isBulletEntered == true)
        {

            sourceAudioEnemy.PlayOneShot(woodCrackClip);

        }
        enemyMovementScript.isBulletEntered = false;

    }
}
