using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Switch : MonoBehaviour
{
    public CinemachineVirtualCamera rightCamera;
    public CinemachineVirtualCamera leftCamera;
    public CinemachineFreeLook mainCamera;

    private Right_Left_Aim mousePosition;
    private int camIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = GameObject.FindGameObjectWithTag("MousePosition").GetComponent<Right_Left_Aim>();
        mainCamera.Priority = 10;
        rightCamera.Priority = 0;
        leftCamera.Priority = 0;
    }
    void Right_Cam()
    {
        rightCamera.Priority = 11;
        leftCamera.Priority = 0;
        mainCamera.Priority = 0;
    }

    void Left_Cam()
    {
        leftCamera.Priority = 11;
        rightCamera.Priority = 0;
        mainCamera.Priority = 0;
    }

    void Main_Cam()
    {
        mainCamera.Priority = 11;
        rightCamera.Priority = 0;
        leftCamera.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if (mousePosition.isRightAimActive)
            {
                Right_Cam();
                if(Input.GetMouseButtonDown(1))
                {
                    mousePosition.isLeftAimActive = false;
                }
            }
            else if (mousePosition.isLeftAimActive)
            {
                
                if (Input.GetMouseButtonDown(1))
                {
                    mousePosition.isRightAimActive = false;
                }
                Left_Cam();
            }
           
        }
        else
        {
            Main_Cam();
        }

    }
}
