using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 velocity; // Topun hareket hızı
    public float enemyBulletDamage ; // Topun verdiği hasar
    public float playerBulletDamage;
    public void SetVelocity(Vector3 direction)
    {
        velocity = direction;
    }

    
}
