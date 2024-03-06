using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruimentUnitScript : MonoBehaviour
{
    [SerializeField] private NavalUnitContainer[] Units;

    public NavalUnitContainer GetUnits(int index)
    {
        if (index >= 0 && index < Units.Length)
        {
            return Units[index];
        }
        else
        {
            Debug.LogError("Ge�ersiz dizi indeksi: " + index);
            return null;
        }
    }

    public int GetUnitCount()
    {
        return Units.Length;
    }

}
