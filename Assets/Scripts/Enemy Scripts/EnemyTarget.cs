using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyTarget : MonoBehaviour
{
    private float radius = 500f;
    private float distance = 80f; // gorus alaninin objenin ne kadar onunde olacagi
    private LayerMask playerLayer;

    void Start()
    {

        playerLayer = LayerMask.GetMask("Player");
    }
    void Update()
    {                                               //transform.forward * distance = onunu daha fazla gormesi icin
         if (Physics.CheckSphere(transform.position + transform.forward * distance, radius,playerLayer)) // gorus alaninda player varsa
        {
     //       Debug.Log("Enemy Target Hit");
        }

    
    
    }


    // SPHERE GIZMOS

    //private void OnDrawGizmos()
    //{

    //    Gizmos.DrawSphere(transform.position + transform.forward * distance, radius);
    //}



}
