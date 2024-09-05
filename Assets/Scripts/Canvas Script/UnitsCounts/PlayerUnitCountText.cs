using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitCountText : MonoBehaviour
{
    private UnitsManager unitsManager;
    [SerializeField] private GameObject playerUnitCountText;
    void Start()
    {
        unitsManager = GameObject.FindGameObjectWithTag("UnitsManager").GetComponent<UnitsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUnitCountText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (unitsManager.unitCount + 1).ToString();
    }
}
