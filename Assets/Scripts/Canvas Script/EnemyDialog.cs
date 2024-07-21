using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyDialog : MonoBehaviour
{
    GameObject InventoryObject;
    CameraSystem CameraSystemScript;
    EnemyDialogOrganize enemyDialogOrganize;
    SmoothAgentMovement smoothAgentMovement;


    public bool didPay = false;
    public bool didSteal = false;
    private bool TekKullan = false;
    GameObject player;

    void Start()
    {


        InventoryObject = GameObject.FindGameObjectWithTag("Inventory");

        enemyDialogOrganize = gameObject.GetComponent<EnemyDialogOrganize>();
        smoothAgentMovement = gameObject.GetComponent<SmoothAgentMovement>();
        CameraSystemScript = GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>();
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void FixedUpdate()
    {
        CameraSystemScript.enemyPos = gameObject.transform.position;

        Debug.Log("smoothAgentMovement.didCatch:  " + smoothAgentMovement.didCatch + "\nTime.timeScale: " + Time.timeScale);
    }

    public void AttackButton()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 1;   //S�L�NECEK ////////////////////////////////////////////////////////////////////////////////////////
        // Time.timeScale = 1; //S�L�NEbilir belki.  Time.timeScale= 0.3 � de silmen laz�m bunu sileceksen ////////////////////////////////////////////////////////////////////////////////////////    
        Debug.Log("AttackButton");
        //SceneManager.LoadScene(sceneName);

        if (!TekKullan)
        {
            SceneManager.LoadScene("BattleScene");
            TekKullan = true;
            smoothAgentMovement.didCatch = false;

            // ENEMY UNITleri Savas sahnesine gondermek icin.
            GameObject.FindGameObjectWithTag("Destroyless").GetComponent<EnemyDestroylessManager>()._EnemyToFightUnitsContainers
            = GetComponent<EnemyUnits>().GetEnemyUnits();
        }


    }


    public void PayButton()
    {
        if (!didPay)
        {

            InventoryObject.GetComponent<InventoryController>().coinCount -= 30;

            didPay = true;

            Destroy(enemyDialogOrganize.spawnEnemyDialog); //
            enemyDialogOrganize.isDialogSpawned = false;   //
                                                           ///// bu dortlu kesinlikle olmali eski hale getirilebilmesi icin.  \\\\\
            smoothAgentMovement.didCatch = false;          //
            smoothAgentMovement.isTargetEnemy = false;     //

            StartCoroutine("WaitCatch");
        }

    }
    private IEnumerator WaitCatch()
    {


        //  agent.isStopped = false;

        didPay = false;

        gameObject.GetComponent<EnemyTarget>().targetCoolDown = true;  // enemy 20 saniye boyunca playeri gormezden gelir.  //
        yield return new WaitForSeconds(20f);                                                                               // ��� cooldown i�in.
        gameObject.GetComponent<EnemyTarget>().targetCoolDown = false;                                                      //   

        //   canCatch = true;



    }

    public void SurrenderButton()
    {
        CameraSystemScript.followEnemy = true;
        CameraSystemScript.followPlayer = false;
        if (!didSteal)
        {


            InventoryObject.GetComponent<InventoryController>().bulletCount = 0;
            int randomNumberSupply = Random.Range(5, 11);
            InventoryObject.GetComponent<InventoryController>().supplyCount = InventoryObject.GetComponent<InventoryController>().supplyCount * randomNumberSupply / 100f;
            int randomNumberCoin = Random.Range(5, 11);
            InventoryObject.GetComponent<InventoryController>().coinCount = InventoryObject.GetComponent<InventoryController>().coinCount * randomNumberCoin / 100f;

            didSteal = true;


            Destroy(enemyDialogOrganize.spawnEnemyDialog); //
            enemyDialogOrganize.isDialogSpawned = false;   //
                                                           ///// bu dortlu kesinlikle olmali eski hale getirilebilmesi icin.  \\\\\
            smoothAgentMovement.didCatch = false;          //
            smoothAgentMovement.isTargetEnemy = false;     //



        }
        StartCoroutine("Surrendered");

    }

    private IEnumerator Surrendered()
    {
        player.SetActive(false);
        didSteal = false;
        yield return new WaitForSeconds(12f);

        CameraSystemScript.followPlayer = true;
        CameraSystemScript.followEnemy = false;

        player.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(2f);
        player.SetActive(true);


        gameObject.GetComponent<EnemyTarget>().targetCoolDown = true;  // enemy 20 saniye boyunca playeri gormezden gelir.  //
        yield return new WaitForSeconds(20f);                                                                               // ��� cooldown i�in.
        gameObject.GetComponent<EnemyTarget>().targetCoolDown = false;                                                      //            

        // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().spawnPosition = gameObject.transform.position;
        //    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().spawnRotation = gameObject.transform.rotation;
        //    yield return new WaitForSeconds(1f);
        //     GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().isAllowSpawn = true;
        //    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().SpawnPlayerAnyWhere();
        //   GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().isAllowSpawn = false;
        //   GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 1;
        //    yield return new WaitForSeconds(10f);
        //   canCatch = true;

    }
}
