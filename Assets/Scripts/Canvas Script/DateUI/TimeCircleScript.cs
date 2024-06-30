using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCircleScript : MonoBehaviour
{
    [SerializeField] private TimeAndDateScript timeAndDateScript;


    void Update()
    {

        transform.localEulerAngles = new Vector3(0, 0, 180 + (timeAndDateScript.GetCurrentTime() * 15f));
        
    }
}
