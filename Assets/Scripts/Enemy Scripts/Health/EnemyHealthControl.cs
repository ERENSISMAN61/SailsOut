using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthControl : MonoBehaviour
{
    
    private EnemyHealthBarControl enemyHealthBarControl;
    private GeneralCrewHealthControl generalCrewHealth;
    private float enemycannonValue = 5f;
    void Start()
    {
        if (enemyHealthBarControl == null)
        {
            enemyHealthBarControl = gameObject.GetComponentInChildren<EnemyHealthBarControl>();
        }

        if (generalCrewHealth == null)
        {
            generalCrewHealth = GameObject.FindGameObjectWithTag("GeneralCrewHealth").GetComponent<GeneralCrewHealthControl>();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            float RandomcannonValue = Random.Range(1f, 2.5f);
            enemyHealthBarControl.health -= RandomcannonValue;
            Debug.Log("Enemy Health: " + enemyHealthBarControl.health);
            enemyHealthBarControl.lerpTimer = 0;
            generalCrewHealth.lerpTimer = 0;
            //Destroy(other.gameObject);

            
        }
    }
}
