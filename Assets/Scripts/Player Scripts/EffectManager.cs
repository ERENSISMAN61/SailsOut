using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    private Rigidbody rb;
    public bool isExploded = false;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void PlayExplosionEffect()
    {
        explosionEffect.Play();
        
    }

    IEnumerator WaitEffectAndDestroy()
    {
        PlayExplosionEffect();
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyShip") /*|| other.gameObject.CompareTag("Ennemy")*/) // sonradan denenecek
        {
            rb.isKinematic = true;
            
            StartCoroutine(WaitEffectAndDestroy());
            
        }
    }
   

}
