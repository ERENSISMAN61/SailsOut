using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnits : MonoBehaviour
{
    private UnitsContainer unit1 = new UnitsContainer(1, 100, 20);
    private UnitsContainer unit2 = new UnitsContainer(2, 150, 40);
    private UnitsContainer unit3 = new UnitsContainer(3, 200, 60);
    private UnitsContainer unit4 = new UnitsContainer(4, 250, 80);
    private UnitsContainer unit5 = new UnitsContainer(5, 300, 100);

    [SerializeField] private List<UnitsContainer> _EnemyUnitsContainers = new List<UnitsContainer>();

    void Start()
    {
        int RandomUnitCount = Random.Range(1, 6);

        for (int i = 0; i < RandomUnitCount; i++)
        {
            int RandomUnit = Random.Range(1, 5);  //1-5 arasinda rastgele bir sayi sec
            switch (RandomUnit)
            {
                case 1:
                    _EnemyUnitsContainers.Add(unit1);
                    break;
                case 2:
                    _EnemyUnitsContainers.Add(unit2);
                    break;
                case 3:
                    _EnemyUnitsContainers.Add(unit3);
                    break;
                case 4:
                    _EnemyUnitsContainers.Add(unit4);
                    break;
                case 5:
                    _EnemyUnitsContainers.Add(unit5);
                    break;
            }
        }

    }

    public int GetEnemyUnitCount()
    {
        return _EnemyUnitsContainers.Count;
    }

    public List<UnitsContainer> GetEnemyUnits()
    {
        return _EnemyUnitsContainers;
    }
}
