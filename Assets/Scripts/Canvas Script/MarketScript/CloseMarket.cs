using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMarket : MonoBehaviour
{

    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMarketMenu();
        }
    }
    public void CloseMarketMenu()
    {
        Destroy(transform.parent.gameObject);
    }
}
