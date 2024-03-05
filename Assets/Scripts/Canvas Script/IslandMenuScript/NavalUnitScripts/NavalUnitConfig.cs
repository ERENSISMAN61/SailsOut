using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavalUnitConfig : MonoBehaviour
{

    [SerializeField] private NavalUnitContainer _NavalUnitContainer;

    //private NavalUnitCard myNavalUnitCard;

    [SerializeField] private Image unitImg;

    [SerializeField] private TextMeshProUGUI unitCost;

    [SerializeField] private TextMeshProUGUI unitRank;

    public void Config()
    {
        //myNavalUnitCard = new NavalUnitCard(_NavalUnitContainer.Img, _NavalUnitContainer.Cost, _NavalUnitContainer.Rank);

        unitImg.sprite = _NavalUnitContainer.GetImg();
        unitCost.text = _NavalUnitContainer.GetCost().ToString();
        unitRank.text = _NavalUnitContainer.GetRank().ToString();

        Debug.Log("Cost:"+unitCost.text);
        Debug.Log("Rank:"+unitRank.text);
        //Debug.Log("Img:"+unitImg.sprite);
    }



}

//public class NavalUnitCard
//{
//    public Sprite _img;

//    public float _cost;

//    public float _rank;

//    public NavalUnitCard(Sprite _img, float _cost, float _rank)
//    {
//        this._img = _img;
//        this._cost = _cost;
//        this._rank = _rank;
//    }

//}
