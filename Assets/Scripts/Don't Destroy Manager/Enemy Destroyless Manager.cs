using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroylessManager : MonoBehaviour
{
    public static EnemyDestroylessManager Instance;


    public List<UnitsContainer> _EnemyToFightUnitsContainers = new List<UnitsContainer>();


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
