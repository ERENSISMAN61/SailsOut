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
    void Start()
    {


        InventoryObject = GameObject.FindGameObjectWithTag("Inventory");

        enemyDialogOrganize = gameObject.GetComponent<EnemyDialogOrganize>();
        smoothAgentMovement = gameObject.GetComponent<SmoothAgentMovement>();
        CameraSystemScript= GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>();
    }

    private void FixedUpdate()
    {
        CameraSystemScript.enemyPos = gameObject.transform.position;
    }

    public void AttackButton()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().mass = 1;   //SÝLÝNECEK ////////////////////////////////////////////////////////////////////////////////////////
        Time.timeScale = 1; //SÝLÝNEbilir belki ////////////////////////////////////////////////////////////////////////////////////////    
        Debug.Log("AttackButton");
        //SceneManager.LoadScene(sceneName);

        if (!TekKullan)
        {
            SceneManager.LoadScene("VeyselScene");
            TekKullan = true;
            smoothAgentMovement.didCatch = false;
        }
        
    }


    public void PayButton()
    {
        if (!didPay)
        {

            InventoryObject.GetComponent<InventoryController>().coinCount -= 30;

            didPay = true;

            Destroy(enemyDialogOrganize.spawnEnemyDialog);
            enemyDialogOrganize.isDialogSpawned = false;

            smoothAgentMovement.didCatch = false;
            smoothAgentMovement.isTargetEnemy = false;
            StartCoroutine("WaitCatch");
        }

    }
    private IEnumerator WaitCatch()
    {


      //  agent.isStopped = false;

        didPay = false;

        yield return new WaitForSeconds(10f);

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
            Destroy(enemyDialogOrganize.spawnEnemyDialog);
            enemyDialogOrganize.isDialogSpawned = false;

            smoothAgentMovement.didCatch = false;
            smoothAgentMovement.isTargetEnemy = false;

        }
        StartCoroutine("Surrendered");

    }

    private IEnumerator Surrendered()
    {

        didSteal = false;
        yield return new WaitForSeconds(12f);

        CameraSystemScript.followPlayer = true;
        CameraSystemScript.followEnemy = false;


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
