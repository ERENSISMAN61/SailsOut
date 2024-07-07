using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarControl : MonoBehaviour
{
    public float health;
    public float maxHealth;


    [SerializeField] private Image healthSlider;
    [SerializeField] private Image bacHealthSlider;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject mainCam;
    public float lerpTimer =0; // Sağlık barının geçiş süresi
    [SerializeField]
    private float chipSeed; // Sağlık azalma hızı
    private void Start()
    {
        //   target = gameObject.GetComponentInParent<>

        maxHealth = 100f;                                                                  /// TASINACAK
        health = 100f;
        updateHealthBar(health, maxHealth);
        //mainCam = Camera.main;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position); // look at camera

        transform.position = target.position + offset;
        UpdateHealthUI();
        updateHealthBar(health, maxHealth);
        
    }
    public void updateHealthBar(float currentHealth, float maxHealth)
    {

        if (currentHealth > 0 && maxHealth > 0)  // HATA VERMEMESI ICIN IF EKLENDI
        {
            float healthSliderAmount = currentHealth / maxHealth;
            //healthSlider.fillAmount = Mathf.Lerp(healthSlider.fillAmount, healthSliderAmount, 0f); // Sağlık barını güncelle

            //healthSlider.fillAmount = healthSliderAmount;
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = healthSlider.fillAmount; // Sağlık barının doluluk oranını al
        float fillB = bacHealthSlider.fillAmount; // 2.Sağlık barının doluluk oranını al
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            healthSlider.fillAmount = hFraction;
            bacHealthSlider.color = Color.red;
            lerpTimer += Time.deltaTime; // Zamanı güncelle
            float percentComplete = lerpTimer / chipSeed; // Yüzde tamamlama oranını hesapla
            percentComplete *= percentComplete; // Yüzde tamamlama oranını hesapla
            bacHealthSlider.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); // 2.Sağlık barını güncelle
        }
        //Canı arttırınca kullanılacak
        if (fillF < hFraction)
        {

            bacHealthSlider.color = Color.green; // 2.Sağlık barının rengini yeşil yap
            bacHealthSlider.fillAmount = hFraction; // 2.Sağlık barını güncelle
            lerpTimer += Time.deltaTime; // Zamanı güncelle
            float percentComplete = lerpTimer / chipSeed; // Yüzde tamamlama oranını hesapla
            percentComplete *= percentComplete; // Yüzde tamamlama oranını hesapla
            healthSlider.fillAmount = Mathf.Lerp(fillF, bacHealthSlider.fillAmount, percentComplete); // Sağlık barını güncelle
        }
    }

    


   
}
