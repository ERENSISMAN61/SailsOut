using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ShowInfoButton : MonoBehaviour
{
    public Button myButton;
    public TextMeshProUGUI countryName;
    // Action event tanımlama
    public event Action onButtonClicked;
    public bool isClickedShowInfo;

    [SerializeField]
    private string CountryName;
    private string CountryInfo;

    void Start()
    {
        // Button'un onClick event'ine bir lambda fonksiyon ekleyin
        myButton.onClick.AddListener(() => onButtonClicked?.Invoke());
    }

    void OnEnable()
    {
        // Dinlemek istediğiniz farklı fonksiyonları burada Action'a ekleyebilirsiniz
        onButtonClicked += LogButtonClickedTrue;
        onButtonClicked += PerformAnotherAction;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        onButtonClicked -= LogButtonClickedFalse;
        onButtonClicked -= PerformAnotherAction;
    }

    private void LogButtonClickedTrue()
    {
        Debug.Log("Button tıklandı!");
        isClickedShowInfo = true;
    }

    private void LogButtonClickedFalse()
    {
        Debug.Log("Button tıklandı!");
        isClickedShowInfo = false;
    }

    private void PerformAnotherAction()
    {
        Debug.Log("Başka bir işlem yapılıyor...");
        countryName.text = CountryName;
    }
}
