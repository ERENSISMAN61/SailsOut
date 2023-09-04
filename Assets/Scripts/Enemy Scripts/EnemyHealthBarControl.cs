using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarControl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 offset;
    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        
        
        slider.value = currentHealth / maxHealth;
    }
    private void Start()
    {
     //   target = gameObject.GetComponentInParent<>

    }
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }

}
