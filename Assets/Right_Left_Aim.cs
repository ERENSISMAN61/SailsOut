using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Left_Aim : MonoBehaviour
{
    public bool isLeftAimActive;
    public bool isRightAimActive;
    // Start is called before the first frame update


    private Vector2 lastMousePosition;
    private Vector2 mouseDelta;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        mouseDelta = (Vector2)Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;


        //Debug.Log("Mouse Delta: " + mouseDelta);
        // mouse sağdaysa
        if (mouseDelta.x > 0)
        {
            isRightAimActive = true;
            isLeftAimActive = false;
        }
        // mouse soldaysa
        else if (mouseDelta.x < 0)
        {
            isLeftAimActive = true;
            isRightAimActive = false;
        }

        
    }
}
