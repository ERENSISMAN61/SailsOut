using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MovingScript : MonoBehaviour
{
    public float forceMagnitude;
    private Rigidbody rb;

    public float moveSpeed;

    public float rotationSpeed;

    private PlayerFire playerFire;


    // public Text ParsomenText;             /////  ACILACAK
    private int ParsomenCount = 0;


    public bool isBulletEntered = false;


    public float deathTime;
    //[SerializeField] GameObject AimObjectForDisable;
    [SerializeField] PlayerHealthBarControl playerHealthBarControl;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        playerHealthBarControl = gameObject.GetComponentInChildren<PlayerHealthBarControl>();

        // Ensure that other required components are properly initialized.
        if (GetComponent<AudioGet>() == null)
        {
            Debug.LogError("AudioGet component not found on the player object.");
        }
        if (GetComponentInChildren<MeshCollider>() == null)
        {
            Debug.LogError("MeshCollider component not found on the player object or its children.");
        }
        if (GetComponent<PlayerFire>() == null)
        {
            Debug.LogError("PlayerFire component not found on the player object.");
        }

        playerFire = gameObject.GetComponent<PlayerFire>();
    }





    private void FixedUpdate()
    {


        //if (Input.GetKey(KeyCode.Space))
        //{
        //    SceneManager.LoadScene("BattleScene"); //silinecek/////////////////////////////////
        //}
        //if (Input.GetKey(KeyCode.E))
        //{ 
        //    SceneManager.LoadScene("Eren Scene"); //silinecek/////////////////////////////////
        //}





    }

    void ShipMovement()
    {
        // Get player input for ship movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Apply forward force
        Vector3 forward = moveSpeed * verticalInput * transform.forward;
        rb.AddForce(forward);




        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Rotate the ship based on the horizontal input

            float rotationForce = horizontalInput * rotationSpeed;// * transform.rotation.w;
            rb.AddTorque(0f, rotationForce, 0f);
        }


    }


    void Update()
    {

        ShipMovement();

        // Movement and Rotation code remains the same as before
        // ...



        //if (playerHealthBarControl.health <= 100 && playerHealthBarControl.health > 75)
        //{


        //}
        //else if (playerHealthBarControl.health <= 75 && playerHealthBarControl.health > 50)
        //{

        //}
        //else if (playerHealthBarControl.health <= 50 && playerHealthBarControl.health > 0)
        //{

        //}
        if (playerHealthBarControl.health == 0 || playerHealthBarControl.health < 0)
        {

            gameObject.GetComponent<PlayerFire>().enabled = false;
            //gameObject.SetActive(false);
            StartCoroutine(waitDestroyPlayer(deathTime));

        }



    }





    IEnumerator waitDestroyPlayer(float destroyTime)
    {
        // Check if required components are null before accessing them.
        if (GetComponent<AudioGet>() != null)
        {
            GetComponent<AudioGet>().enabled = false;
        }
        else
        {
            Debug.LogError("AudioGet component not found on the player object.");
        }

        if (GetComponentInChildren<MeshCollider>() != null)
        {
            GetComponentInChildren<MeshCollider>().enabled = false;
        }
        else
        {
            Debug.LogError("MeshCollider component not found on the player object or its children.");
        }

        // Disable other components as needed.
        // ...

        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);

    }


    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.CompareTag("Heart"))
        //{
        //    playerHealthBarControl.health += 10;
        //    //playerHealthBarControl.updateHealthBar(playerHealthBarControl.health, playerHealthBarControl.maxHealth);
        //    Destroy(collision.gameObject);
        //}


        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Vector3 toCollisionPoint = collision.transform.position - gameObject.transform.position;
            float dotProduct = Vector3.Dot(transform.right, toCollisionPoint);
            Debug.Log("Dot: " + dotProduct);

            // Çarpma noktasını kullanarak kuvvet uygulayın
            Vector3 collisionPoint = collision.transform.position;

            if (dotProduct > 0)
            {
                rb.AddForceAtPosition(new Vector3(5, 0, 3), gameObject.transform.position, ForceMode.Impulse);
                StartCoroutine(SwingObject(10, 1f)); // Sağa yumuşak sallanma
            }
            else
            {
                rb.AddForceAtPosition(new Vector3(-5, 0, -3), gameObject.transform.position, ForceMode.Impulse);
                StartCoroutine(SwingObject(-10, 1f)); // Sağa yumuşak sallanma
            }
        }

    }

    private IEnumerator SwingObject(float angle, float duration)
    {
        float elapsedTime = 0;
        float initialRotation = transform.eulerAngles.z;
        float targetRotation = initialRotation + angle;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float zRotation = Mathf.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation));
            yield return null;
        }

        // Geriye yumuşak sallanma
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float zRotation = Mathf.Lerp(targetRotation, initialRotation, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation));
            yield return null;
        }
    }



}