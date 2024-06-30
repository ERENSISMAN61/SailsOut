using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DateDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TimeAndDateScript timeManager;

    void Start()
    {
        timeManager.OnDateChanged.AddListener(UpdateDateText); //Subscribe to the event
        UpdateDateText();
    }

    void UpdateDateText()
    {
        dateText.text = timeManager.GetCurrentDateInfo();
    }
}
