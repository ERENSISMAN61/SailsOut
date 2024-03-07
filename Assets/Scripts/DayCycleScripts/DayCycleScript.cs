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

    [Header("Sun Settings")]
    [SerializeField] private Light sun;
    [SerializeField] private float sunPosition;

    void Start()
    {
        UpdateTimeText();
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

    }

    void UpdateTimeText()
    {
        float hours = Mathf.FloorToInt(currentTime);
        float minutes = Mathf.FloorToInt((currentTime - hours) * 60f);

        currentTimeString = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    void UpdateLight()
    {
        sunPosition = currentTime / 24f;
        sun.transform.localRotation = Quaternion.Euler(new Vector3((sunPosition * 360f) - 90f, 170f, 0f));
    }
}
