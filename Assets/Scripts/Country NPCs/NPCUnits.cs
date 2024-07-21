using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUnits : MonoBehaviour
{
    private UnitsContainer unit1 = new UnitsContainer(1, 100, 20);
    private UnitsContainer unit2 = new UnitsContainer(2, 150, 40);
    private UnitsContainer unit3 = new UnitsContainer(3, 200, 60);
    private UnitsContainer unit4 = new UnitsContainer(4, 250, 80);
    private UnitsContainer unit5 = new UnitsContainer(5, 300, 100);

    [SerializeField] private int rank1UnitCount, rank2UnitCount, rank3UnitCount, rank4UnitCount, rank5UnitCount;

    [SerializeField] private List<UnitsContainer> _NPCUnitsContainers = new List<UnitsContainer>();

    void Start()
    {


        for (int i = 0; i < rank1UnitCount; i++)
        {
            _NPCUnitsContainers.Add(unit1);
        }

        for (int i = 0; i < rank2UnitCount; i++)
        {
            _NPCUnitsContainers.Add(unit2);
        }

        for (int i = 0; i < rank3UnitCount; i++)
        {
            _NPCUnitsContainers.Add(unit3);
        }

        for (int i = 0; i < rank4UnitCount; i++)
        {
            _NPCUnitsContainers.Add(unit4);
        }

        for (int i = 0; i < rank5UnitCount; i++)
        {
            _NPCUnitsContainers.Add(unit5);
        }

    }

    public int GetNPCUnitCount()
    {
        return _NPCUnitsContainers.Count;
    }

    public List<UnitsContainer> GetNPCUnits()
    {
        return _NPCUnitsContainers;
    }
}
