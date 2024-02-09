using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class SetDestinationIsland : MonoBehaviour
{
    GameObject playerObject;

    private Transform transformDock;
    private Vector3 offset;

    private bool canGetClose = false;
    private bool canStartAnimate = false;

    private float Dot, DotForward; //ilki(Dot) dot sað sol için, ikincisi(DotForward) ileri geri için

    private Vector3 hedefNokta;// Dot için

    void Start()
    {
        transformDock = gameObject.transform;

        offset = new Vector3(-60, 0, -60);
    }
    void Update()
    {

        // Debug.Log("Piano y: "+((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y-(transform.rotation.eulerAngles.y-90))));

        // Debug.Log("Merkeze Uzaklýðý: "+ Vector3.Distance(new Vector3(GameObject.FindWithTag("Player").gameObject.transform.position.x, 0, GameObject.FindWithTag("Player").gameObject.transform.position.z), new Vector3(transformDock.position.x, 0, transformDock.position.z)));
        //Debug.Log("StartPoint Uzaklýðý: "+ Vector3.Distance(new Vector3(GameObject.FindWithTag("Player").gameObject.transform.position.x, 0, GameObject.FindWithTag("Player").gameObject.transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)));



    

        if (canGetClose)
        {
            if (playerObject == null)//object null ise kontrol
            {
                //   Debug.Log("player object null. Atama yapýlýyor.");
                playerObject = GameObject.FindWithTag("Player");
            }







            //  Debug.Log("Az Player position: " + GameObject.FindWithTag("Player").transform.position);
            //  Debug.Log("Az Dock position: " + transformDock.position + offset);

            //Debug.Log("Az Distance: " + Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)));
            //Y leri katmadan ne kadar yakýn olduðunu ölçtük

            if (Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)) <= 5) //animasyon baþlangýcýna vardýysa
            {       //yleri katmadan 5den daha yakýnlarsa  baþlat
                canStartAnimate = true;


            }



            if (canStartAnimate)
            {


                playerObject.GetComponent<SmoothPlayerMovement>().isStartDockAnimate = true; // Iskele animasyonu basladiginda herhangi bir yere gidemesin

                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!YAPILACAK: enemy de yakalayabilir mi deðiþkeni kapanmasý lazým !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                //  Debug.Log("Az Rotation is started");



                // ----------------------DONME ANIMASYONU-----------------------------

                // float t = 1f - Mathf.Pow(1f - Time.deltaTime, 1.5f);
                playerObject.gameObject.transform.rotation= Quaternion.Lerp(playerObject.gameObject.transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y-90, 0f), 1.5f* Time.deltaTime);// 1.5f * Time.deltaTime
                                                                                                                                                                                                             //döndürme kodu



                // ----------------------ILERLEME ANIMASYONU-----------------------------

                // playerObject.gameObject.transform.Translate(Vector3.forward * 30f * Time.deltaTime); // z ekseninde ilerleme kodu

                playerObject.gameObject.transform.position = Vector3.Lerp(playerObject.gameObject.transform.position, new Vector3(playerObject.transform.position.x + playerObject.transform.forward.x *11f, playerObject.transform.position.y, playerObject.transform.position.z + playerObject.transform.forward.z *11f), 1.5f * Time.deltaTime); // 1.5f * Time.deltaTime





                //---------------ÝSKELENÝN TAM YANINDA MI DOT ÝLE KONTROL----------------
                 hedefNokta = Vector3.Normalize(GameObject.FindWithTag("Player").gameObject.transform.position - transform.position);

                DotForward = Vector3.Dot(transform.right, hedefNokta);// ilerideyse -1, gerideyse 1, tam yanýnda 0 döner.



                //-------------------ROTASYON TAMAMLANDI MI KONTROL----------------------
                //Rotation tamamlandýðýnda ilerleme animasyonu da duruyo  çünkü bloðu false yapýp kapatýyoruz.
                if (((((playerObject.gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) <= 1f && ((playerObject.gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) >= -1f)
                        || (((playerObject.gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) >=359f && ((playerObject.gameObject.transform.rotation.eulerAngles.y)-(transform.rotation.eulerAngles.y-90)) <= 361f))
                        && (DotForward <= 0.1f  &&  DotForward >= -0.1f)) //rotasyon tamamlandýysa ve hedefin tam yanýna geldiyse
                {

                    playerObject.GetComponent<SmoothPlayerMovement>().isStartDockAnimate = false;

                    canStartAnimate = false;
                    canGetClose = false;

                }



            }

        }
    }

    public void SetDestToDock()
    {
        playerObject = GameObject.FindWithTag("Player");



        //Aþaðýda yazacaðým kod ile hedefPozisyon'a göre benimPozisyonum saðda mý solda mý diye kontrol
        Vector3 dirToTarget = Vector3.Normalize(playerObject.transform.position - transform.position); // playerdan benim pozisyonumu çýkarýp normalize ettim. Normalize etmezsen uzaklýk ne kadar olursa olsun 1 olurmþ

        Dot = Vector3.Dot(transform.forward, dirToTarget);//saðýndaysa 1, solundaysa -1, tam karþýsý/arkasý 0 döner.
                                                          // Debug.Log("Dot right/left: " + Dot);


        if (Dot>=0) // saðdaysa
        {
            offset = new Vector3(70, 0, -70); //Z koordinatý iskelenin ne kadar gerisinda animasyonun baþlayacaðýný belirler. x koordinatý saðda mý solda mý animasyonun baþlayacaðýný belirler.
        }
        else if (Dot<0) // soldaysa veya tam karþýsýndaysa
        {
            offset = new Vector3(-40, 0, -70);
        }



        playerObject.GetComponent<SmoothPlayerMovement>().isDestinationSet  = true;



        //iskeleye uzaklýðýmýz
        //  Debug.Log("PPP UZAKLIK: "+Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock.position.x, 0, transformDock.position.z)));

        if (Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock.position.x, 0, transformDock.position.z))>95) //PLAYER iskeleye yakýn deðilse iskeleye git
        {                                                                                                                                                   //eðer iskeledeyse tekrar iskeleye yönelme
                                                                                                                                                            //  Debug.Log("PPP DISARDAdeyiz");

            playerObject.GetComponent<SmoothPlayerMovement>().SetDestinationPlus(new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z));
            // Debug.Log("Destination set to: " + transformDock.position + offset);


            canGetClose = true;

        }
        else
        {
            if (playerObject.GetComponent<SmoothPlayerMovement>().PathLocations != new Vector3[0])  //iskele içindeyken baþka bir yöne gitmeye çalýþýrken  ada tuþuna týklanýrsa orda kalsýn gitmeye çalýþtýðý yer iptal olsun.
            {
                playerObject.GetComponent<SmoothPlayerMovement>().PathLocations = new Vector3[0];

            }
        }


    }




}
