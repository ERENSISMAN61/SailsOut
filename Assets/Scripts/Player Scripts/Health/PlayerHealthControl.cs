using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControl : MonoBehaviour
{
    
    private PlayerHealthBarControl playerHealthBarControl;
    private GeneralCrewHealthControl generalCrewHealth;
    private CameraShakeControl cameraShakeControl;
    public bool isShotted = false;
    private float shakingTime;
    private void Start()
    {
        playerHealthBarControl = gameObject.GetComponentInChildren<PlayerHealthBarControl>();
        generalCrewHealth = GameObject.FindGameObjectWithTag("GeneralCrewHealth").GetComponent<GeneralCrewHealthControl>();
        cameraShakeControl = gameObject.GetComponent<CameraShakeControl>();
        shakingTime = cameraShakeControl.ShakeTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            isShotted = true;
            float RandomcannonValue = Random.Range(1f, 2.5f);
            playerHealthBarControl.health -= RandomcannonValue;
            playerHealthBarControl.lerpTimer = 0;
            generalCrewHealth.lerpTimer = 0;
            StartCoroutine(ResetIsShotted());
        }
    }

    private IEnumerator ResetIsShotted()
    {
        yield return new WaitForSeconds(shakingTime); // Adjust the delay as needed
        isShotted = false;
    }

    private void Update()
    {
        Debug.Log("isshotted: " + isShotted);
    }
}
