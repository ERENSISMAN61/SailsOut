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
            isBulletEntered = true;
            if (isBulletEntered == true)
            {
                //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\   bu oç hatasını bulmak için 2 saatimi verdim piç kod    //////////////////////////////////////////////////////////////////
                // health -= GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<BulletController>().enemyBulletDamage;
                playerHealthBarControl.health -= collision.gameObject.GetComponent<BulletController>().enemyBulletDamage;

                playerHealthBarControl.lerpTimer = 0f; // reset the timer
                                                       //playerHealthBarControl.updateHealthBar(playerHealthBarControl.health, playerHealthBarControl.maxHealth);

            }

        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            isBulletEntered = false;
            Destroy(collision.gameObject);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Island"))
        {
            ParsomenCount++;
            collision.gameObject.SetActive(false);
            //    ParsomenText.text = String.Format("Parsomen: {0}/3", ParsomenCount);    /////////////// ACILACAK



        }


    }


}