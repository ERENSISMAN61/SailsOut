using UnityEngine;


[CreateAssetMenu(fileName = "New Naval Unit Type", menuName = "Naval Unit Type")]
public class NavalUnitContainer : ScriptableObject
{
    [SerializeField]private Sprite Img;

    [SerializeField] private float Cost;

    [SerializeField] private float Rank;

    


    public Sprite GetImg()
    {
        return Img;
    }

    public float GetCost()
    {
        return Cost;
    }

    public float GetRank()
    {
        return Rank;
    }
    
}
