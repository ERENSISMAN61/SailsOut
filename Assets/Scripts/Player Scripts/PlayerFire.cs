using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform[] rightFirePoint;
    public Transform[] leftFirePoint;
    

    


    public bool isActiveRightLevel1 = false;
    public bool isActiveRightLevel2 = false;
    public bool isActiveRightLevel3 = false;
    
    public float launchAngle;
    public float initialSpeed;

    private float CurrentTime = 0f;
    private void Start()
    {
        CurrentTime = Time.time;
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //mousePosition = GameObject.FindGameObjectWithTag("MousePosition").GetComponent<Right_Left_Aim>();
    }

    void SetActiveCannons()
    {
        if (isActiveRightLevel1 == true)
        {
            FireFunction2(0);
        }

        if (isActiveRightLevel2 == true)
        {

            
            FireFunction2(1);
        }
        if (isActiveRightLevel3 == true)
        {
            
            FireFunction2(2);
        }

    }

    void FireCannonball(Transform firePoint)
    {

        GameObject cannonball = Instantiate(cannonballPrefab, firePoint.position, Quaternion.identity);
        float radianAngle = Mathf.Deg2Rad * launchAngle;
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        // Ateş etme yönünü hesapla
        Vector3 launchDirection = (firePoint.forward * 2) + (Vector3.up * Mathf.Tan(radianAngle));

        rb.velocity = launchDirection * initialSpeed;
        Destroy(cannonball, 5f);

    }

    //void VisualizeTrajectory()
    //{
    //    Vector3 initialPosition = firePoint.position;
    //    float radianAngle = Mathf.Deg2Rad * launchAngle;
    //    Vector3 initialVelocity = new Vector3(initialSpeed * Mathf.Cos(radianAngle), initialSpeed * Mathf.Sin(radianAngle), 0f);
    //    Vector3[] trajectory = CalculateTrajectory(initialPosition, initialVelocity, numPoints, timeStep, firePoint.rotation);

    //    // Update LineRenderer positions
    //    for (int i = 0; i < numPoints; i++)
    //    {
    //        lineRenderer.SetPosition(i, trajectory[i]);
    //    }
    //}

    //public Vector3[] CalculateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, int numPoints, float timeStep, Quaternion rotation)
    //{
    //    Vector3[] trajectory = new Vector3[numPoints];

    //    for (int i = 0; i < numPoints; i++)
    //    {
    //        float time = i * timeStep;
    //        Quaternion rotatedRotation = rotation * Quaternion.Euler(0f, -90f, 0f);
    //        trajectory[i] = initialPosition + (rotatedRotation * (initialVelocity * time)) + 0.5f * Vector3.up * gravity * time * time;
    //    }

    //    return trajectory;
    //}

    void FireFunction(int cannonNumber)
    {
        if (Input.GetMouseButtonDown(0))
        {

            FireCannonball(leftFirePoint[cannonNumber]);


            CurrentTime = Time.time;
        }
        if (Input.GetMouseButtonDown(1))
        {

            FireCannonball(rightFirePoint[cannonNumber]);
            CurrentTime = Time.time;
        }
    }


    

    [System.Obsolete]
    void FireFunction2(int cannonNumber)
    {
        if (Input.GetMouseButton(1))
        {
           

            if (Input.GetMouseButton(0))
            {
                FireCannonball(leftFirePoint[cannonNumber]);
                FireCannonball(rightFirePoint[cannonNumber]);
                CurrentTime = Time.time;
            }

            CurrentTime = Time.time;
        }
        else
        {
            
        }
            
       
    }

    void Update()
    {
        SetActiveCannons();
        if (Time.time - CurrentTime >= 3f)
        {
        }

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
