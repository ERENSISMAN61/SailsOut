using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidScript : MonoBehaviour
{
    [SerializeField] private float increaseSpeed = 0.03f; // Slider'ın artış hızı (değer/saniye)

    private int decreaseBulletAmount = 10; // Bullet sayısının azalma miktarı
    [SerializeField] private int minDecreaseBullet = 5; // Bullet sayısının azalma miktarı
    [SerializeField] private int maxDecreaseBullet = 15; // Bullet sayısının azalma miktarı

    [SerializeField] private TextMeshProUGUI RaidFinishedText;
    private float faceDilateSpeed = 0.5f;

    bool hasTriggered8 = false;
    bool hasTriggered16 = false;
    bool hasTriggered24 = false;

    [SerializeField] private GameObject LootObject;
    private int IncreaseSupplyAmount = 10; //loottan cikacak supply miktarı
    private int IncreaseCoinAmount = 10; //loottan cikacak coin miktarı

    [SerializeField] private int minIncreaseSupply = 5; //loottan cikacak supply miktarı
    [SerializeField] private int maxIncreaseSupply = 15; //loottan cikacak supply miktarı

    [SerializeField] private int minIncreaseCoin = 5; //loottan cikacak coin miktarı
    [SerializeField] private int maxIncreaseCoin = 15; //loottan cikacak coin miktarı

    private bool canLootGenerate = true;


    void Start()
    {
        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(2);  //ZAMANI HIZLANDIR

    }
    void Update()
    {

        gameObject.GetComponent<Slider>().value += increaseSpeed * Time.deltaTime;

        RandomDecreaseBullet();



        //particle effect. Canvas Screen Space - Camera olmalı
        //   [SerializeField] private GameObject fxObject;
        // fxObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -gameObject.GetComponent<Slider>().value * 360));

        FinishRaid();

    }

    void FinishRaid()
    {
        //Raid Sonlandığında
        if (gameObject.GetComponent<Slider>().value >= 1)
        {
            GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(0); //ZAMANI DURDUR
            //Face'teki Dilate'i arttırarak 0 yapıcaz.
            if (RaidFinishedText.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate) < 0)
            {
                RaidFinishedText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, RaidFinishedText.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate) + (faceDilateSpeed * Time.unscaledDeltaTime));
            }
            else
            {
                RaidFinishedText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0);

                LootObject.SetActive(true);

                if (canLootGenerate)
                {
                    RandomLoot();
                    GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>().IncreaseSupplyCount(IncreaseSupplyAmount);
                    GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>().IncreaseCoinCount(IncreaseCoinAmount);

                    canLootGenerate = false;
                }

                LootObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text =
                IncreaseSupplyAmount + "\n" + IncreaseCoinAmount;
            }
        }

    }

    void RandomLoot()
    {
        IncreaseSupplyAmount = Random.Range(minIncreaseSupply, maxIncreaseSupply);

        IncreaseCoinAmount = Random.Range(minIncreaseCoin, maxIncreaseCoin);


    }

    void RandomDecreaseBullet()
    {
        //DECREASE BULLET COUNT
        float currentTime = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().GetCurrentTime();
        float tolerance = 0.1f; // Zaman toleransı

        // Zamanın belirli değerlere yakın olup olmadığını kontrol et
        bool isTimeCloseTo8 = Mathf.Abs(currentTime - 8f) < tolerance;
        bool isTimeCloseTo16 = Mathf.Abs(currentTime - 16f) < tolerance;
        bool isTimeCloseTo24 = Mathf.Abs(currentTime - 24f) < tolerance;

        // Zaman dilimlerine göre bayrakları kontrol et ve ayarla
        if (isTimeCloseTo8 && !hasTriggered8)
        {
            TriggerEvent();
            hasTriggered8 = true;
        }
        else if (!isTimeCloseTo8)
        {
            hasTriggered8 = false;
        }

        if (isTimeCloseTo16 && !hasTriggered16)
        {
            TriggerEvent();
            hasTriggered16 = true;
        }
        else if (!isTimeCloseTo16)
        {
            hasTriggered16 = false;
        }

        if (isTimeCloseTo24 && !hasTriggered24)
        {
            TriggerEvent();
            hasTriggered24 = true;
        }
        else if (!isTimeCloseTo24)
        {
            hasTriggered24 = false;
        }
    }

    void TriggerEvent()
    {
        Debug.Log("BBV Bullet Decrease");
        decreaseBulletAmount = Random.Range(minDecreaseBullet, maxDecreaseBullet);
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>().DecreaseBulletCount(decreaseBulletAmount);
    }

    public void DoneButton()
    {

        GameObject.FindGameObjectWithTag("Player").GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = true;
        Destroy(gameObject);
    }

}
