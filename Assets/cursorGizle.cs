using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorGizle : MonoBehaviour
{
    void Start()
    {
        // Make the cursor invisible
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        // Toggle cursor visibility with a key press (for testing)
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = !Cursor.visible;
        }
    }
}
