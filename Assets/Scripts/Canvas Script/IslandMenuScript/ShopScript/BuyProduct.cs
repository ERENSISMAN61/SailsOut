using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProduct : MonoBehaviour
{
    private InventoryController inventoryController;

    [SerializeField] private bool isFlour = false;
    [SerializeField] private bool isMeat = false;
    [SerializeField] private bool isCheese = false;
    [SerializeField] private bool isOil = false;
    [SerializeField] private bool isLightBall = false;
    [SerializeField] private bool isMidBall = false;
    [SerializeField] private bool isHeavyBall = false;

    private void Start()
    {
        inventoryController = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>();
    }

    public void BuyProductt()
    {

        if (isFlour)
        {
            if (inventoryController.coinCount >= 20)
            {
                inventoryController.coinCount -= 20;
                inventoryController.supplyCount += 10;
            }
        }
        else if (isMeat)
        {
            if (inventoryController.coinCount >= 67)
            {
                inventoryController.coinCount -= 67;
                inventoryController.supplyCount += 35;
            }
        }
        else if (isCheese)
        {
            if (inventoryController.coinCount >= 34)
            {
                inventoryController.coinCount -= 34;
                inventoryController.supplyCount += 15;
            }
        }
        else if (isOil)
        {
            if (inventoryController.coinCount >= 51)
            {
                inventoryController.coinCount -= 51;
                inventoryController.supplyCount += 25;
            }
        }
        else if (isLightBall)
        {
            if (inventoryController.coinCount >= 5)
            {
                inventoryController.coinCount -= 5;
                inventoryController.bulletCount += 10;
            }
        }
        else if (isMidBall)
        {
            if (inventoryController.coinCount >= 15)
            {
                inventoryController.coinCount -= 15;
                inventoryController.bulletCount += 10;
            }
        }
        else if (isHeavyBall)
        {
            if (inventoryController.coinCount >= 30)
            {
                inventoryController.coinCount -= 30;
                inventoryController.bulletCount += 10;
            }
        }
    }

}
