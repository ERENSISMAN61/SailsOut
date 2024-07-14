using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private Floater floater;
    private RedEnemyFire redEnemyFire;
    private redEnemySmoothMovement redEnemySmoothMovement;
    private EnemyHealthBarControl enemyHealthBarControl;
    private int waitDestroyTime = 15;
    void Start()
    {
        if (floater == null)
        {
            floater = gameObject.GetComponent<Floater>();    
            
        }
        if (enemyHealthBarControl == null)
        {
            enemyHealthBarControl = gameObject.GetComponentInChildren<EnemyHealthBarControl>();
        }
        if (redEnemyFire == null)
        {
            redEnemyFire = gameObject.GetComponentInParent<RedEnemyFire>();
        }
        if (redEnemySmoothMovement == null)
        {
            redEnemySmoothMovement = gameObject.GetComponentInParent<redEnemySmoothMovement>();
        }
        floater = gameObject.GetComponent<Floater>();    
        redEnemyFire = gameObject.GetComponentInParent<RedEnemyFire>();
        redEnemySmoothMovement = gameObject.GetComponentInParent<redEnemySmoothMovement>();
        enemyHealthBarControl = gameObject.GetComponentInChildren<EnemyHealthBarControl>();
    }

    IEnumerator DestroyEnemy()
    {
        floater.FloatingLevel -= Time.fixedDeltaTime*2;
        yield return new WaitForSeconds(waitDestroyTime);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyHealthBarControl.health <= 0.1f)
        {
            
            redEnemyFire.enabled = false;
            redEnemySmoothMovement.enabled = false;
            StartCoroutine(DestroyEnemy());

        }
    }
}
