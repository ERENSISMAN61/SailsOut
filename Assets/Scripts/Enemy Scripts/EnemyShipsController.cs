using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShipsController : MonoBehaviour
{
    public float maxHealth =100f;
    public float health =100f;

    public float rotationSpeed = 3f;
    public float timeBetweenBursts = 2f;
    public Sprite lessDamagedSprite;
    public Sprite highDamagedSprite;
    public Sprite destroyedSprite;

    

    private SpriteRenderer spriteRenderer;

    [SerializeField] EnemyHealthBarControl healthBar;

    
    private GameObject InventoryObj;
    private bool isAllow = true;

    public bool isBulletOut = false;
    public bool isBulletEntered = false;

   

    private void Awake()
    {
       // healthBar.GetComponentInChildren<EnemyHealthBarControl>();
    }

    void Start()
    {
        
        InventoryObj = GameObject.FindGameObjectWithTag("Inventory");

        health = maxHealth;
        healthBar.updateHealthBar(health, maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        

        

    }

    void Update()
    {
        
        
        if (health <= 0 && isAllow)
        {
            isAllow= false;
            InventoryObj.GetComponent<InventoryController>().ItemDrop();

        }
        
    }


    //void RotateTowardsPlayer()
    //{
    //    Vector3 directionToTarget = target.position - transform.position;
    //    float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
    //    Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //}
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isBulletEntered = true;
            health -= collision.gameObject.GetComponent<BulletController>().playerBulletDamage;
            healthBar.updateHealthBar(health, maxHealth);
            if (health <= 75 && health > 50)
            {
                spriteRenderer.sprite = lessDamagedSprite;
            }
            else if (health <= 50 && health > 0)
            {
                spriteRenderer.sprite = highDamagedSprite;
            }
            else if (health <= 0)
            {
                spriteRenderer.sprite = destroyedSprite;
                Destroy(gameObject, 0.5f);

            }

        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isBulletEntered = false;
            Destroy(collision.gameObject);
        }
    }





}
