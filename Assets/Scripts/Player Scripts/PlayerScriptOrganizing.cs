using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScriptOrganizing : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<PlayerFire>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "VeyselScene")
        {
            gameObject.GetComponent<PlayerFire>().enabled = false;
            
            Debug.Log("Eren Scene");
        }
        else if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            gameObject.GetComponent<PlayerFire>().enabled = true;
            
            Debug.Log("BattleScene");
        }
    }
}
