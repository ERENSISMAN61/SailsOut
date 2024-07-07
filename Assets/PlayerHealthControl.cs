using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControl : MonoBehaviour
{
    
    private PlayerHealthBarControl playerHealthBarControl;
    private float cannonValue = 10f;
    private void Start()
    {
        playerHealthBarControl = gameObject.GetComponentInChildren<PlayerHealthBarControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            playerHealthBarControl.health -= cannonValue;
        }
    }
}
