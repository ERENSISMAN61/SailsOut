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
    public Rigidbody2D rb;
    
    public float moveSpeed = 5f;
    
    public float rotationSpeed = 5f;

    public Sprite healthySprite;
    public Sprite lessDamagedSprite;
    public Sprite highDamagedSprite;
    public Sprite destroyedSprite;

    SpriteRenderer spriteRenderer;



    public Text ParsomenText;
    private int ParsomenCount = 0;


    public bool isBulletEntered = false;

    
    public float deathTime;
    [SerializeField] GameObject AimObjectForDisable;
    [SerializeField] PlayerHealthBarControl playerHealthBarControl;
   

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    private void FixedUpdate()
    {

        
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("BattleScene"); //silinecek/////////////////////////////////
        }
        if (Input.GetKey(KeyCode.E))
        { 
            SceneManager.LoadScene("Eren Scene"); //silinecek/////////////////////////////////
        }
        // Get the horizontal input (e.g., A and D keys or left and right arrows)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Check if there is horizontal input (left or right key pressed)
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Rotate the ship based on the horizontal input
           
            float rotationForce = -horizontalInput * rotationSpeed * Time.fixedDeltaTime*10;
            rb.AddTorque(rotationForce);
        }


        // Get the vertical input (e.g., W and S keys or up and down arrows)
        float verticalInput = Input.GetAxis("Vertical");

       







        // Get the horizontal input (e.g., A and D keys or left and right arrows)
    



        // Calculate the direction the ship should move in
        Vector3 moveDirection = transform.up * verticalInput;

        // Apply force to move the ship in the desired direction
        rb.AddForce(moveDirection * forceMagnitude);

        // Apply lateral force to allow left and right movement
  

        // Limit maximum velocity
        if (rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }
    void Update()
    {
   

        
       
        // Movement and Rotation code remains the same as before
        // ...

         

        if(playerHealthBarControl.health  <= 100 && playerHealthBarControl.health > 75)
        {

            spriteRenderer.sprite = healthySprite;
        }
        else if (playerHealthBarControl.health <= 75 && playerHealthBarControl.health > 50)
        {
            spriteRenderer.sprite = lessDamagedSprite;
        }
        else if (playerHealthBarControl.health <= 50 && playerHealthBarControl.health > 0)
        {
            spriteRenderer.sprite = highDamagedSprite;
        }
        else if (playerHealthBarControl.health == 0  || playerHealthBarControl.health < 0)
        {
            spriteRenderer.sprite = destroyedSprite;
            gameObject.GetComponent<PlayerFire>().enabled = false;
            //gameObject.SetActive(false);
            StartCoroutine(waitDestroyPlayer(deathTime));

        }

        

    }

   



    IEnumerator waitDestroyPlayer(float destroyTime)
    {
        
        
        gameObject.GetComponent<AudioGet>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
       // GetComponent<EdgeCollider2D>().enabled = false;
        //  GetComponent<PlayerAimWeapon>().gameObject.SetActive(false);
        AimObjectForDisable.SetActive(false);
        gameObject.GetComponent<ShipMovementScript>().enabled = false;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Heart"))
        {
            playerHealthBarControl.health += 10;
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
                playerHealthBarControl.health -= collision.gameObject.GetComponent<BulletController>().enemyBulletDamage;

                playerHealthBarControl.lerpTimer = 0f; // reset the timer
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