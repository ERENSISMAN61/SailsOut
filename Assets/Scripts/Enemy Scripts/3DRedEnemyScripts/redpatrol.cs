using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class redPatrol : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerShip;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerShip = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerShip != null)
        {
            MoveToPlayer();
            RotateTowardsPlayer();
        }
    }

    void MoveToPlayer()
    {
        agent.SetDestination(playerShip.position);
    }

    void RotateTowardsPlayer()
    {
        Vector3 directionToTarget = playerShip.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed);
    }


}
