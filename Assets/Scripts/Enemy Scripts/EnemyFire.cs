using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    
    private GameObject playerShip;
    public GameObject fieldObject;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private int shotsRemaining;
    private float timeSinceLastBurst;
    public float timeBetweenBursts = 2f;
    public int shotsPerBurst = 3;

    private Transform target;
    AudioSource sourceAudioE; //Audio
    public AudioClip enemyShotAudio;

    private EnemyShipsController enemyShipsController;
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        sourceAudioE = gameObject.GetComponent<AudioSource>(); // Audio
        enemyShipsController = gameObject.GetComponent<EnemyShipsController>();

        shotsRemaining = shotsPerBurst;
    }

    // Update is called once per frame
    private void Update()
    {
        
        target = null;
        if (playerShip.IsDestroyed())
        {
            playerShip = null;
        }
        else
        {
            playerShip = GameObject.FindWithTag("Player");
        }


        if (fieldObject.GetComponent<FieldController>().isEntered == true)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            

            timeSinceLastBurst += Time.deltaTime;
            if (timeSinceLastBurst >= timeBetweenBursts && shotsRemaining > 0)
            {


                /////// kod açılması gerekiyor   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                FireBullet();
                
                enemyShipsController.isBulletOut = true;

                sourceAudioE.PlayOneShot(enemyShotAudio); //enemy shoot Audio          




                shotsRemaining--;
                timeSinceLastBurst = 0f;
            }
        }

        if (shotsRemaining <= 0)
        {
            shotsRemaining = shotsPerBurst;
        }
        enemyShipsController.isBulletOut = false;
    }

    
    void FireBullet()
    {


        var newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        newBullet.GetComponent<BulletController>().enemyBulletDamage = gameObject.GetComponent<EnemyLevelManager>().enemyDamage;
        newBullet.GetComponent<BulletController>().SetVelocity(transform.up * bulletSpeed);
        Destroy(newBullet, 3f);
    }
}
