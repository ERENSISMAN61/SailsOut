using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControl : MonoBehaviour
{
    
    private PlayerHealthBarControl playerHealthBarControl;
    private DestroylessManager destroylessManager;
    private void Start()
    {
        playerHealthBarControl = gameObject.GetComponentInChildren<PlayerHealthBarControl>();
        destroylessManager = GameObject.FindGameObjectWithTag("Destroyless").GetComponent<DestroylessManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            float RandomcannonValue = Random.Range(1f, 2.5f);
            playerHealthBarControl.health -= RandomcannonValue;
            playerHealthBarControl.lerpTimer = 0;
            destroylessManager.lerpTimer = 0;
        }
    }
}
