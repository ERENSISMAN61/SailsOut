using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private bool isCoin = false;
    [SerializeField] private bool isSupply = false;
    [SerializeField] private bool isBullet = false;

    [SerializeField] private TextMeshProUGUI inventoryObjectsText;

    private InventoryController InventoryManager;
    private void Start()
    {
        InventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>();
    }

    private void Update()
    {
        if (isCoin)
        {
            inventoryObjectsText.text = InventoryManager.coinCount.ToString();
        }
        else if (isSupply)
        {
            inventoryObjectsText.text = InventoryManager.supplyCount.ToString();
        }
        else if (isBullet)
        {
            inventoryObjectsText.text = InventoryManager.bulletCount.ToString();
        }
    }


}
