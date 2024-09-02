using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyEffectManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionEffect;
    private Rigidbody rb;

    private void Start()
    {
        GameObject explosionObject = GameObject.FindGameObjectWithTag("ExplosionEffect");
        if (explosionObject != null)
        {
            explosionEffect = explosionObject.GetComponent<ParticleSystem>();
        }
        else
        {
            Debug.LogError("ExplosionEffect tag'li obje bulunamadı!");
        }

        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void PlayExplosionEffect()
    {
        if (explosionEffect != null)
        {
            explosionEffect.Play();
        }
        else
        {
            Debug.LogError("ExplosionEffect atanmış değil!");
        }
    }

    IEnumerator WaitEffectAndDestroy()
    {
        PlayExplosionEffect();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            rb.isKinematic = true;
            StartCoroutine(WaitEffectAndDestroy());
        }
    }


}
