using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject[] itemDrops;

    public float supplyCount = 0;  //number "0"

    public float coinCount = 0;    //number "1"

    public float bulletCount = 0;  //number "2"

    GameObject DestroylessObject;
    private void Start()
    {
        DestroylessObject = GameObject.FindGameObjectWithTag("Destroyless");

        supplyCount = DestroylessObject.GetComponent<DestroylessManager>().playerSupplyDM;
        coinCount = DestroylessObject.GetComponent<DestroylessManager>().playerCoinDM;
        bulletCount = DestroylessObject.GetComponent<DestroylessManager>().playerBulletDM;


    }
    public void Update()
    {
        DestroylessObject.GetComponent<DestroylessManager>().playerSupplyDM = supplyCount;
        DestroylessObject.GetComponent<DestroylessManager>().playerCoinDM = coinCount;
        DestroylessObject.GetComponent<DestroylessManager>().playerBulletDM = bulletCount;
    }
    public void ItemDrop()
    {

        int randomCount = Random.Range(1, 4); // kaç item düþeceðini belirler
        for (int i = 0; i < randomCount; i++) //düþecek item kadar fonksiyonu çalýþtýrýr.
        {
            int randomNumber = Random.Range(0, 3);  // hangi itemdan  düþeceðini belirler. her "for"  döngüsünde tekrar çalýþýr bu sayede farklý itemlar ayný anda düþme ihtimali vardýr.

            if (randomNumber == 0)
            {
                supplyCount += 60;         // -------------------þu anlýk 60 . deðiþecek tekrardan ---------------------------------------------------------------------------
            }
            else if (randomNumber == 1)
            {
                coinCount += 10;
            }
            else if (randomNumber == 2)
            {
                bulletCount += 10;
            }
        }

    }



}
