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
            float Randomforce = Random.Range(5f, 10f);
            if (dotProduct > 0)
            {
                
                //- olcak
                rb.AddForceAtPosition(Vector3.left * Randomforce, parentPosition, ForceMode.Impulse);
                StartCoroutine(SwingObject(10, 1f)); // Sağa yumuşak sallanma
            }
            else
            {
                rb.AddForceAtPosition(Vector3.right * Randomforce, parentPosition, ForceMode.Impulse);
                StartCoroutine(SwingObject(-10, 1f)); // Sağa yumuşak sallanma
            }
        }
    }

    private IEnumerator SwingObject(float angle, float duration)
    {
        float elapsedTime = 0;
        float initialRotation = transform.eulerAngles.z;
        float targetRotation = initialRotation + angle;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float zRotation = Mathf.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation));
            yield return null;
        }

        // Geriye yumuşak sallanma
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float zRotation = Mathf.Lerp(targetRotation, initialRotation, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation));
            yield return null;
        }
    }
}
