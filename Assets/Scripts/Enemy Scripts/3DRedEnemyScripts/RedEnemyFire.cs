using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedEnemyFire : MonoBehaviour
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
    public float distanceBetweenofShips = 20f;

    public float launchAngle;

    AudioSource sourceAudioE; //Audio
    public AudioClip enemyShotAudio;
    private float distance;
    private EnemyMovement enemyShipsController;

    public bool isActiveRightLevel1 = false;
    public bool isActiveRightLevel2 = false;
    public bool isActiveRightLevel3 = false;
    //private BattlePatrolScript battlePatrolScript;


    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        //battlePatrolScript = gameObject.GetComponent<BattlePatrolScript>();
        sourceAudioE = gameObject.GetComponent<AudioSource>(); // Audio
        enemyShipsController = gameObject.GetComponent<EnemyMovement>();

        shotsRemaining = shotsPerBurst;



    }

    // Update is called once per frame
    private void Update()
    {
        //if( battlePatrolScript.isScriptWorking == true )
        //{
        //    gameObject.GetComponent<BattlePatrolScript>().enabled = true;
        //}
        //else
        //{
        //    gameObject.GetComponent<BattlePatrolScript>().enabled = false;
        //}
        if (playerShip == null)
        {
            return;
        }
        else
        {
            playerShip = GameObject.FindGameObjectWithTag("Player");
        }

        
        distance = Vector3.Distance(playerShip.transform.position, gameObject.transform.position);
        
        RotateTowardsTarget(playerShip.transform.position);
        
        if (distance <= distanceBetweenofShips)
        {
            
            //transform.rotation = Quaternion.LookRotation(target.position - transform.position);
            

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

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 3 * Time.time);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
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
        //FireBullet(leftFirePoint[cannonNumber]);
        FireBullet(rightFirePoint[cannonNumber]);

    }
    void FireBullet(Transform firePoint)
    {


        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        float radianAngle = Mathf.Deg2Rad * launchAngle;
        //newBullet.GetComponent<BulletController>().enemyBulletDamage = gameObject.GetComponent<EnemyLevelManager>().enemyDamage;
        //newBullet.GetComponent<BulletController>().SetVelocity(transform.up * bulletSpeed);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        // Ateş etme yönünü hesapla
        Vector3 launchDirection = (firePoint.forward * 2) + (Vector3.up * Mathf.Tan(radianAngle));

        rb.velocity = launchDirection * bulletSpeed;
        Destroy(newBullet, 3f);
    }
}
