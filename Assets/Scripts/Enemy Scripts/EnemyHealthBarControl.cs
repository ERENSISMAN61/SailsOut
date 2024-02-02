using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarControl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject mainCam;
    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        
        
        slider.value = currentHealth / maxHealth;
    }
    private void Start()
    {
     //   target = gameObject.GetComponentInParent<>
     
        
        //mainCam = Camera.main;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position); // look at camera

        transform.position = target.position + offset;
    }

}
