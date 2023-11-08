using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class xEnemyTargetText : MonoBehaviour
{
    private NavMeshAgent agent;
    private void Start()
    {
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
        //agent.updatePosition = false;
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        
    }
    void Update()
    {// Ana gemiye 5m kala durmasını sağlama
     // Düşman gemisinin konumunu al
        Vector3 enemyPosition = transform.position;
        // Ana geminin konumunu al
        Vector3 motherShipPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        // Ana gemi ile düşman gemisi arasındaki vektörü hesapla
        Vector3 vectorToMotherShip = enemyPosition - motherShipPosition;

        float distanceToMotherShip = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        Debug.Log(distanceToMotherShip);

        if (distanceToMotherShip < 80)
        {
            agent.isStopped = true;
            agent.updateRotation = false;
            
        }
        else
        {
            agent.isStopped = false;
        }
        // Düşman gemisinin rotasyonunu 90 derece sağa döndür
        if (agent.isStopped)
        {
            Quaternion rotation = Quaternion.LookRotation(motherShipPosition - enemyPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f * Time.deltaTime);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
        }




    }






}
