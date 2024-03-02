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

    public CinemachineFreeLook rightCamera;
    public CinemachineFreeLook leftCamera;
    private GameObject cam;



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
        ammo = maxAmmo; // Mermi sayısını maksimum mermi sayısına eşitle

        
        cam = GameObject.FindGameObjectWithTag("MainCamera");

    }


    void FireCannonball(Vector3 targetPoint, Transform firePoint)
    {

        //Vector3 firePointRotation = (targetPoint - firePoint.position);

        //// Topun rotasyonunu, hedefe doğru bakacak şekilde ayarla
        //Quaternion fireRotation = Quaternion.FromToRotation(firePoint.forward, firePointRotation);

        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();


        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        Debug.Log("Ray: " + ray.direction * 100f);
        rb.AddForce((ray.direction * initialSpeed) + (Vector3.up * launchAngle), ForceMode.Impulse);


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
            if (Input.GetMouseButtonDown(0))
            {
                // Mermi sayısının sıfırdan büyük olduğunu kontrol et
                if (ammo > 0)
                {
                    if (Time.time - lastShotTime > timeBetweenShots)
                    {

                        
                        if (rightCamera.Priority > leftCamera.Priority)
                        {

                            if (isActiveLevel1)
                            {

                                FireCannonball(Vector3.forward, rightFirePoint[0]);//Vector3.forward is not important here you will fix the parameter later

                            }

                            if (isActiveLevel2)
                            {

                                FireCannonball(Vector3.forward, rightFirePoint[1]);

                            }

                            if (isActiveLevel3)
                            {

                                FireCannonball(Vector3.forward, rightFirePoint[2]);

                            }

                            lastShotTime = Time.time;

                        }
                        else if (leftCamera.Priority > rightCamera.Priority)
                        {
                            if (isActiveLevel1)
                            {

                                FireCannonball(Vector3.forward, leftFirePoint[0]);

                            }

                            if (isActiveLevel2)
                            {

                                FireCannonball(Vector3.forward, leftFirePoint[1]);

                            }

                            if (isActiveLevel3)
                            {

                                FireCannonball(Vector3.forward, leftFirePoint[2]);

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

    void FixedUpdate()
    {
        FireFunction2();
        
    }


}
