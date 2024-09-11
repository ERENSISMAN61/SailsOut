using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCTarget : MonoBehaviour
{
    private float radius = 500f;
    private float distance = 80f; // gorus alaninin objenin ne kadar onunde olacagi
    private LayerMask playerLayer;

    private HashSet<Collider> objectsInSphere = new HashSet<Collider>();

    public bool targetCoolDown = false;

    void Start()
    {

        playerLayer = LayerMask.GetMask("OutlineFalse", "OutlineTrue");
    }
    void Update()
    {// g�r�� alan�ndaki t�m player nesnelerini bir diziye ata
        Collider[] enemies = Physics.OverlapSphere(transform.position + transform.forward * distance, radius, playerLayer);

        //transform.forward * distance = onunu daha fazla gormesi icin// gorus alaninda player varsa


        HashSet<Collider> newObjectsInSphere = new HashSet<Collider>();


        foreach (Collider enemy in enemies)
        {

            if (enemy.CompareTag("PlayerParts") && enemy != transform.GetComponentInChildren<Collider>())//... ve kendini gormesin. dogru calisiyo olmasi lazim
            {
                if (!targetCoolDown)
                {
                    newObjectsInSphere.Add(enemy);

                    //NPC TARGET

                    string thisShipCountryNum = transform.parent.tag;//ulke numarasini al bu geminin
                    string otherShipCountryNum = GameObject.FindGameObjectWithTag("DiplomacyManager").transform.GetChild(0).GetComponent<GetYourCountry>().GetCountryNum().ToString(); // karsilasdigi geminin ulke numarasini al

                    //DiplomacyManager'dan iki geminin ülkelerinin savasta olup olmadiginin kontrolu

                    bool areEnemies = GameObject.FindGameObjectWithTag("DiplomacyManager").GetComponent<DiplomacyManager>().AreAtWar(thisShipCountryNum, otherShipCountryNum); // bu iki ulke arasinda savas var mi
                    // Debug.Log("areEnemies ," + thisShipCountryNum + "," + otherShipCountryNum + " " + areEnemies + "\n");

                    //ENEMY TARGET

                    int thisShipUnitCount = GetComponent<NPCUnits>().GetNPCUnitCount();
                    int otherShipUnitCount = GameObject.FindGameObjectWithTag("UnitsManager").GetComponent<UnitsManager>().unitCount;

                    bool strongerThanEnemy = thisShipUnitCount > otherShipUnitCount;
                    Debug.Log("DÜŞMAN GÜÇLÜ MÜ:" + areEnemies);

                    if (!objectsInSphere.Contains(enemy) && areEnemies && strongerThanEnemy)//daha onceki frame'de bu obje yoksa ve (savas halindeyse)
                    {
                        Debug.Log("Truee");
                        if (GetComponent<SmoothAgentMovement>() != null)
                        {
                            gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy = true;
                        }
                        else if (GetComponent<SmoothNPCMovement>() != null)
                        {
                            gameObject.GetComponent<SmoothNPCMovement>().isTargetEnemy = true;
                        }
                    }

                }

            }
        }

        foreach (Collider oldObject in objectsInSphere)
        {
            if (!newObjectsInSphere.Contains(oldObject))
            {

                Debug.Log("Falsee");

                if (GetComponent<SmoothAgentMovement>() != null)
                {
                    gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy = false;
                }
                else if (GetComponent<SmoothNPCMovement>() != null)
                {
                    gameObject.GetComponent<SmoothNPCMovement>().isTargetEnemy = false;
                }
            }
        }

        objectsInSphere = newObjectsInSphere;

        //foreach (Collider enemy in enemies)
        //{
        //    if (enemy.CompareTag("PlayerParts"))
        //    {
        //        if (gameObject.GetComponent<SmoothAgentMovement>().isTargetEnemy == true)
        //        {
        //        if(gameObject.GetComponent)
        //        }

        //    }
        //}


    }


    // SPHERE GIZMOS

    //private void OnDrawGizmos()
    //{

    //    Gizmos.DrawSphere(transform.position + transform.forward * distance, radius);
    //}



}
