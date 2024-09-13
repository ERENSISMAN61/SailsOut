using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTime : MonoBehaviour
{
    private TimeAndDateScript timeDate;
    private DestroylessManager destroylessManager;

    void Awake()
    {
        // timeDate = UnityEngine.Object.FindObjectOfType<TimeAndDateScript>();
        ////     destroylessManager = UnityEngine.Object.FindObjectOfType<DestroylessManager>();
        //   Debug.Log("Time Date: " + destroylessManager.sceneCurrentTime);
        //    timeDate.SetCurrentTime(destroylessManager.sceneCurrentTime);
    }
}
