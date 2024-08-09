using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCrewHealthControl : MonoBehaviour
{
    //--------------------------------------------------------------VEYSEL BASLANGIC--------------------------------------------------------------
    public bool filledHealth = false;
    public List<GameObject> playerCrewHealth = new List<GameObject>();
    public float totalPlayerCurrentHealth;
    public float totalPlayerMaxHealth;

    public List<GameObject> enemyCrewHealth = new List<GameObject>();
    public float totalEnemyCurrentHealth;
    public float totalEnemyMaxHealth;

    public Image playerHealthSlider;
    public Image playerBackHealthSlider;

    public Image enemyHealthSlider;
    public Image enemyBackHealthSlider;

    public float lerpTimer; // Sağlık barının geçiş süresi
    private float chipSeed = 0.5f; // Sağlık azalma hızı
    //--------------------------------------------------------------VEYSEL BITIS--------------------------------------------------------------


    private void Start()
    {
        InitializePlayerCrewHealth();
        InitializeEnemyCrewHealth();
    }


    private void InitializePlayerCrewHealth()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerCrewHealth.Clear();
        foreach (GameObject player in players)
        {
            if (player.GetComponentInChildren<PlayerHealthBarControl>() != null)
            {
                playerCrewHealth.Add(player);
            }
        }
    }

    private void InitializeEnemyCrewHealth()
    {
        
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("EnemyShip");
        enemyCrewHealth.Clear();
        Debug.Log("Found " + enemys.Length + " enemies with tag 'EnemyShip'");

        foreach (GameObject enemy in enemys)
        {
            if (enemy.GetComponentInChildren<EnemyHealthBarControl>() != null)
            {
                enemyCrewHealth.Add(enemy);
                Debug.Log("Added enemy: " + enemy.name);
            }
            else
            {
                Debug.Log("Enemy " + enemy.name + " does not have EnemyHealthBarControl component");
            }
        }

        foreach (var enemyHealth in enemyCrewHealth)
        {
            if (enemyHealth.GetComponentInChildren<EnemyHealthBarControl>().health > 0)
            {
                totalEnemyMaxHealth += enemyHealth.GetComponentInChildren<EnemyHealthBarControl>().maxHealth;
            }
        }

        Debug.Log("Total enemies in list: " + enemyCrewHealth.Count);
    }

    private void Update()
    {
        CalculateHealth();
        UpdatePlayerCrewHealthUI();
        UpdateEnemyCrewHealthUI();


    }

    private void CalculateHealth()
    {
        totalPlayerCurrentHealth = 0;
        totalPlayerMaxHealth = 0;

        List<GameObject> playersToRemove = new List<GameObject>();
        foreach (var player in playerCrewHealth)
        {
            if (player.GetComponentInChildren<PlayerHealthBarControl>().health <= 0)
            {
                playersToRemove.Add(player);
            }
            else
            {
                totalPlayerCurrentHealth += player.GetComponentInChildren<PlayerHealthBarControl>().health;
                totalPlayerMaxHealth += player.GetComponentInChildren<PlayerHealthBarControl>().maxHealth;
            }
        }

        foreach (var player in playersToRemove)
        {
            playerCrewHealth.Remove(player);
        }

        totalEnemyCurrentHealth = 0;
        //totalEnemyMaxHealth = 0;

        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (var enemy in enemyCrewHealth)
        {
            if (enemy.GetComponentInChildren<EnemyHealthBarControl>().health <= 0)
            {
                
                enemiesToRemove.Add(enemy);
            }
            else
            {
                totalEnemyCurrentHealth += enemy.GetComponentInChildren<EnemyHealthBarControl>().health;
                //totalEnemyMaxHealth += enemy.GetComponentInChildren<EnemyHealthBarControl>().maxHealth;
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            enemyCrewHealth.Remove(enemy);
        }


    }



    public void UpdatePlayerCrewHealthUI()
    {
        float fillF = playerHealthSlider.fillAmount; // Sağlık barının doluluk oranını al
        float fillB = playerBackHealthSlider.fillAmount; // 2.Sağlık barının doluluk oranını al
        float hFraction = totalPlayerCurrentHealth / totalPlayerMaxHealth;
        if (fillB > hFraction)
        {
            playerHealthSlider.fillAmount = hFraction;
            playerBackHealthSlider.color = Color.red;
            lerpTimer += Time.deltaTime; // Zamanı güncelle
            float percentComplete = lerpTimer / chipSeed; // Yüzde tamamlama oranını hesapla
            percentComplete *= percentComplete; // Yüzde tamamlama oranını hesapla
            playerBackHealthSlider.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); // 2.Sağlık barını güncelle
        }
        //Canı arttırınca kullanılacak
        if (fillF < hFraction)
        {
            playerBackHealthSlider.color = Color.green; // 2.Sağlık barının rengini yeşil yap
            playerBackHealthSlider.fillAmount = hFraction; // 2.Sağlık barını güncelle
            lerpTimer += Time.deltaTime; // Zamanı güncelle
            float percentComplete = lerpTimer / chipSeed; // Yüzde tamamlama oranını hesapla
            percentComplete *= percentComplete; // Yüzde tamamlama oranını hesapla
            playerHealthSlider.fillAmount = Mathf.Lerp(fillF, playerBackHealthSlider.fillAmount, percentComplete); // Sağlık barını güncelle
        }
    }

    public void UpdateEnemyCrewHealthUI()
    {
        float fillF = enemyHealthSlider.fillAmount; // Sağlık barının doluluk oranını al
        float fillB = enemyBackHealthSlider.fillAmount; // 2.Sağlık barının doluluk oranını al
        float hFraction = totalEnemyCurrentHealth / totalEnemyMaxHealth;
        if (fillB > hFraction)
        {
            enemyHealthSlider.fillAmount = hFraction;
            enemyBackHealthSlider.color = Color.red;
            lerpTimer += Time.deltaTime; // Zamanı güncelle
            float percentComplete = lerpTimer / chipSeed; // Yüzde tamamlama oranını hesapla
            percentComplete *= percentComplete; // Yüzde tamamlama oranını hesapla
            enemyBackHealthSlider.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete); // 2.Sağlık barını güncelle
        }
        //Canı arttırınca kullanılacak
        //if (fillF < hFraction)
        //{

        //    enemyBackHealthSlider.color = Color.green; // 2.Sağlık barının rengini yeşil yap
        //    enemyBackHealthSlider.fillAmount = hFraction; // 2.Sağlık barını güncelle
        //    lerpTimer += Time.deltaTime; // Zamanı güncelle
        //    float percentComplete = lerpTimer / chipSeed; // Yüzde tamamlama oranını hesapla
        //    percentComplete *= percentComplete; // Yüzde tamamlama oranını hesapla
        //    enemyHealthSlider.fillAmount = Mathf.Lerp(fillF, enemyBackHealthSlider.fillAmount, percentComplete); // Sağlık barını güncelle
        //}


    }



}
