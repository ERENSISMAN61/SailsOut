using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenuScript : MonoBehaviour
{
    private GameObject marketObject;
    private GameObject spawnMarketPrefab;
    private GameObject playerObject;

    public bool canMarketOpen = false; //�ehirler aras� market a�t���nda hatalar ��kabilir. ��karsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool de�eri true yaparak iki bool ile ��z�lebilir

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
            
            // playerObject.GetComponent<SmoothPlayerMovement>().isMarketOpened = true;   // e�er market instanciate edilmeyen bir sistem yaparsam sadece marketi kapa a� sistem� yaparsam bunlar�n de�i�mesi
            ////        MARKET �C�N S�L�ND� MENU DE KULLANILDI CUNKU                                                                         //gerekir. �u anl�k bu �ekilde yapt�m.

            onceOpened = true;

            canMarketOpen = false;

        }




    }

    private void LateUpdate()
    {
        if (onceOpened && spawnMarketPrefab == null) //market bir kere a��lm��sa ve kapat�lm��sa
        {

            Debug.Log("Market is closed");
       //     GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = false;
        //    playerObject.GetComponent<SmoothPlayerMovement>().isMarketOpened = false;                                // MARKET �C�N S�L�ND� MENU DE KULLANILDI CUNKU
            isMarketOpen = false;

            onceOpened = false;
        }


    }

    public void SetMenuTrue()
    {
        canMarketOpen = true;
    }


}
