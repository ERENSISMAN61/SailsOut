using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerFire : MonoBehaviour
{


    public GameObject cannonballPrefab;
    public Transform[] rightFirePoint;
    public Transform[] leftFirePoint;


    public bool isActiveLevel1 = false;
    public bool isActiveLevel2 = false;
    public bool isActiveLevel3 = false;

    private Right_Left_Aim mousePosition;
    private DrawTrajectory drawTrajectory;
    public CinemachineFreeLook rightCamera;
    public CinemachineFreeLook leftCamera;
    private GameObject cam;

    public Vector3 offset_FirePos0;
    public Vector3 offset_FirePos1;
    public Vector3 offset_FirePos2;


    private GameObject rightTrajectory;
    private GameObject leftTrajectory;


    public bool isShooting = false;
    private float CurrentTime = 0f;
    public float launchAngle;
    public float initialSpeed;

    public float timeBetweenShots = 1f; // Set your desired time between shots here
    private float lastShotTime = 0f;


    // Yeni tanımladığımız değişkenler
    public int maxAmmo = 10; // Maksimum mermi sayısı
    private int ammo; // Mevcut mermi sayısı

    public LayerMask targetLayer;   // Atışın etkileşimde bulunacağı layer

    private void Start()
    {
        CurrentTime = Time.time;
        //drawTrajectory = gameObject.GetComponentInChildren<DrawTrajectory>();
        mousePosition = GameObject.FindGameObjectWithTag("MousePosition").GetComponent<Right_Left_Aim>();
        ammo = maxAmmo; // Mermi sayısını maksimum mermi sayısına eşitle
       
        rightTrajectory = GameObject.FindGameObjectWithTag("RightTrajectory");
        leftTrajectory = GameObject.FindGameObjectWithTag("LeftTrajectory");
        cam = GameObject.FindGameObjectWithTag("MainCamera");

    }


    void FireCannonball(Vector3 targetPoint ,Transform firePoint, Vector3 offset)
    {

        Vector3 firePointRotation = (targetPoint - firePoint.position) + offset;

        // Topun rotasyonunu, hedefe doğru bakacak şekilde ayarla
        Quaternion fireRotation = Quaternion.FromToRotation(firePoint.forward, firePointRotation);

        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        

        //float radianAngle = Mathf.Deg2Rad * launchAngle;


        ////drawProjection.ShowTrajectory(firePoint.position, initialSpeed, launchAngle);
        //Vector3 mouseInput = rightCamera.m_LookAt.position;

        //Vector3 launchDirection = firePoint.forward * 0.5f * Mathf.Abs(Physics.gravity.y) * 5 * 5;
        


        rb.velocity = (firePointRotation.normalized) * initialSpeed;

       
        Destroy(cannonball, 5f);
        lastShotTime = Time.time;

        // Her ateş ettiğinde mermi sayısını bir azalt
        ammo--;
        Debug.Log("Mermi sayısı: " + ammo);


    }



    void FireFunction2()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                // Mermi sayısının sıfırdan büyük olduğunu kontrol et
                if (ammo > 0)
                {
                    if (Time.time - lastShotTime > timeBetweenShots)
                    {
                       
                        // Draw the ray in the Scene view
                        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 2f);
                        if (rightCamera.Priority > leftCamera.Priority)
                        {

                            if (isActiveLevel1)
                            {
                                

                                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                                {
                                    FireCannonball(hit.point, rightFirePoint[0], Vector3.right * 5);
                                }
                            }

                            if (isActiveLevel2)
                            {
                                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                                {
                                    FireCannonball(hit.point, rightFirePoint[1], offset_FirePos1);
                                }
                            }

                            if (isActiveLevel3)
                            {
                                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                                {
                                    FireCannonball(hit.point, rightFirePoint[2], Vector3.left * 5);
                                }
                            }

                            lastShotTime = Time.time;

                        }
                        else if (leftCamera.Priority > rightCamera.Priority)
                        {
                            if (isActiveLevel1)
                            {
                                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                                {
                                    FireCannonball(hit.point, leftFirePoint[0], Vector3.right * 5);
                                }
                            }

                            if (isActiveLevel2)
                            {
                                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                                {
                                    FireCannonball(hit.point, leftFirePoint[1], offset_FirePos1);
                                }
                            }

                            if (isActiveLevel3)
                            {
                                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                                {
                                    FireCannonball(hit.point, leftFirePoint[2], Vector3.left * 5);
                                }
                            }
                            lastShotTime = Time.time;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }



    }


    IEnumerator Reload()
    {
        // Yeniden yükleme süresi kadar bekle
        yield return new WaitForSeconds(timeBetweenShots);
        // Yeniden yükleme durumunu false yap
        //isReloading = false;
    }

    void Update()
    {
        FireFunction2();
        //if (lineRenderer != null)
        //{
        //    if (Input.GetMouseButton(1))
        //    {
        //        VisualizeTrajectory();
        //        lineRenderer.enabled = true;
        //    }
        //    if (Input.GetMouseButtonUp(1))
        //    {
        //        initialSpeed = 0f;
        //        lineRenderer.enabled = false;
        //    }
        //}

        //float mouseY = Input.GetAxis("Mouse Y");

        //if (mouseY == 0f)
        //{
        //    // Fare Y ekseninde hareket etmiyor
        //    // initialSpeed'i sabit tut
        //}
        //else
        //{
        //    // Fare Y ekseninde hareket ediyor
        //    if (mouseY > 0f)
        //    {
        //        // Yukarı doğru hareket
        //        initialSpeed += speedChangedRate * Time.deltaTime;
        //    }
        //    else
        //    {
        //        // Aşağı doğru hareket
        //        initialSpeed -= speedChangedRate * Time.deltaTime;
        //        initialSpeed = Mathf.Max(initialSpeed, 0f); // Minimum hız sınırlaması (sıfırdan küçük olamaz)
        //    }
        //}

    }


}
