using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RedEnemyFire : MonoBehaviour
{
    [SerializeField]
    private GameObject playerShip; // Reference to the player ship GameObject
    public GameObject bulletPrefab; // Prefab for the enemy bullet
    public Transform[] rightFirePoint; // Array of right fire points for bullets
    public Transform[] leftFirePoint; // Array of left fire points for bullets
    public float bulletSpeed = 10f; // Speed of the enemy bullets
    public float firstFireTime = 3f; // Time before the first burst of bullets
    private float isStoppedTime; // Time before the first burst of bullets

    private int shotsRemaining; // Number of shots remaining in the current burst
    private float timeSinceLastBurst; // Time elapsed since the last burst
    public float timeBetweenBursts = 2f; // Time between consecutive bursts
    public int shotsPerBurst = 3; // Number of shots in each burst
    public float launchAngle = 45f; // Launch angle of the enemy bullets
    public float randomAngleValue = 2f; // Random angle value for bullet launch


    public float distance; // Distance at which the enemy engages the player
    private NavMeshAgent enemyAgent; // Reference to the NavMeshAgent component

    public bool isActiveRightLevel1 = false; // Flag for activating right fire level 1
    public bool isActiveRightLevel2 = false; // Flag for activating right fire level 2
    public bool isActiveRightLevel3 = false; // Flag for activating right fire level 3
    public float rotationSpeed = .008f; // Rotation speed of the enemy

    private redEnemySmoothMovement redEnemySmoothMovement; // Reference to the RedEnemySmoothMovement script
    [SerializeField]    
    private PlayerHealthBarControl healthOfPlayerShip;
    private EnemyHealthBarControl enemyHealthBar;

    AudioSource sourceAudioE; // Audio source for enemy sounds
    public AudioClip enemyShotAudio; // Audio clip for enemy shots

    void Start()
    {
        //healthOfPlayerShip = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponentInChildren<PlayerHealthBarControl>();
        enemyAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        //playerShip = GameObject.FindGameObjectWithTag("Player"); // Find and store the player ship GameObject
        sourceAudioE = GetComponent<AudioSource>(); // Get the AudioSource component
        redEnemySmoothMovement = GetComponent<redEnemySmoothMovement>(); // Get the RedEnemySmoothMovement script
        enemyHealthBar = GetComponentInChildren<EnemyHealthBarControl>();
        shotsRemaining = shotsPerBurst; // Initialize the shots remaining in the burst
        
    }

    void Update()
    {
        if (playerShip == null)
        {
            playerShip = GameObject.FindGameObjectWithTag("Player"); // Re-find the player ship if it is null
        }

        timeSinceLastBurst += Time.deltaTime; // Update the time elapsed since the last burst

        if (shotsRemaining <= 0)
        {
            shotsRemaining = shotsPerBurst; // Reset the shots remaining in the burst when it reaches zero
        }

        float distanceToMotherShip = Vector3.Distance(playerShip.transform.position, transform.position); // Calculate the distance to the player ship

        if (distanceToMotherShip < distance && healthOfPlayerShip.health > 0)
        {
            
            //Debug.Log("Player Caught");
            //Debug.Log("Distance to MotherShip: " + distanceToMotherShip);
            redEnemySmoothMovement.enabled = false; // Disable smooth movement script
            enemyAgent.isStopped = true; // Stop the NavMeshAgent
            
            RotateTowardsTarget2(playerShip.transform.position); // Rotate towards the player ship
            
            if (timeSinceLastBurst >= timeBetweenBursts && shotsRemaining > 0 && isStoppedTime > firstFireTime)
            {
                StartCoroutine(SetActiveCannonsEnumerator());
                sourceAudioE.PlayOneShot(enemyShotAudio); // Play the enemy shot audio

                shotsRemaining--;
                timeSinceLastBurst = 0f; // Reset the time since the last burst
                
                
            }
        }
        else
        {
            isStoppedTime = 0f; // Reset the time since the enemy stopped
            enemyAgent.isStopped = false; // Resume NavMeshAgent movement
            if(enemyHealthBar.health >= 0)
            {
                redEnemySmoothMovement.enabled = true; // Enable smooth movement script
            }
            
        }
    }

    private void RotateTowardsTarget2(Vector3 targetPosition)
    {
        float distanceCannonsToMotherShipRight = Vector3.Dot(playerShip.transform.position - transform.position, rightFirePoint[0].forward);
        float distanceCannonsToMotherShipLeft = Vector3.Dot(playerShip.transform.position - transform.position, leftFirePoint[0].forward);

        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Dönüş hızını artırmak için rotationSpeed * Time.deltaTime ile çarpın
        float step = rotationSpeed * Time.deltaTime;

        if (distanceCannonsToMotherShipRight < distanceCannonsToMotherShipLeft)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
            //transform.Rotate(new Vector3(0, 90 * rotationSpeed), Space.Self);
            transform.RotateAround(transform.position, transform.up, 90 * rotationSpeed);
            isStoppedTime += Time.deltaTime; // Update the time elapsed since the enemy stoppedD
            Debug.Log("Stopped Time: " + isStoppedTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
            //transform.Rotate(new Vector3(0, -90 * rotationSpeed), Space.Self);
            transform.RotateAround(transform.position, transform.up, -90 * rotationSpeed);
            isStoppedTime += Time.deltaTime; // Update the time elapsed since the enemy stopped
            Debug.Log("Stopped Time: " + isStoppedTime);
        }

    }

    IEnumerator SetActiveCannonsEnumerator()
    {
        yield return new WaitForSeconds(firstFireTime);
        SetActiveCannons();
    }
    void SetActiveCannons()
    {
        if (isActiveRightLevel1)
        {
            FireFunction(0);
        }

        if (isActiveRightLevel2)
        {
            FireFunction(1);
        }

        if (isActiveRightLevel3)
        {
            FireFunction(2);
        }
    }

    void FireFunction(int cannonNumber)
    {
        float distanceToMotherShip = Vector3.Distance(playerShip.transform.position, transform.position);
        float distanceCannonsToMotherShipRight = Vector3.Dot(playerShip.transform.position - transform.position, rightFirePoint[0].forward);
        float distanceCannonsToMotherShipLeft = Vector3.Dot(playerShip.transform.position - transform.position, leftFirePoint[0].forward);

        if (distanceToMotherShip < distance)
        {
            // Calculate the firing angle based on distance
            float modifiedLaunchAngle = launchAngle * (distanceToMotherShip / distance); // Modify launch angle based on distance

            if (distanceCannonsToMotherShipLeft > distanceCannonsToMotherShipRight)
            {
                FireBullet(leftFirePoint[cannonNumber], modifiedLaunchAngle); // Fire from the left cannon
            }
            else
            {
                FireBullet(rightFirePoint[cannonNumber], modifiedLaunchAngle); // Fire from the right cannon
            }
        }
    }

    void FireBullet(Transform firePoint, float modifiedLaunchAngle)
    {
        float distanceToMotherShip = Vector3.Distance(playerShip.transform.position, transform.position);
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        float randomAngle = Random.Range(-randomAngleValue, randomAngleValue); // Randomize the launch angle
        float radianAngle = Mathf.Deg2Rad * (modifiedLaunchAngle + randomAngle); // Use modified launch angle
        //Debug.Log("Adjusted Launch Angle: " + modifiedLaunchAngle);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        Vector3 launchDirection = (firePoint.forward * 2) + (Vector3.up * Mathf.Tan(radianAngle));
        rb.AddForce(launchDirection * bulletSpeed);
        Destroy(newBullet, 5f); // Destroy the bullet after 5 seconds
    }
}
