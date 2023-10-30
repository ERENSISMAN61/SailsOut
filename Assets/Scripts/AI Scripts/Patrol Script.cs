using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // important for AI.
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


//if you use this code you are contractually obligated to like the YT video
public class PatrolScript : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    private Transform centerPoint; //center of the area the agent wants to move around in
    //instead of centerPoint you can set it as the transform of the agent if you don't care about a specific area

    private float rotationSpeed = 3f;
    public Vector3 destinationPoint;
    public Vector3 startPoint;
    private GameObject playerShip;
    private float WaitingTime;

    private EnemyShipsController shipsController;

    private TextMeshProUGUI conversationText;

    private float catchDistance = 3.5f;  // player yakalanma mesafesi
    
    private GameObject enemyDialog;
    private GameObject spawnEnemyDialog;

    GameObject AttackButtonObject;
    GameObject PayButtonObject;
    GameObject SurrenderButtonObject;


    public bool canCatch = true;

    void Start()
    {
        // sava? bitiminde ve paray? kabul etti?inde waiting time ? ba?lat

        enemyDialog = (GameObject)Resources.Load("Resources/Prefabs/Canvas Prefabs/EnemyDialog");

        


        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        centerPoint = transform; 

        startPoint = transform.position;
        agent.SetDestination(startPoint);
        shipsController = GetComponent<EnemyShipsController>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
    }


    private void MoveToPlayer()
    {

        if (playerShip == null)
        {
            shipsController.GetComponent<FieldController>().isEntered = false;
        }
        else
        {
            agent.SetDestination(playerShip.transform.position);
            RotateTowardsPlayer();

        }

    }

    void Update()
    {
       // Debug.Log(Time.timeScale);///////////////////silinecek////////////////////////////////
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("ErenTest2"); //silinecek //////////////////////////////////////////////
        }
        if (shipsController.GetComponent<FieldController>().isEntered == false || (shipsController.GetComponent<FieldController>().isEntered == true && !canCatch))
        {
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {

                if (RandomPoint(centerPoint.position, range, out destinationPoint)) //pass in our center point and radius of area
                {
                    Debug.DrawRay(destinationPoint, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(destinationPoint);


                }
            }
        }
        else
        {
            playerShip = GameObject.FindGameObjectWithTag("Player");
            MoveToPlayer();
        }

        RotateTowardsPoint();

        
        CatchControl();

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    void RotateTowardsPlayer()
    {
        Vector3 directionToTarget = playerShip.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    void RotateTowardsPoint()
    {
        Vector3 directionToTarget = destinationPoint - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void CatchControl()
    {
        if (shipsController.GetComponent<FieldController>().isEntered == true)
        {
            playerShip = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(agent.transform.position, (playerShip.transform.position)) <= catchDistance && canCatch) // player konumu ile enemy konumunun mesafesi catchDistance(3.5f)den kucukse
            {
                 spawnEnemyDialog = Instantiate(enemyDialog, new Vector3(+960, +540, 0), Quaternion.identity, GameObject.Find("Canvas").transform);  // enemy dialogu Spawnla
                /////\\\\\
                AttackButtonObject = GameObject.Find("AttackButton");
                PayButtonObject = GameObject.Find("PayButton");
                SurrenderButtonObject = GameObject.Find("SurrenderButton");

                AttackButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gameObject.GetComponent<EnemyDialog>().AttackButton("BattleScene"));
                PayButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gameObject.GetComponent<EnemyDialog>().PayButton());
                SurrenderButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => gameObject.GetComponent<EnemyDialog>().SurrenderButton());
                /////\\\\\


                canCatch = false;

                agent.isStopped = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 100000;
                SlowDownGame();

                conversationText = GameObject.FindGameObjectWithTag("EnemyText").GetComponent<TextMeshProUGUI>();
                conversationText.text = "I've got you cornered. Surrender or we will attack.";

            }
        }

    }

    public IEnumerator WaitCatch()
    {
        Destroy(spawnEnemyDialog);
        ResumeGame();
        agent.isStopped = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 1;
        gameObject.GetComponent<EnemyDialog>().didPay = false;

        yield return new WaitForSeconds(10f);

        canCatch = true;


       
    }

    public IEnumerator Surrendered()
    {
        Destroy(spawnEnemyDialog); //Destroy gelecek
        ResumeGame();
        agent.isStopped = false;
        gameObject.GetComponent<EnemyDialog>().didSteal = false;
        
        yield return new WaitForSeconds(12f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().spawnPosition = gameObject.transform.position;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().spawnRotation = gameObject.transform.rotation;
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().isAllowSpawn = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().SpawnPlayerAnyWhere();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().isAllowSpawn = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 1;
        yield return new WaitForSeconds(10f);
        canCatch = true;
       
    }

    void SlowDownGame()
    {
        Time.timeScale = 0.1f;
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

}