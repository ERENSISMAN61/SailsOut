using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 spawnVector;
    public GameObject cannonBall;
    public float newSpawnTime = 2.0f;


    public static Spawner instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        spawnVector = transform.position;
    }

    void SpawnCannonBall()
    {
        Instantiate(cannonBall, spawnVector, Quaternion.identity);   
    }


    void newSpawnRequest()
    {
        Invoke("SpawnCannonBall", newSpawnTime);
    }

}
