using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI işlemleri için gerekli

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private string sceneName = "ErenScene"; // Varsayılan sahne adı, Inspector üzerinden değiştirilebilir.
    [SerializeField] private Text resultText; // UI'da sonucu göstermek için Text bileşeni

    private GeneralCrewHealthControl generalHealth;

    void Start()
    {
        // GeneralCrewHealth objesini bul ve bileşenini al
        generalHealth = GameObject.FindGameObjectWithTag("GeneralCrewHealth").GetComponent<GeneralCrewHealthControl>();

        // Oyuncu veya düşman sağlığını kontrol et
        CheckGameStatus();
    }

    void CheckGameStatus()
    {
        if (generalHealth.totalPlayerCurrentHealth <= 0)
        {
            // Oyuncuların tümü yok edildi
            DisplayResult("Red Team Won");
        }
        else if (generalHealth.totalEnemyCurrentHealth <= 0)
        {
            // Düşmanların tümü yok edildi
            DisplayResult("Blue Team Won");
        }
    }

    void DisplayResult(string message)
    {
        if (resultText != null)
        {
            resultText.text = message; // UI'da sonucu göster
        }
        else
        {
            Debug.LogWarning("Result Text component is not assigned!");
        }

        Invoke("LoadScene", 2f); // 2 saniye sonra sahneyi yükle
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName); // Sahne yükleme
    }
}
