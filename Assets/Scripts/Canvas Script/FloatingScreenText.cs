using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScreenText : MonoBehaviour
{

    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;

    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    
    void Update()
    {
    Vector3 pos = mainCamera.WorldToScreenPoint(lookAt.position + offset);

        if(transform.position != pos)
        {
            transform.position = pos;
        }

        
    }
}
