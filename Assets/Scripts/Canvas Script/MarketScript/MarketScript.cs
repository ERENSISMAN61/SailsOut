using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketScript : MonoBehaviour
{
    private GameObject marketObject;
    private GameObject spawnMarketPrefab;

    public bool isMarketOpen = false; //þehirler arasý market açtýðýnda hatalar çýkabilir. çýkarsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool deðeri true yaparak iki bool ile çözülebilir
    void Start()
    {
        marketObject = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Market Prefab/MarketCanvas");

    }
    void Update()
    {
        if (isMarketOpen)
        {
            spawnMarketPrefab = Instantiate(marketObject, GameObject.Find("Canvas").transform);
            isMarketOpen = false;
        }


    }

    //public void SetMenuTrue()
    //{
    //    isMarketOpen = true;
    //}


}
