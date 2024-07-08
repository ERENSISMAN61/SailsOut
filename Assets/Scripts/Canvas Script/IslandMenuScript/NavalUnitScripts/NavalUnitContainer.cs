using UnityEngine;


[CreateAssetMenu(fileName = "New Naval Unit Type", menuName = "Naval Unit Type")]
public class NavalUnitContainer : ScriptableObject
{
    [SerializeField] private Sprite Img;

    [SerializeField] private float Cost;

    [SerializeField] private float Rank;

    [SerializeField] private float Health;

    [SerializeField] private float AttackPower;

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

    public float GetHealth()
    {
        return Health;
    }

    public float GetAttackPower()
    {
        return AttackPower;
    }

}
