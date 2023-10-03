using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BattlePatrolScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    private float rotationSpeed = 3f;
    public Vector3 destinationPoint;
    public Vector3 startPoint;
    private GameObject playerShip;
    private float WaitingTime;

    private RedEnemyFire shipsController;

    public bool isScriptWorking;


    private float catchDistance = 15f;  // player yakalanma mesafesi
    public bool isAsked = false;
    public bool canAsk = false;

    void Start()
    {

        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        startPoint = transform.position;
        agent.SetDestination(startPoint);
        shipsController = gameObject.GetComponent<RedEnemyFire>();

        playerShip = GameObject.FindGameObjectWithTag("Player");


    }


    private void MoveToPlayer()
    {


        if (playerShip == null)
        {
            //shipsController.fieldObject.GetComponent<FieldController>().isEntered = false;

        }
        else
        {
            agent.SetDestination(playerShip.transform.position);


        }

        canAsk = true;

    }
    void FindPlayerShip()
    {
        if (playerShip.IsDestroyed())
        {
            playerShip = null;
        }
        else
        {
            playerShip = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        FindPlayerShip();
        

        Debug.DrawRay(destinationPoint, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos

        //MoveToPlayer();
        CatchControl();
    }


    void RotateTowardsPlayer()
    {
        if (playerShip == null)
        {
            
        }
        else
        {

            //Vector3 directionToTarget = playerShip.transform.position - transform.position;
            //float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
            //Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(playerShip.transform.position);
        }
    }


    void CatchControl()
    {
        if (playerShip == null)
        {

        }
        else
        {

            if (Vector3.Distance(agent.transform.position, (playerShip.transform.position)) <= catchDistance) // player konumu ile enemy konumunun mesafesi catchDistance(15f)den kucukse
            {
                agent.isStopped = true;
                //isScriptWorking = false;


            }
            else
            {
                agent.isStopped = false;
                //isScriptWorking = true;
                RotateTowardsPlayer();
                MoveToPlayer();
                //agent.SetDestination(playerShip.transform.position);



            }
        }
    }

}
