using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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



    void Start()
    {
        UpdateTimeText();
        EnablePlanet();
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

    }
    void OnValidate()
    {
        UpdateTimeText();
        UpdateLight();
        EnablePlanet();
    }
    void UpdateTimeText()
    {
        float hours = Mathf.FloorToInt(currentTime);
        float minutes = Mathf.FloorToInt((currentTime - hours) * 60f);

        currentTimeString = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        
        sun.transform.rotation = Quaternion.Euler(sunRotation - 90f, -90f,0f);
        moon.transform.rotation = Quaternion.Euler(sunRotation + 90f, -90f, 0f);


        float sunIntensityMultiplier = sunIntensityCurve.Evaluate(currentTime / 24f);
        float moonIntensityMultiplier = sunIntensityCurve.Evaluate(currentTime / 24f);

        HDAdditionalLightData sunData = sun.GetComponent<HDAdditionalLightData>();
        HDAdditionalLightData moonData = moon.GetComponent<HDAdditionalLightData>();

        if (sunData != null)
        {
            sunData.intensity = sunIntensity * sunIntensityMultiplier;
        }
        if (moonData != null)
        {
            moonData.intensity = moonIntensity * moonIntensityMultiplier;
        }

        //float temperature = lightTemperature.Evaluate(currentTime / 24f);
        //Light light = sun.GetComponent<Light>();

        //if (light != null)
        //{
        //    light.colorTemperature = temperature * 10000f;
        //}

    }

    void EnablePlanet()
    {
        if (currentTime >= 5.7f && currentTime <= 18.3f)
        {
            sun.gameObject.SetActive(true);
            isSunEnable = true;
        }
        else
        {
            sun.gameObject.SetActive(false);
            isSunEnable = false;
        }

        if (currentTime >= 6.3f && currentTime <= 17.7f)
        {
            moon.gameObject.SetActive(false);
            isMoonEnable = false;
        }
        else
        {
            moon.gameObject.SetActive(true);
            isMoonEnable = true;
        }
    }
}
