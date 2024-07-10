using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnit : MonoBehaviour
{
    private NavalUnitContainer _NavalUnitContainer;

    private UnitsManager _UnitsManager;

    private GridLGRowCountFinder _gridLGRowCountFinder;


    public void BuyUnitFunction()
    {
        _UnitsManager = GameObject.FindGameObjectWithTag("UnitsManager").GetComponent<UnitsManager>();

        _NavalUnitContainer = gameObject.GetComponent<NavalUnitConfig>().GetNavalUnitContainer();

        _gridLGRowCountFinder = transform.parent.GetComponent<GridLGRowCountFinder>();

        if (GameObject.FindWithTag("Inventory").GetComponent<InventoryController>().coinCount >= _NavalUnitContainer.GetCost())
        {
            GameObject.FindWithTag("Inventory").GetComponent<InventoryController>().coinCount -= _NavalUnitContainer.GetCost(); // coin azaltma

            //unit olusturma
            UnitsContainer newUnit = new UnitsContainer(_NavalUnitContainer.GetRank(), _NavalUnitContainer.GetHealth(), _NavalUnitContainer.GetAttackPower());
            // unit ekleme
            _UnitsManager.UnitsContainers.Add(newUnit);
            // unit sayısını güncelleme
            _UnitsManager.unitCount = _UnitsManager.UnitsContainers.Count;

            _gridLGRowCountFinder.RemoveProduct(transform.GetSiblingIndex());// remove product from grid layout group

            //  Debug.Log("QEUnit added: Rank " + _NavalUnitContainer.GetRank() + ", Health " + _NavalUnitContainer.GetHealth() + ", Attack Power " + _NavalUnitContainer.GetAttackPower());
        }
        else
        {
            return;
        }

    }
}
