using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseIslandMenu : MonoBehaviour
{

    public bool didCloseIslandMenu = false;
    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DestroyIslandMenu();
        }
    }
    public void DestroyIslandMenu()
    {
        didCloseIslandMenu = true;

        Destroy(transform.parent.parent.parent.gameObject);


    }
}
