using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShipMovementScript : MonoBehaviour
{
    public float forceMagnitude;
    private Rigidbody rb;

    public float moveSpeed;

    public float rotationSpeed;




    public Text ParsomenText;
    private int ParsomenCount = 0;


    public bool isBulletEntered = false;


    public float deathTime;
    //[SerializeField] GameObject AimObjectForDisable;
    [SerializeField] private GameObject playerHealthBarObj;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();


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

        //ShipMovement();
        


    }

    //void ShipMovement()
    //{
    //    // Get player input for ship movement
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    float verticalInput = Input.GetAxis("Vertical");

    //    // Apply forward force
    //    Vector3 forward = moveSpeed * verticalInput * transform.forward;
    //    rb.AddForce(forward);




    //    if (Mathf.Abs(horizontalInput) > 0.1f)
    //    {
    //        // Rotate the ship based on the horizontal input

    //        float rotationForce = horizontalInput * rotationSpeed * transform.rotation.w;
    //        rb.AddTorque(0f, rotationForce, 0f);
    //    }


    //}

    private void setMoveSpeedAndRotationSpeed()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            moveSpeed += 5;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            moveSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            rotationSpeed += 10;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rotationSpeed = 0;
        }
    }

    void Update()
    {
        
        setMoveSpeedAndRotationSpeed();

        // Movement and Rotation code remains the same as before
        // ...



        if (playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health <= 100 && playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health > 75)
        {


        }
        else if (playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health <= 75 && playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health > 50)
        {

        }
        else if (playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health <= 50 && playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health > 0)
        {

        }
        else if (playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health == 0 || playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health < 0)
        {

            gameObject.GetComponent<PlayerFire>().enabled = false;
            //gameObject.SetActive(false);

            Debug.Log("PLAYER ÖLDÜ");
            StartCoroutine(waitDestroyPlayer(deathTime));


        }



    }





    IEnumerator waitDestroyPlayer(float destroyTime)
    {


        gameObject.GetComponent<AudioGet>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        // GetComponent<EdgeCollider2D>().enabled = false;
        //  GetComponent<PlayerAimWeapon>().gameObject.SetActive(false);
        //AimObjectForDisable.SetActive(false);
        gameObject.GetComponent<ShipMovementScript>().enabled = false;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health += 10;
            //playerHealthBarControl.updateHealthBar(playerHealthBarControl.health, playerHealthBarControl.maxHealth);
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            isBulletEntered = true;
            if (isBulletEntered == true)
            {
                //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\   bu oç hatasını bulmak için 2 saatimi verdim piç kod    //////////////////////////////////////////////////////////////////
                // health -= GameObject.FindGameObjectWithTag("EnemyBullet").GetComponent<BulletController>().enemyBulletDamage;
                playerHealthBarObj.GetComponent<PlayerHealthBarControl>().health -= collision.gameObject.GetComponent<BulletController>().enemyBulletDamage;

                playerHealthBarObj.GetComponent<PlayerHealthBarControl>().lerpTimer = 0f; // reset the timer
                                                       //playerHealthBarControl.updateHealthBar(playerHealthBarControl.health, playerHealthBarControl.maxHealth);

            }

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            isBulletEntered = false;
            Destroy(collision.gameObject);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Island"))
        {
            ParsomenCount++;
            collision.gameObject.SetActive(false);
            ParsomenText.text = String.Format("Parsomen: {0}/3", ParsomenCount);



        }


    }


}