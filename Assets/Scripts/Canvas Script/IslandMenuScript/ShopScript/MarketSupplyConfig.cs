using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketSupplyConfig : MonoBehaviour
{

    [SerializeField] private MarketSupplyContainer _MarketSupplyContainer;

    [SerializeField] private TextMeshProUGUI _supplyName;

    [SerializeField] private Image _supplyImg;

    [SerializeField] private TextMeshProUGUI _supplyCost;

    [SerializeField] private TextMeshProUGUI _supplyAmount;


    public void Config()
    {
        //myMarketSupplyCard = new MarketSupplyCard(_MarketSupplyContainer.Img, _MarketSupplyContainer.Cost, _MarketSupplyContainer.Rank);

        _supplyImg.sprite = _MarketSupplyContainer.GetImg();
        _supplyName.text = _MarketSupplyContainer.GetSupplyName();
        _supplyCost.text = _MarketSupplyContainer.GetCost().ToString();
        _supplyAmount.text = _MarketSupplyContainer.GetSupplyAmount().ToString();


    }

    public void SetMarketSupplyContainer(MarketSupplyContainer _MarketSupplyContainer)
    {
        this._MarketSupplyContainer = _MarketSupplyContainer;
        Config();
    }

    public MarketSupplyContainer GetMarketSupplyContainer()
    {
        return _MarketSupplyContainer;
    }


}

//public class MarketSupplyCard
//{
//    public Sprite _img;

//    public float _cost;

//    public float _rank;

//    public MarketSupplyCard(Sprite _img, float _cost, float _rank)
//    {
//        this._img = _img;
//        this._cost = _cost;
//        this._rank = _rank;
//    }

//}
