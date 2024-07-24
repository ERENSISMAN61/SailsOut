using UnityEngine;


[CreateAssetMenu(fileName = "New Market Supply Type", menuName = "Market Supply Type")]
public class MarketSupplyContainer : ScriptableObject
{
    [SerializeField] private string supplyName;

    [SerializeField] private Sprite Img;

    [SerializeField] private float Cost;

    [SerializeField] private float supplyAmount;




    public Sprite GetImg()
    {
        return Img;
    }

    public float GetCost()
    {
        return Cost;
    }

    public float GetSupplyAmount()
    {
        return supplyAmount;
    }

    public string GetSupplyName()
    {
        return supplyName;
    }
}
