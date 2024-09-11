using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GetYourCountry : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject parentTransform;

    [SerializeField] private int selectedCountry;

    void Awake()
    {
        selectedCountry = PlayerPrefs.GetInt("SelectedCountry");
        Debug.Log("Your country is: " + PlayerPrefs.GetInt("SelectedCountry"));
    }

    private void MovePlayerToCountry()
    {
        int CountryNum = PlayerPrefs.GetInt("SelectedCountry");
        string CountryName = CountryNum.ToString();

        Debug.Log("Your Country Str: " + CountryName);

        //Transform parentTransform = GameObject.FindGameObjectWithTag(CountryName).transform;

        playerObj.transform.SetParent(parentTransform.transform);


    }

    public int GetCountryNum()
    {
        return selectedCountry;
    }
}
