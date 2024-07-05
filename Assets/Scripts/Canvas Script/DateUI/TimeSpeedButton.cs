using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedButton : MonoBehaviour
{

    [SerializeField] private TimeAndDateScript timeManager;

    public void SetTime0x()
    {
        timeManager.SetTimeSpeed(0);
    }
    public void SetTime1x()
    {
        timeManager.SetTimeSpeed(1);
    }

    public void SetTime2x()
    {
        timeManager.SetTimeSpeed(2);
    }
}
