using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketGoods : MonoBehaviour
{

     private GameObject InventoryObject;

    InventoryController inventoryController;

    private void Start()
    {
        InventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        inventoryController = InventoryObject.GetComponent<InventoryController>();
        //InventoryController inventoryController = FindFirstObjectByType<InventoryController>();
    }
    public void buyButtonSuplly()
    {
        if (inventoryController.coinCount >= 50)
        {
            inventoryController.coinCount -= 50;
            inventoryController.supplyCount += 20;
        }
    }

    public void sellButtonSuplly()
    {
        if (inventoryController.supplyCount > 20)
        {
            inventoryController.coinCount += 50;
            inventoryController.supplyCount -= 20;
        }

    }

    public void buyButtonBullet()
    {
        if (inventoryController.coinCount >= 50)
        {
            inventoryController.coinCount -= 50;
            inventoryController.bulletCount += 20;
        }
    }
    public void sellButtonBullet()
    {
        if (inventoryController.bulletCount > 20)
        {
            inventoryController.coinCount += 50;
            inventoryController.bulletCount -= 20;
        }


    }
}
