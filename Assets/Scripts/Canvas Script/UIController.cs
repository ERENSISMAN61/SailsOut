using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    private GameObject InventoryObj;
    public float bulletCount_ = 0;
    public Text bulletText;

    public float coinCount_ = 0;
    public Text coinText;

    public float supplyCount_ = 0;
    public Text supplyText;
    private void Start()
    {
        InventoryObj = GameObject.FindGameObjectWithTag("Inventory");
    }
    void Update()
    {
        bulletCount_ = InventoryObj.GetComponent<InventoryController>().bulletCount;
        //bulletText.text = String.Format("Bullet: {0}", bulletCount_);
        bulletText.text = bulletCount_.ToString();


        coinCount_ = InventoryObj.GetComponent<InventoryController>().coinCount;
        //coinText.text = String.Format("Coin: {0}", coinCount_);
        coinText.text = coinCount_.ToString();

        supplyCount_ = InventoryObj.GetComponent<InventoryController>().supplyCount;
        //supplyText.text = String.Format("Supply: {0}", supplyCount_);
        supplyText.text = supplyCount_.ToString();

    }
}
