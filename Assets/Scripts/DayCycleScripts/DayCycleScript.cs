using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayCycleScript : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0f, 24f)]
    [SerializeField] private float currentTime;
    [SerializeField] private float timeSpeed = 1f;

    [Header("CurrentTime")]
    [SerializeField] private string currentTimeString;



    //[SerializeField] private bool isDay;
    [SerializeField] private bool isSunEnable;
    [SerializeField] private bool isMoonEnable;
    [SerializeField] private bool isNightLightEnable;

    [Header("CurrentGraphTime")]
    [SerializeField] private float currentGraphTime;

    [Header("Sun Settings")]
    [SerializeField] private Light sun;
    [SerializeField] private float sunPosition = 1f;

    [SerializeField] private float sunIntensity = 1f;
    [SerializeField] private AnimationCurve sunIntensityCurve;

    //[SerializeField] private AnimationCurve lightTemperature;
    //shadow settings de var

    [Header("Moon Settings")]
    [SerializeField] private Light moon;

    [SerializeField] private float moonIntensity = 1f;
    [SerializeField] private AnimationCurve moonIntensityCurve;

    [Header("Night Ground Light Settings")]
    [SerializeField] private Light nightLight;

    [SerializeField] private float nightLightIntensity = 1f;
    [SerializeField] private AnimationCurve nightLightIntensityCurve;

    [Header("Stars")]
    [SerializeField] VolumeProfile volumeProfile;
                     private PhysicallyBasedSky skySettings;
    [SerializeField] private float starsIntensity = 1f;
    [SerializeField] private AnimationCurve starsIntensityCurve;


    [Header("Sky Exposure")]
    [SerializeField] private float exposure = 1f;
    [SerializeField] private AnimationCurve exposureCurve;
    private Exposure skyExposure;




    void Start()
    {
        UpdateTimeText();
        EnablePlanet();
        UpdateStarredSky();
        UpdateExposure();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += timeSpeed * Time.deltaTime;
        
        if (currentTime >= 24f)
        {
            currentTime = 0f;
        }

        UpdateTimeText();
        UpdateLight();

        EnablePlanet();
        UpdateStarredSky();
        UpdateExposure();
    }
    void OnValidate()
    {
        UpdateTimeText();
        UpdateLight();
        EnablePlanet();
        UpdateStarredSky();
        UpdateExposure();
    }
    void UpdateTimeText()
    {
        float hours = Mathf.FloorToInt(currentTime);
        float minutes = Mathf.FloorToInt((currentTime - hours) * 60f);

        currentTimeString = string.Format("{0:00}:{1:00}", hours, minutes);

        currentGraphTime = currentTime/24f;
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        
        sun.transform.rotation = Quaternion.Euler(sunRotation - 90f, -90f,0f);
        moon.transform.rotation = Quaternion.Euler(sunRotation + 90f, -90f, 0f);


        float sunIntensityMultiplier = sunIntensityCurve.Evaluate(currentTime / 24f);
        float moonIntensityMultiplier = moonIntensityCurve.Evaluate(currentTime / 24f);
        float nightLightIntensityMultiplier = nightLightIntensityCurve.Evaluate(currentTime / 24f);

        HDAdditionalLightData sunData = sun.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData moonData = moon.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData nightLightData = nightLight.GetComponent<HDAdditionalLightData>();

        if (sunData != null)
        {
            sunData.intensity = sunIntensity * sunIntensityMultiplier;
        }
        if (moonData != null)
        {
            moonData.intensity = moonIntensity * moonIntensityMultiplier;
        }
        if (nightLightData != null)
        {
            nightLightData.intensity = nightLightIntensity * nightLightIntensityMultiplier;
        }

        //float temperature = lightTemperature.Evaluate(currentTime / 24f);
        //Light light = sun.GetComponent<Light>();

        //if (light != null)
        //{
        //    light.colorTemperature = temperature * 10000f;
        //}

    }

    void EnablePlanet() // AND SHADOW
    {
        HDAdditionalLightData sunData = sun.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData moonData = moon.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData nightLightData = nightLight.GetComponent<HDAdditionalLightData>();

        if (Input.GetKeyDown(KeyCode.H))
        {
            sunData.EnableShadows(true);
            nightLightData.EnableShadows(true);
            moonData.EnableShadows(true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            sunData.EnableShadows(false);
            nightLightData.EnableShadows(true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            sunData.EnableShadows(true);
            nightLightData.EnableShadows(false);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            sunData.EnableShadows(false);
         moonData.EnableShadows(true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            moonData.EnableShadows(false);
         sunData.EnableShadows(true);
        }


        if (currentTime >= 5.7f && currentTime <= 18.3f)
        {
            sunData.EnableShadows(true);
            moonData.EnableShadows(false);

            sun.gameObject.SetActive(true);
            isSunEnable = true;
        }
        else
        {
            sunData.EnableShadows(false);
            moonData.EnableShadows(true);

            sun.gameObject.SetActive(false);
            isSunEnable = false;
        }

        if (currentTime >= 7f && currentTime <= 17f)
        {
            moon.gameObject.SetActive(false);
            isMoonEnable = false;
        }
        else
        {
            moon.gameObject.SetActive(true);
            isMoonEnable = true;
        }

        if (currentTime >= 8f && currentTime <= 14f)
        {
            nightLight.gameObject.SetActive(false);
            isNightLightEnable = false;

        }
        else
        {
            nightLight.gameObject.SetActive(true);
            isNightLightEnable = true;
        }
    }

    void UpdateStarredSky()
    {
        volumeProfile.TryGet<PhysicallyBasedSky>(out skySettings);
        skySettings.spaceEmissionMultiplier.value = starsIntensityCurve.Evaluate(currentTime / 24f)* starsIntensity;

    }

    void UpdateExposure()
    {
        volumeProfile.TryGet<Exposure>(out skyExposure);
        skyExposure.fixedExposure.value = exposureCurve.Evaluate(currentTime / 24f)* exposure;

    }




}
