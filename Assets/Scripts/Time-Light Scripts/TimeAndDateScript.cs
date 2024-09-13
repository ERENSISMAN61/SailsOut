using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TimeAndDateScript : MonoBehaviour
{
    [Header("Time Settings")]

    [Range(0f, 24f)]
    [SerializeField] private float currentTime;
    [SerializeField] private string currentTimeString;
    [SerializeField] private int timeSpeed = 1; //only showing time speed in inspector
    [SerializeField] private float timeSpeedMultiplier = 1;

    [Header("Day and Season Settings")]
    private int daysInSeason = 16;
    private string[] seasons = { "Spring", "Summer", "Autumn", "Winter" };
    [SerializeField] private int currentDay = 1;
    [SerializeField] private string currentSeason;
    [SerializeField] private int currentYear = 1530;

    public UnityEvent OnDateChanged; //Event for updating UI


    [Header("Current Graph Time")] //For DayCycle Curve graph. x variable
    [SerializeField] private float currentGraphTime;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            currentTime = GameObject.FindGameObjectWithTag("Destroyless").GetComponent<DestroylessManager>().sceneCurrentTime;

        }

        currentSeason = seasons[0];
        UpdateTimeText();

        OnDateChanged.Invoke(); // Oyunun başlangıcında tarih değişikliği olayını tetikleyin      Invoke
    }

    void Update()
    {
        UpdateTimeText();
        TimeCalculate();
        UpdateDayAndSeasonAndYear();

        Debug.Log("Current Time: " + currentTime + " // " + currentTimeString + "|  Current Day: " + currentDay + "|  Current Season: " + currentSeason + "|  Current Year: " + currentYear);

        if ((currentTime > 18 && currentTime < 24) || (currentTime > 0 && currentTime < 5))
        {
            timeSpeedMultiplier = 0.3f;
        }
        else
        {
            timeSpeedMultiplier = 0.1f;
        }
    }
    void TimeCalculate()
    {
        //Debug.Log("CurrTime: "+currentTime+"\n Actual Time: "+ Time.time);

        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            currentTime += timeSpeedMultiplier * Time.deltaTime * 0.1f;

        }
        else
        {
            currentTime += timeSpeedMultiplier * Time.deltaTime;
        }


        if (currentTime >= 24f)
        {
            currentTime = 0f;
            currentDay++;

            OnDateChanged.Invoke();   //Event for updating UI                                       Invoke
        }
    }
    public float GetCurrentTime()
    {
        return currentTime;
    }

    void UpdateTimeText()
    {
        float hours = Mathf.FloorToInt(currentTime);
        float minutes = Mathf.FloorToInt((currentTime - hours) * 60f);

        currentTimeString = string.Format("{0:00}:{1:00}", hours, minutes);

        currentGraphTime = currentTime / 24f;
    }
    void UpdateDayAndSeasonAndYear()
    {
        if (currentDay > daysInSeason)
        {
            currentDay = 1; // Yeni mevsimin ilk gününe geç
            int currentSeasonIndex = Array.IndexOf(seasons, currentSeason);
            int nextSeasonIndex = (currentSeasonIndex + 1) % seasons.Length;
            currentSeason = seasons[nextSeasonIndex];

            if (currentSeason == "Spring")
            {
                currentYear++;
            }

            OnDateChanged.Invoke(); // Mevsim değişikliğinden sonra UI güncellemesi için olayı tetikle
        }

    }

    public float GetCurrTime()
    {
        return currentTime;
    }


    public string GetCurrentDateInfo()
    {
        return $"{currentDay} {currentSeason} {currentYear}";
    }

    public void SetTimeSpeed(int speedMultiplier)
    {
        Time.timeScale = speedMultiplier;
        //   timeSpeedMultiplier = speedMultiplier;

        timeSpeed = speedMultiplier; // only showing time speed in inspector  
    }

    public int GetTimeSpeed()
    {

        return timeSpeed;
    }

}
