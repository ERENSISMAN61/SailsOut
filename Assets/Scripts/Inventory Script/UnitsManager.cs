using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public int unitCount;
    public List<UnitsContainer> UnitsContainers = new List<UnitsContainer>();

    GameObject DestroylessObject;
    private void Start()
    {
        DestroylessObject = GameObject.FindGameObjectWithTag("Destroyless");

        UnitsContainers = DestroylessObject.GetComponent<DestroylessManager>()._UnitsContainers;
        unitCount = DestroylessObject.GetComponent<DestroylessManager>()._unitCount;
    }

    private void Update()
    {
        DestroylessObject.GetComponent<DestroylessManager>()._UnitsContainers = UnitsContainers;
        DestroylessObject.GetComponent<DestroylessManager>()._unitCount = unitCount;
    }

    //Listede none olarak görünse de hemen altında infoları görünüyor componentte.

    /*                           Unitleri alttaki şekilde çekebilirsin. Unit içindekiler -> "Rank, Health, AttackPower"


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int i = 0;
                foreach (UnitsContainer unit in UnitsContainers)
                {
                    Debug.Log("Units " + i + " Rank: " + unit.rank + "\nUnits " + i + " Health: " + unit.health + "\nUnits " + i + " Attack Power: " + unit.attackPower);
                    i++;
                }
            }

        }*/
}
