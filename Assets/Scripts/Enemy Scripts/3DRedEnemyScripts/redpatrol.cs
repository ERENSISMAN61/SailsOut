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
    public float rotationSpeed = 0.2f;
    private RedEnemyFire redEnemyFire;

    public Transform rightCannonTransform;
    public Transform leftCannonTransform;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerShip = GameObject.FindGameObjectWithTag("Player").transform;
        redEnemyFire = gameObject.GetComponentInChildren<RedEnemyFire>();
    }

    void Update()
    {
        if (playerShip != null)
        {
            MoveToPlayer();

        }

        float distanceToMotherShip = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        
        //Debug.Log(distanceToMotherShip);

        if (distanceToMotherShip < 80)
        {
            agent.isStopped = true;
            //agent.updateRotation = false;
            //transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
            //                                                     // Dönüşü gerçekleştir

            //Quaternion rotasyon = Quaternion.LookRotation(transform.position, new Vector3(0f,90f,0f));
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotasyon, 5f * Time.deltaTime);
            
            RotateTowardsMotherShip();
            redEnemyFire.enabled = true;
        }
        else
        {
            RotateTowardsMotherShipExit();
            redEnemyFire.enabled = false;
            agent.isStopped = false;
            //RotateTowardsPlayer();

        }
    }

    void MoveToPlayer()
    {
        agent.SetDestination(playerShip.position);
    }

    //void RotateTowardsPlayer()
    //{
    //    Vector3 directionToTarget = playerShip.position - transform.position;
    //    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed);
    //}

    void RotateTowardsMotherShip()
    {
        float distanceCannonsToMotherShipRight = Vector3.Distance(rightCannonTransform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        float distanceCannonsToMotherShipLeft = Vector3.Distance(leftCannonTransform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        Debug.Log("RightCannon" + distanceCannonsToMotherShipRight);
        Debug.Log("LeftCannon" + distanceCannonsToMotherShipLeft);

        if(distanceCannonsToMotherShipRight < distanceCannonsToMotherShipLeft)
        {
            Vector3 directionToMotherShip = playerShip.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToMotherShip, Vector3.up);
            targetRotation *= Quaternion.Euler(0, 90f, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Eğer dönüş tamamlandıysa hareket etmeye devam et
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                agent.isStopped = false;
            }
        }
        else
        {
            Vector3 directionToMotherShip = playerShip.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToMotherShip, Vector3.up);
            targetRotation *= Quaternion.Euler(0, -90f, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Eğer dönüş tamamlandıysa hareket etmeye devam et
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                agent.isStopped = false;
            }
        }


        
    }

    void RotateTowardsMotherShipExit()
    {
        Vector3 directionToMotherShip = playerShip.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToMotherShip, Vector3.up);
        //targetRotation *= Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Eğer dönüş tamamlandıysa hareket etmeye devam et
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
        {
            agent.isStopped = false;
        }
    }
}
