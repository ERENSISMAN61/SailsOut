using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Switch : MonoBehaviour
{
    public GameObject rightCamera;
    public GameObject leftCamera;
    private GameObject mainCamera;

    private Right_Left_Aim mousePosition;
    private int camIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mousePosition = GameObject.FindGameObjectWithTag("MousePosition").GetComponent<Right_Left_Aim>();
    }



    public void CamAnimationManagement()
    {
        if(camIndex == 0)
        {
            Main_Cam();
        }
    }

    void Right_Cam()
    {
        rightCamera.SetActive(true);
        leftCamera.SetActive(false);
        mainCamera.SetActive(false);
    }

    void Left_Cam()
    {
        leftCamera.SetActive(true);
        rightCamera.SetActive(false);
        mainCamera.SetActive(false);
    }

    void Main_Cam()
    {
        mainCamera.SetActive(true);
        rightCamera.SetActive(false);
        leftCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if (mousePosition.isRightAimActive)
            {
                
                Right_Cam();
            }
            else if (mousePosition.isLeftAimActive)
            {
                
                Left_Cam();
            }
           
        }
        else
        {
            Main_Cam();
        }

    }
}
