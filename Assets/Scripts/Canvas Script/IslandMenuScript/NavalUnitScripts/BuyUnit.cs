using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnit : MonoBehaviour
{
    private NavalUnitContainer _NavalUnitContainer;

    private UnitsManager _UnitsManager;
    public void BuyUnitFunction()
    {
        _UnitsManager = GameObject.FindGameObjectWithTag("UnitsManager").GetComponent<UnitsManager>();

        /*
        if (_UnitsManager == null)
        {
            Debug.LogError("QEUnitsManager bulunamadı!");
            return;
        }*/

        _NavalUnitContainer = gameObject.GetComponent<NavalUnitConfig>().GetNavalUnitContainer();

        /* if (_NavalUnitContainer == null)
         {
             Debug.LogError("QENavalUnitContainer bulunamadı!");
             return;
         }*/

        //unit olusturma
        UnitsContainer newUnit = new UnitsContainer(_NavalUnitContainer.GetRank(), _NavalUnitContainer.GetHealth(), _NavalUnitContainer.GetAttackPower());
        // unit ekleme
        _UnitsManager.UnitsContainers.Add(newUnit);
        // unit sayısını güncelleme
        _UnitsManager.unitCount = _UnitsManager.UnitsContainers.Count;

        //  Debug.Log("QEUnit added: Rank " + _NavalUnitContainer.GetRank() + ", Health " + _NavalUnitContainer.GetHealth() + ", Attack Power " + _NavalUnitContainer.GetAttackPower());
    }
}
