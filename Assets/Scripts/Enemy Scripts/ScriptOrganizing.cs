using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptOrganizing : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<EnemyFire>().enabled = false;
        gameObject.GetComponent<PatrolScript>().enabled = false;
        gameObject.GetComponent<BattlePatrolScript>().enabled = false;
        gameObject.GetComponent<EnemyDialog>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "VeyselScene")
        {
            gameObject.GetComponent<EnemyFire>().enabled = false;
            gameObject.GetComponent<PatrolScript>().enabled = true;
            gameObject.GetComponent<BattlePatrolScript>().enabled = false;
            gameObject.GetComponent<EnemyDialog>().enabled = true;
            Debug.Log("Veysel Scene");
        }
        else if(SceneManager.GetActiveScene().name == "BattleScene")
        {
            gameObject.GetComponent<EnemyFire>().enabled = true;
            gameObject.GetComponent<PatrolScript>().enabled = false;
            gameObject.GetComponent<BattlePatrolScript>().enabled = true;
            gameObject.GetComponent<EnemyDialog>().enabled = false;
            Debug.Log("BattleScene");
        }
    }


}
