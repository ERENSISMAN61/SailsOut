using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroylessManager : MonoBehaviour
{
    public static DestroylessManager Instance;

    public float playerCurrentHealthDM;
    public float playerMaxHealthDM;
    public float playerCoinDM;
    public float playerBulletDM;
    public float playerSupplyDM;

    public bool filledHealth = false;

    public int _unitCount;
    public List<UnitsContainer> _UnitsContainers = new List<UnitsContainer>();

    private void Awake()
    {

        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(this.gameObject);


        }

        else if (Instance != this)
        {
            Destroy(this.gameObject);


        }



    }

}
