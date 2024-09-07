using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryManager : MonoBehaviour
{
    private const string CountryKey = "SelectedCountry";
    private static CountryManager _instance;

    // Property to access the singleton instance
    [System.Obsolete]
    public static CountryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find existing instance or create a new one
                _instance = FindObjectOfType<CountryManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject("CountryManager");
                    _instance = singleton.AddComponent<CountryManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }


    // Method to save the selected country ID
    public void SaveCountry(int countryId)
    {
        PlayerPrefs.SetInt(CountryKey, countryId);
        PlayerPrefs.Save();

    }

    // Method to load the selected country ID
    public int LoadCountry()
    {
        return PlayerPrefs.GetInt(CountryKey); // Default to 1 (Norvheim) if not set
    }

    
}
