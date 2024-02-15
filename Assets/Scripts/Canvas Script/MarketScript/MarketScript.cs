using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketScript : MonoBehaviour
{
    private GameObject marketObject;
    private GameObject spawnMarketPrefab;

    public bool canMarketOpen = false; //�ehirler aras� market a�t���nda hatalar ��kabilir. ��karsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool de�eri true yaparak iki bool ile ��z�lebilir

    public bool isMarketOpen = false;
    void Start()
    {
        marketObject = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Market Prefab/MarketCanvas");

    }
    void Update()
    {
        if (canMarketOpen)
        {
            spawnMarketPrefab = Instantiate(marketObject, GameObject.Find("Canvas").transform);
            canMarketOpen = false;
        }


    }

    //public void SetMenuTrue()
    //{
    //    canMarketOpen = true;
    //}


}
