using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyDialog : MonoBehaviour
{
    GameObject InventoryObject;


    public bool didPay = false;
    public bool didSteal = false;

    void Start()
    {


        InventoryObject = GameObject.FindGameObjectWithTag("Inventory");
    }
    public void AttackButton(string sceneName)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 1;   //SÝLÝNECEK ////////////////////////////////////////////////////////////////////////////////////////
        Time.timeScale = 1; //SÝLÝNEbilir belki ////////////////////////////////////////////////////////////////////////////////////////    
        SceneManager.LoadScene(sceneName);
    }

    public void PayButton()
    {
        if (!didPay)
        {


            InventoryObject.GetComponent<InventoryController>().coinCount -= 30;

            didPay = true;
            gameObject.GetComponent<PatrolScript>().StartCoroutine("WaitCatch");
        }

    }

    public void SurrenderButton()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().targetCamera = gameObject.transform;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        if (!didSteal)
        {
            InventoryObject.GetComponent<InventoryController>().bulletCount = 0;
            int randomNumberSupply = Random.Range(5, 11);
            InventoryObject.GetComponent<InventoryController>().supplyCount = InventoryObject.GetComponent<InventoryController>().supplyCount * randomNumberSupply / 100f;
            int randomNumberCoin = Random.Range(5, 11);
            InventoryObject.GetComponent<InventoryController>().coinCount = InventoryObject.GetComponent<InventoryController>().coinCount * randomNumberCoin / 100f;

            didSteal = true;
        }
        gameObject.GetComponent<PatrolScript>().StartCoroutine("Surrendered");

    }

}
