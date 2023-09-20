using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    private GameObject playerShip;

    public GameObject bulletPrefab;
    public Transform[] rightFirePoint;
    public Transform[] leftFirePoint;
    public float bulletSpeed = 10f;

    private int shotsRemaining;
    private float timeSinceLastBurst;
    public float timeBetweenBursts = 2f;
    public int shotsPerBurst = 3;

    private Transform target;
    AudioSource sourceAudioE; //Audio
    public AudioClip enemyShotAudio;
    private float distance;
    private EnemyShipsController enemyShipsController;

    public bool isActiveRightLevel1 = false;
    public bool isActiveRightLevel2 = false;
    public bool isActiveRightLevel3 = false;

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
        distance = Vector3.Distance(playerShip.transform.position, gameObject.transform.position);

        target = null;
        if (playerShip.IsDestroyed())
        {
            playerShip = null;
        }
        else
        {
            playerShip = GameObject.FindWithTag("Player");
        }


        if (distance <= 5f)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;


            timeSinceLastBurst += Time.deltaTime;
            if (timeSinceLastBurst >= timeBetweenBursts && shotsRemaining > 0)
            {


                /////// kod açılması gerekiyor   /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                SetActiveCannons();

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


    void SetActiveCannons()
    {
        if (isActiveRightLevel1 == true)
        {
            FireFunction(0);
        }

        if (isActiveRightLevel2 == true)
        {
            FireFunction(1);
        }
        if (isActiveRightLevel3 == true)
        {
            FireFunction(2);
        }

    }
    void FireFunction(int cannonNumber)
    {
        FireBullet(leftFirePoint[cannonNumber]);
        FireBullet(rightFirePoint[cannonNumber]);

    }
    void FireBullet(Transform firePoint)
    {


        var newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        newBullet.GetComponent<BulletController>().enemyBulletDamage = gameObject.GetComponent<EnemyLevelManager>().enemyDamage;
        //newBullet.GetComponent<BulletController>().SetVelocity(transform.up * bulletSpeed);
        Destroy(newBullet, 3f);
    }
}
