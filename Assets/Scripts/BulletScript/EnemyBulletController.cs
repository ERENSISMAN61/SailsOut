using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    
    public float enemyBulletDamage; // Topun verdiği hasar
    public float playerBulletDamage;
    [SerializeField]
    private ParticleSystem explosion;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cannon Ball player gemisine değdi");
            explosion.Play();
        }
    }
    
}
