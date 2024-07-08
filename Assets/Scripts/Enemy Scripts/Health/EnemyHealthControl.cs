using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour
{
    
    private EnemyHealthBarControl enemyHealthBarControl;
    private float enemycannonValue = 5f;
    void Start()
    {
        if (enemyHealthBarControl == null)
        {
            enemyHealthBarControl = gameObject.GetComponentInChildren<EnemyHealthBarControl>();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by bullet");
            enemyHealthBarControl.health -= 5f;
            enemyHealthBarControl.lerpTimer = 0;

            
        }
    }
}
