using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMarket : MonoBehaviour
{

    public bool didCloseMarket = false;
    private void Start()
    {
        
    }
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMarketMenu();
        }
    }
    public void CloseMarketMenu()
    {
        didCloseMarket = true;

        Destroy(transform.parent.parent.gameObject);


    }
}
