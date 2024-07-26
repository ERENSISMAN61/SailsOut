using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaddd : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
        }
    }
}
