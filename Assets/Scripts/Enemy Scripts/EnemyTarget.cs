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

    private HashSet<Collider> objectsInSphere = new HashSet<Collider>();

    void Start()
    {

        playerLayer = LayerMask.GetMask("OutlineFalse", "OutlineTrue");
    }
    void Update()
    {// görüþ alanýndaki tüm player nesnelerini bir diziye ata
        Collider[] enemies = Physics.OverlapSphere(transform.position + transform.forward * distance, radius, playerLayer); 

        //transform.forward * distance = onunu daha fazla gormesi icin// gorus alaninda player varsa


        HashSet<Collider> newObjectsInSphere = new HashSet<Collider>();


        foreach (Collider enemy in enemies) 
        {
            if (enemy.CompareTag("PlayerParts"))
            {

                newObjectsInSphere.Add(enemy);

                if (!objectsInSphere.Contains(enemy))
                {
                    Debug.Log("Truee");
                    gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy = true;
                }

            }
        }

        foreach (Collider oldObject in objectsInSphere)
        {
            if (!newObjectsInSphere.Contains(oldObject))
            {

                    Debug.Log("Falsee");
                    gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy = false;

            }
        }

        objectsInSphere = newObjectsInSphere;

        //foreach (Collider enemy in enemies)
        //{
        //    if (enemy.CompareTag("PlayerParts"))
        //    {
        //        if (gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy == true)
        //        {
        //        if(gameObject.GetComponent)
        //        }

        //    }
        //}


    }


    // SPHERE GIZMOS

    //private void OnDrawGizmos()
    //{

    //    Gizmos.DrawSphere(transform.position + transform.forward * distance, radius);
    //}



}
