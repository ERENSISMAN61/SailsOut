using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{

    //public float speed;
    //private Vector3 lastPosition;
    //public Transform transformPlayer;

    //private Camera mainCamera;

    // public float zoomOutSpeed = 0.4f;
    //  public float maxZoomOut = 9f;
    //  public float minZoomIn = 7f;
    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //}
    //void FixedUpdate()
    //{
    //  //  speed = (transformPlayer.position - lastPosition).magnitude / Time.deltaTime;
    //    lastPosition = transformPlayer.position;
    // //   float zoomOutAmount = Mathf.Clamp(speed * zoomOutSpeed, 0f, maxZoomOut);

    //   //     mainCamera.orthographicSize = Mathf.Clamp(minZoomIn + zoomOutAmount, minZoomIn, maxZoomOut);

    //    Vector3 newPosition = new Vector3(transformPlayer.position.x, transformPlayer.position.y, transform.position.z);
    //    transform.position = newPosition;
    //}

    private Transform targetShip;
    public Transform targetCamera;  // Gemi yokken takip edilecek obje
    // [SerializeField] float cameraSpeed ;

    private bool hedefVar = true; // Hedef transformunun varlýðýný tutan bir deðiþken
    private bool isSpawned = false;
    private GameObject PlayerObject;
    private GameObject spawnPlayer;
    public static CameraManager thisCameraManager;


    private Transform spawnPoint;

    public bool isAllowSpawn = false; //\\    //\\____BU 3 SATIR, SPAWNLAMAK ÝSTEDÝÐÝMÝZ YER____//\\    //\\
    public Vector3 spawnPosition;    //  \\  //  \\_(.position)_VARSA KULLANILACAK_(.rotation)_//  \\  //  \\
    public Quaternion spawnRotation;//    \\//    \\__________________________________________//    \\//    \\

    private GameObject firstSpawn;
    private void Awake()
    {

        spawnPlayer = (GameObject)Resources.Load("Prefabs/PlayerPrefabs/DefaultShip");

        if (GameObject.Find("Spawn Point") != null)
        {
            spawnPoint = GameObject.Find("Spawn Point").transform;  // "Spawn Point" isminde obje olmalý.
        }
        else if(spawnPoint == null)
        {
            spawnPoint = gameObject.transform;
        }
        firstSpawn = Instantiate(spawnPlayer, spawnPoint.position, Quaternion.Euler(0, 0, 0)); 
    }
    void Start()
    {
       
        //firstSpawn.GetComponent<ShipMovementScript>().maxHealth= 100f;
        //firstSpawn.GetComponent<ShipMovementScript>().health= 100f;


        thisCameraManager = this;


        PlayerObject= GameObject.FindWithTag("Player");

        targetShip = PlayerObject.transform;

        targetCamera = gameObject.transform;

        spawnPosition = new Vector3(0, 0, 0);
        spawnRotation = Quaternion.Euler(0, 0, 0);


    }

    public void Update()
    {

        //transform.position = Vector3.Slerp(transform.position, new Vector3(targetShip.position.x, targetShip.position.y, transform.position.z), cameraSpeed);
        if (hedefVar) // Eðer hedef varsa
        {
            PlayerTarget(); // Kahramaný takip et
            isSpawned = false;
        }
        else // Eðer hedef yoksa
        {

            CameraTarget(); // Düþmaný takip et

        }

    }


    public void PlayerTarget()
    {


        if (targetShip == null) // Eðer Hedef transformu kaybolursa
        {
            hedefVar = false; // Hedefin varlýðýný false yap
        }
        else
        {
            transform.position = new Vector3(targetShip.position.x, /*targetCamera.position.y*/ 800f, -500f);
        }

    }

    public void CameraTarget()
    {

        transform.position = new Vector3(targetCamera.position.x, targetCamera.position.y, transform.position.z);

    }

    public void SpawnPlayerAnyWhere()
    {
        if (!hedefVar)
        {
            if (!isSpawned && isAllowSpawn)
            {

                Instantiate(spawnPlayer, spawnPosition, spawnRotation); //Obje spawnlanýr istediðimiz yerde

                PlayerObject= GameObject.FindWithTag("Player");
                targetShip = PlayerObject.transform;
                isSpawned = true;
                hedefVar = true;
            }
        }
    }
}
