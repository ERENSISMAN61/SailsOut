using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogCoinControl : MonoBehaviour
{
    [SerializeField] private GameObject notEnoghCoinText;
    [SerializeField] private Button payButton;
    private GameObject InventoryObject;


    private float coinCount;// dialog acildiginda kac liramiz oldugu

    [SerializeField] private float payCount = 30; //kac lira odeyecegi

    private bool canPay = true;

    void Start()
    {
        InventoryObject = GameObject.FindGameObjectWithTag("Inventory");

        coinCount = InventoryObject.GetComponent<InventoryController>().coinCount;

        if (coinCount < payCount)
        {
            canPay = false;
            notEnoghCoinText.SetActive(true);
            payButton.interactable = false;

        }
        else
        {
            canPay = true;
            notEnoghCoinText.SetActive(false);
            payButton.interactable = true;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
