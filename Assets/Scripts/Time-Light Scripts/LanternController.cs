using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LanternController : MonoBehaviour
{
    private List<Light> lanternLights = new List<Light>();
    private TimeAndDateScript timeAndDateScript;

    void Start()
    {
        // Find the TimeAndDateScript component in the scene
        timeAndDateScript = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>();

        // Find all objects with the tag "Lantern" and get their Light components
        GameObject[] lanterns = GameObject.FindGameObjectsWithTag("Lantern");

        foreach (GameObject lantern in lanterns)
        {
            Light[] lights = lantern.GetComponentsInChildren<Light>();
            lanternLights.AddRange(lights);
        }
    }

    void Update()
    {
        // Get the current hour from the TimeAndDateScript
        float currentHour = timeAndDateScript.GetCurrentTime();

        // Enable or disable the Light component based on the current hour
        bool shouldEnable = currentHour >= 18f || currentHour < 6f;
        foreach (Light light in lanternLights)
        {
            light.enabled = shouldEnable;
        }
    }
}