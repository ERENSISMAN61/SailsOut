using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenuScript : MonoBehaviour
{
    private GameObject marketObject;
    private GameObject spawnMarketPrefab;
    private GameObject playerObject;

    public bool canMarketOpen = false; //þehirler arasý market açtýðýnda hatalar çýkabilir. çýkarsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool deðeri true yaparak iki bool ile çözülebilir

    public bool isMarketOpen = false;

    private bool onceOpened = false;
    void Start()
    {
        marketObject = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Menu Prefab/MarketCanvas");
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if (canMarketOpen)
        {
            spawnMarketPrefab = Instantiate(marketObject, GameObject.Find("Canvas").transform);


            isMarketOpen = true;

            //GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = true;
            
            // playerObject.GetComponent<SmoothPlayerMovement>().isMarketOpened = true;   // eðer market instanciate edilmeyen bir sistem yaparsam sadece marketi kapa aç sistemþ yaparsam bunlarýn deðiþmesi
            ////        MARKET ÝCÝN SÝLÝNDÝ MENU DE KULLANILDI CUNKU                                                                         //gerekir. þu anlýk bu þekilde yaptým.

            onceOpened = true;

            canMarketOpen = false;

        }




    }

    private void LateUpdate()
    {
        if (onceOpened && spawnMarketPrefab == null) //market bir kere açýlmýþsa ve kapatýlmýþsa
        {

            Debug.Log("Market is closed");
       //     GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = false;
        //    playerObject.GetComponent<SmoothPlayerMovement>().isMarketOpened = false;                                // MARKET ÝCÝN SÝLÝNDÝ MENU DE KULLANILDI CUNKU
            isMarketOpen = false;

            onceOpened = false;
        }


    }

    public void SetMenuTrue()
    {
        canMarketOpen = true;
    }


}
