using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 velocity; // Topun hareket hızı
    public float enemyBulletDamage ; // Topun verdiği hasar
    public float playerBulletDamage;
    [SerializeField]
    private ParticleSystem explosion;
    public void SetVelocity(Vector3 direction)
    {
        velocity = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyShip"))
        {
            Debug.Log("Cannon Ball düşman gemisine değdi");
            explosion.Play();
        }
    }
    void Update()
    {
        // Topu ileri yönde hareket ettir
        //transform.position += velocity * Time.deltaTime;
    }
}
