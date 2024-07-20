using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroylessManager : MonoBehaviour
{
    public static DestroylessManager Instance;

    public float playerCurrentHealthDM;
    public float playerMaxHealthDM;
    public float playerCoinDM;
    public float playerBulletDM;
    public float playerSupplyDM;




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

    void Start()
    {
        UnitsContainer unit1 = new UnitsContainer(1, 100, 20);
        UnitsContainer unit2 = new UnitsContainer(2, 150, 40);
        UnitsContainer unit3 = new UnitsContainer(3, 200, 60);
        UnitsContainer unit4 = new UnitsContainer(4, 250, 80);
        UnitsContainer unit5 = new UnitsContainer(4, 250, 80);
        UnitsContainer unit6 = new UnitsContainer(5, 300, 100);
        UnitsContainer unit7 = new UnitsContainer(5, 300, 100);

        _UnitsContainers.Add(unit1);
        _UnitsContainers.Add(unit2);
        _UnitsContainers.Add(unit3);
        _UnitsContainers.Add(unit4);
        _UnitsContainers.Add(unit5);
        _UnitsContainers.Add(unit6);
        _UnitsContainers.Add(unit7);

        _unitCount = _UnitsContainers.Count;

    }


}
