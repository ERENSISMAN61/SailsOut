using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAimWeapon : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        
    }


    private void Update()
    {


        Transform mainShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;


        float shipAngle = mainShip.rotation.eulerAngles.z;
        if ((angle-shipAngle <=-180 && angle-shipAngle>=-360) || (angle-shipAngle<=180 && angle-shipAngle >=0) ) // geminin önü
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

    }
}
