using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Rigidbody rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (rb == null ) 
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().transform.position;
            Vector3 toCollisionPoint = other.transform.position - parentPosition;
            float dotProduct = Vector3.Dot(transform.right, toCollisionPoint);
            Debug.Log("Dot: " + dotProduct);

            // Çarpma noktasını kullanarak kuvvet uygulayın
            Vector3 collisionPoint = other.transform.position;
            float Randomforce = Random.Range(0.5f, 2.1f);
            if (dotProduct > 0)
            {
                rb.AddForceAtPosition(new Vector3(Randomforce, 2f, Randomforce), parentPosition, ForceMode.Impulse);
            }
            else
            {
                rb.AddForceAtPosition(new Vector3(-Randomforce, 2f, -Randomforce), parentPosition, ForceMode.Impulse);
            }
        }
    }
}
