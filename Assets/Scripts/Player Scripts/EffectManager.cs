using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    private Rigidbody rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        }

        if (explosionEffect == null)
        {
            Debug.LogError("Explosion Effect is not assigned in the inspector on " + gameObject.name);
        }
    }
    private void PlayExplosionEffect()
    {
        
        if (explosionEffect != null)
        {
            explosionEffect.Play();
        }
        else
        {
            Debug.LogError("Explosion Effect is not assigned in the inspector.");
        }
    }

    IEnumerator WaitEffectAndDestroy()
    {
        PlayExplosionEffect();
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyShip") /*|| other.gameObject.CompareTag("Ennemy")*/) // sonradan denenecek
        {

            if (rb != null)
            {
                rb.isKinematic = true;
                StartCoroutine(WaitEffectAndDestroy());
            }
            else
            {
                Debug.LogError("Rigidbody is missing on " + gameObject.name);
            }

        }
    }


}
