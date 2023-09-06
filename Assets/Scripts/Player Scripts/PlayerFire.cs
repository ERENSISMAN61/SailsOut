using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform firePoint;
    public float cannonballSpeed = 10f;

    void FireCannonball()
    {
        GameObject newCannonball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = newCannonball.GetComponent<Rigidbody>();

        // Set the velocity of the cannonball
        rb.velocity = firePoint.forward * cannonballSpeed;

        Destroy(newCannonball, 3f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireCannonball();
        }
    }
}
