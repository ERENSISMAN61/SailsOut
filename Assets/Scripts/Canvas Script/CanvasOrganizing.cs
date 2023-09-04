using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOrganizing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas market;
    private bool isMarketOpen = false;
    void Start()
    {
        market.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMarket();
        }
    }

    void ToggleMarket()
    {
        isMarketOpen = !isMarketOpen;
        market.enabled = isMarketOpen;
    }
}
