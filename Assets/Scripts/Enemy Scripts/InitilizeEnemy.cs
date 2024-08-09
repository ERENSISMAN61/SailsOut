using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitilizeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyDestroylessManager destroylessManager;
    public GameObject enemyPrefab;
    public Transform initlizePoint;
    void Awake()
    {
        destroylessManager = GameObject.FindGameObjectWithTag("Destroyless").GetComponent<EnemyDestroylessManager>();
        
        for(int i = 0; i < destroylessManager._EnemyToFightUnitsContainers.Count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, initlizePoint.position, Quaternion.identity);
            initlizePoint.position += new Vector3(100f, 0f, 0f);
        }
        //GameObject enemyPrefab = Instantiate(enemyPrefab, initlizePoint.position, Quaternion.identity);
    }

   
}
