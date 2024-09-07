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
    public TextMeshProUGUI countryInfo;
    // Action event tanımlama
    public event Action onButtonClicked;
    public bool isClickedShowInfo;

    [SerializeField]
    private string CountryName;
    [SerializeField]
    private string CountryInfo;

    
    public int countryID;
    [Obsolete]
    private CountryManager countryManager;

    [Obsolete]
    void Start()
    {
        countryManager = UnityEngine.Object.FindObjectOfType<CountryManager>();
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

    [Obsolete]
    private void PerformAnotherAction()
    {
        Debug.Log("Başka bir işlem yapılıyor...");
        countryName.text = CountryName;
        countryInfo.text = CountryInfo;
        Debug.Log("Country ID: " + countryID);
        countryManager.SaveCountry(countryID);

    }

    
}
