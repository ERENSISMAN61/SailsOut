using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestinationIsland : MonoBehaviour
{
    private Transform transformDock;
    [SerializeField] private Vector3 offset;

    private bool canGetClose = false;
    private bool canStartAnimate = false;
    void Start()
    {
        transformDock = gameObject.transform;


    }
    void Update()
    {
        Debug.Log("Piano y: "+((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y-(transform.rotation.eulerAngles.y-90))));
     


        if (canGetClose)
        {
            Debug.Log("Az canGetClose True");
            //  Debug.Log("Az Player position: " + GameObject.FindWithTag("Player").transform.position);
            //  Debug.Log("Az Dock position: " + transformDock.position + offset);

            Debug.Log("Az Distance: " + Vector3.Distance(new Vector3(GameObject.FindWithTag("Player").transform.position.x, 0, GameObject.FindWithTag("Player").transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)));
            //Y leri katmadan ne kadar yakýn olduðunu ölçtük

            if (Vector3.Distance(new Vector3(GameObject.FindWithTag("Player").transform.position.x, 0, GameObject.FindWithTag("Player").transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)) <= 5) //animasyon baþlangýcýna vardýysa
            {       //yleri katmadan 5den daha yakýnlarsa  baþlat
                canStartAnimate = true;
            }
            if (canStartAnimate)
            {
                GameObject.FindWithTag("Player").GetComponent<SmoothPlayerMovement>().isStartDockAnimate = true; // Iskele animasyonu basladiginda herhangi bir yere gidemesin
                                                                                                                 
                                                        // !!!!!!!YAPILACAK: enemy de yakalayabilir mi deðiþkeni kapanmasý lazým !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Debug.Log("Az Rotation is started");



                // float t = 1f - Mathf.Pow(1f - Time.deltaTime, 1.5f);
                      GameObject.FindWithTag("Player").gameObject.transform.rotation= Quaternion.Lerp(GameObject.FindWithTag("Player").gameObject.transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y-90, 0f), 1.5f* Time.deltaTime );// 1.5f * Time.deltaTime
                //döndürme kodu


                //// Örneðin, 5 adýmda hedefe ulaþsýn
                //GameObject player = GameObject.FindWithTag("Player");
                //Quaternion targetRotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y - 90f, 0f);
                //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, t);




                  GameObject.FindWithTag("Player").gameObject.transform.Translate(Vector3.forward * 50f * Time.deltaTime);//BAKILACAK DETAYLI



                if ( (((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) <= 1f && ((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) >= -1f )
                || (((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) >=359f && ((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) <= 361f))
                {
                    canGetClose = false;
                    canStartAnimate = false;

                    GameObject.FindWithTag("Player").GetComponent<SmoothPlayerMovement>().isStartDockAnimate = false;

                    Debug.Log("Azzz Rotation is done");
                }


                //if (GameObject.FindWithTag("Player").gameObject.transform.rotation.y - (transform.rotation.y-0.73084352f)<=0.01 && GameObject.FindWithTag("Player").gameObject.transform.rotation.y - (transform.rotation.y-0.73084352f)>=(-0.01))
                //    //birbirine yaklaþtýysa düzeltilmesi gerekebilir

                //{
                //    Debug.Log("Az Rotation is done");
                //    canGetClose = false;
                //    canStartAnimate = false;
                //}
            }
        }
    }
    public void SetDestToDock()
    {
        GameObject.FindWithTag("Player").GetComponent<SmoothPlayerMovement>().isDestinationSet  = true;

        GameObject.FindWithTag("Player").GetComponent<SmoothPlayerMovement>().SetDestinationPlus(new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z));
        Debug.Log("Destination set to: " + transformDock.position + offset);

        canGetClose = true;
    }
}
