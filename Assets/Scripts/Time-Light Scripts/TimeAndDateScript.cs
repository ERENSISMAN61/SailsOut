using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TimeAndDateScript : MonoBehaviour
{
    [Header("Time Settings")]

    [Range(0f, 24f)]
    [SerializeField] private float currentTime;
    [SerializeField] private string currentTimeString;
    [SerializeField] private float timeSpeed = 1f;
    private int timeSpeedMultiplier = 1;

    [Header("Day and Season Settings")]
    private int daysInSeason = 30;
    private string[] seasons = { "Spring", "Summer", "Autumn", "Winter" };
    [SerializeField] private int currentDay = 1;
    [SerializeField] private string currentSeason;


    [Header("Current Graph Time")] //For DayCycle Curve graph. x variable
    [SerializeField] private float currentGraphTime;
    void Start()
    {
        currentSeason = seasons[0];
        UpdateTimeText();
    }

    void Update()
    {
        UpdateTimeText();
        TimeCalculate();
        UpdateDayAndSeason();

        Debug.Log("Current Time: " + currentTime+" // " +currentTimeString+ "|  Current Day: " + currentDay + "|  Current Season: " + currentSeason);
        Debug.Log("timeSpeedMultiplier: " + timeSpeedMultiplier);
            if (Input.GetKeyDown(KeyCode.X))
            {
                timeSpeedMultiplier = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                timeSpeedMultiplier = 1;
            }
    }
    void TimeCalculate(){
        //Debug.Log("CurrTime: "+currentTime+"\n Actual Time: "+ Time.time);
        currentTime += timeSpeed * timeSpeedMultiplier * Time.deltaTime;
        
        if (currentTime >= 24f)
        {
            currentTime = 0f;
            currentDay++;
        }
    }
    public float GetCurrentTime(){
        return currentTime;
    }

        void UpdateTimeText()
    {
        float hours = Mathf.FloorToInt(currentTime);
        float minutes = Mathf.FloorToInt((currentTime - hours) * 60f);

        currentTimeString = string.Format("{0:00}:{1:00}", hours, minutes);

        currentGraphTime = currentTime/24f;
    }
    void UpdateDayAndSeason()
    {
        if (currentDay > daysInSeason)
        {
            currentDay = 1;
            int currentSeasonIndex = Array.IndexOf(seasons, currentSeason);
            int nextSeasonIndex = (currentSeasonIndex + 1) % seasons.Length;
            currentSeason = seasons[nextSeasonIndex];
        }
    }




}
