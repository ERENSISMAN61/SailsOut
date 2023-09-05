using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarControl : MonoBehaviour
{

    public float health;
    public float maxHealth;
    private GameObject DestroylessObject;



    [SerializeField] private Image healthSlider;
    [SerializeField] private Image hungerSlider;
    [SerializeField] private Image bacHealthSlider;

    public float lerpTimer; // Sağlık barının geçiş süresi
    private float chipSeed = 2f; // Sağlık azalma hızı
    public float hunger = 100f; // Açlık
    public float maxHunger = 100f; // Maksimum açlık
    public float hungerReducingTime; // Açlık azalma hızı
    private float lastReducingTime; // Son azalma zamanı
    private float lastIncreaseHealthTime; // son Sağlık artırma zamanı
    [SerializeField] public GameObject inventory; // Envanter
    private float supplyCount_; // Supply sayısı
    public float increaseHunger = 10; // Açlık artırma miktarı
    public float decreaseHunger = 10; // Açlık azaltma miktarı
    public float decreaseSupplyCount = 10; // Supply sayısını azaltma miktarı
    public float decreaseHealthforHunger = 10f; // Sağlık azaltma miktarı
    public float increaseHealthforHunger = 10f; // Sağlık artırma miktarı
    public float hungerTime = 60f; // her 60 sn de bir açlık azalma süresi
    public float healthReducing_HungerTime = 30f; // açlık 0 olduğunda her 30 sn de bir sağlık azalma süresi
    public float healthIncreasing_HungerTime = 30f; // açlık 0 olduğunda her 30 sn de bir sağlık artma süresi


    private void Start()
    {

        DestroylessObject = GameObject.FindGameObjectWithTag("Destroyless");                   /// TASINACAK
        if (!DestroylessObject.GetComponent<DestroylessManager>().filledHealth)                /// TASINACAK
        {

            DestroylessObject.GetComponent<DestroylessManager>().playerMaxHealthDM = 100f;     /// TASINACAK
            DestroylessObject.GetComponent<DestroylessManager>().playerCurrentHealthDM = 100f;  /// TASINACAK

            maxHealth = 100f;                                                                  /// TASINACAK
            health = 100f;                                                                     /// TASINACAK

            DestroylessObject.GetComponent<DestroylessManager>().filledHealth = true;          /// TASINACAK
        }


        maxHealth = DestroylessObject.GetComponent<DestroylessManager>().playerMaxHealthDM;    /// TASINACAK
        health = DestroylessObject.GetComponent<DestroylessManager>().playerCurrentHealthDM;   /// TASINACAK


        //health = maxHealth;    ///////  acılmayacak \\\\\\

        updateHealthBar(health, maxHealth);





        lastReducingTime = Time.time; // Başlangıç zamanını ayarla
        lastIncreaseHealthTime = Time.time; // Başlangıç zamanını ayarla
        inventory = GameObject.FindGameObjectWithTag("Inventory"); // Envanteri bul
        if(hunger <= 0) // Eğer açlık 0 ve altındaysa
        {
            hungerReducingTime = Time.time;// Başlangıç zamanını ayarla
        }
        
    }

    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        if(currentHealth>0 && maxHealth >0)  // HATA VERMEMESI ICIN IF EKLENDI
        {
            float healthSliderAmount = currentHealth / maxHealth;
            healthSlider.fillAmount = Mathf.Lerp(healthSlider.fillAmount, healthSliderAmount, 0f); // Sağlık barını güncelle

            //healthSlider.fillAmount = healthSliderAmount;
        }


    }

    public void updateHungerBar(float currentHunger, float maxHunger)
    {
        
        hungerSlider.fillAmount = currentHunger / maxHunger; // Açlık barını güncelle
    }

    public void UpdateHealthUI()
    {
        float fillF = healthSlider.fillAmount; // Sağlık barının doluluk oranını al
        float fillB = bacHealthSlider.fillAmount; // 2.Sağlık barının doluluk oranını al
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
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


    private void FixedUpdate()
    {
        DestroylessObject.GetComponent<DestroylessManager>().playerCurrentHealthDM = health;  /// TASINACAK
        DestroylessObject.GetComponent<DestroylessManager>().playerMaxHealthDM = maxHealth;   /// TASINACAK

    }
    private void Update()
    {
        UpdateHealthUI();
        updateHealthBar(health, maxHealth); // Sağlık barını güncelle

        supplyCount_ = inventory.GetComponent<InventoryController>().supplyCount; // Inventory'den supplyCount değerini al
        if (Time.time - lastReducingTime >= hungerTime) // 1 dakika geçti mi?
        {
            hunger -= decreaseHunger; // Açlık barını azalt
            // eğer açlık 0'dan küçükse
            if (hunger < 0)
            {
                hunger = 0; // Açlık barını sıfırla
            }

            updateHungerBar(hunger, maxHunger); // Açlık barını azalt
            lastReducingTime = Time.time; // Zamanı güncelle
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Eğer Q tuşuna basıldıysa
        {
            if (supplyCount_ >= 10 && hunger < 100) // Eğer envanterde 10 tane supply varsa
            {
                hunger += increaseHunger; // Açlık barını artır
                inventory.GetComponent<InventoryController>().supplyCount -= decreaseSupplyCount; // Envanterdeki supply sayısını azalt
                updateHungerBar(hunger, maxHunger); // Açlık barını güncelle
            }

        }

        if(hunger <= 0 ) // Eğer açlık 0'dan küçükse
        {
            
            if (Time.time - hungerReducingTime >= healthReducing_HungerTime) // yarım dakika geçti mi?
            {
                health -= decreaseHealthforHunger;// Sağlık değişkenini azalt
                lerpTimer = 0f; // Zamanı sıfırla
                hungerReducingTime = Time.time; // Zamanı güncelle
            }
        }

        if(hunger == maxHunger) // Eğer açlık max açlığa eşit ise
        {
            //Eğer sağlık max sağlıktan küçük ise
            if (health < maxHealth)
            {
                // Eğer 30 saniye geçtiyse
                if (Time.time - lastIncreaseHealthTime >= healthIncreasing_HungerTime)
                {

                    health += increaseHealthforHunger;// Sağlık değişkenini azalt
                    lerpTimer = 0f; // Zamanı sıfırla
                    lastIncreaseHealthTime = Time.time; // Zamanı güncelle
                    
                    
                }
            }
        }
        
        


    }





}
