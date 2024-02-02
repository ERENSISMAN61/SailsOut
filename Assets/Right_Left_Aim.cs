using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Left_Aim : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    public bool isLeftAimActive;
    public bool isRightAimActive;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.WorldToScreenPoint(Input.mousePosition);
        if(mousePosition.x < 0)
        {
            isLeftAimActive = false;
            isRightAimActive = true;
        }
        else
        {
            isRightAimActive = false;
            isLeftAimActive = true;
        }
        Debug.Log("Position: " + mousePosition);
    }
}
