using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class SetDestinationIsland : MonoBehaviour
{
    GameObject playerObject;

    public List<Transform> transformDock = new List<Transform>();
    //[SerializeField] private Transform[] transformDock;
    private Vector3 offset, offsetEnd;

    private bool canGetClose = false;
    private bool canStartAnimate = false;

    private float Dot, DotForward; //ilki(Dot) dot sa� sol i�in, ikincisi(DotForward) ileri geri i�in

    private Vector3 hedefNokta;// Dot i�in

    public GameObject IslandMenuObjectScript;

    private int i = 0; // dock number

    void Start()
    {
        // transformDock = gameObject.transform;
        IslandMenuObjectScript = gameObject.transform.parent.gameObject;
        offset = new Vector3(-60, 0, -60);
    }
    void Update()
    {

        // Debug.Log("Piano y: "+((GameObject.FindWithTag("Player").gameObject.transform.rotation.eulerAngles.y-(transform.rotation.eulerAngles.y-90))));

        // Debug.Log("Merkeze Uzakl���: "+ Vector3.Distance(new Vector3(GameObject.FindWithTag("Player").gameObject.transform.position.x, 0, GameObject.FindWithTag("Player").gameObject.transform.position.z), new Vector3(transformDock.position.x, 0, transformDock.position.z)));
        //Debug.Log("StartPoint Uzakl���: "+ Vector3.Distance(new Vector3(GameObject.FindWithTag("Player").gameObject.transform.position.x, 0, GameObject.FindWithTag("Player").gameObject.transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)));





        if (canGetClose)
        {
            if (playerObject == null)//object null ise kontrol
            {
                //   Debug.Log("player object null. Atama yap�l�yor.");
                playerObject = GameObject.FindWithTag("Player");
            }







            //  Debug.Log("Az Player position: " + GameObject.FindWithTag("Player").transform.position);
            //  Debug.Log("Az Dock position: " + transformDock.position + offset);

            //Debug.Log("Az Distance: " + Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock.position.x + offset.x, 0, transformDock.position.z + offset.z)));
            //Y leri katmadan ne kadar yak�n oldu�unu �l�t�k

            if (Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock[i].position.x + offset.x, 0, transformDock[i].position.z + offset.z)) <= 5) //animasyon ba�lang�c�na vard�ysa
            {       //yleri katmadan 5den daha yak�nlarsa  ba�lat
                canStartAnimate = true;

                offsetEnd = playerObject.gameObject.transform.position + transformDock[i].right * -80f;

            }



            if (canStartAnimate)
            {


                playerObject.GetComponent<SmoothPlayerMovement>().isStartDockAnimate = true; // Iskele animasyonu basladiginda herhangi bir yere gidemesin

                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!YAPILACAK: enemy de yakalayabilir mi de�i�keni kapanmas� laz�m !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                //  Debug.Log("Az Rotation is started");



                // ----------------------DONME ANIMASYONU-----------------------------

                // float t = 1f - Mathf.Pow(1f - Time.deltaTime, 1.5f); 
                playerObject.gameObject.transform.rotation = Quaternion.Lerp(playerObject.gameObject.transform.rotation, Quaternion.Euler(0f, transformDock[i].rotation.eulerAngles.y - 90, 0f), 1.5f * Time.deltaTime);// 1.5f * Time.deltaTime
                                                                                                                                                                                                                        //d�nd�rme kodu



                // ----------------------ILERLEME ANIMASYONU-----------------------------

                // playerObject.gameObject.transform.position = Vector3.Lerp(playerObject.gameObject.transform.position, new Vector3(playerObject.transform.position.x + playerObject.transform.forward.x *11f, playerObject.transform.position.y, playerObject.transform.position.z + playerObject.transform.forward.z *11f), 1.5f * Time.deltaTime); // 1.5f * Time.deltaTime

                if (((((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) <= 50f && ((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) >= -50f)
                        || (((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) >= 309f && ((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) <= 411f)))
                {
                    playerObject.gameObject.transform.position = Vector3.Lerp(playerObject.gameObject.transform.position, offsetEnd, 0.4f * Time.deltaTime); // d�nme animasyonu 50 dereceden az kald�ysa daha h�zl� ilerle
                }
                else if (((((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) <= 90f && ((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) >= -90f)
                        || (((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) >= 269f && ((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) <= 441f)))
                {
                    playerObject.gameObject.transform.position = Vector3.Lerp(playerObject.gameObject.transform.position, offsetEnd, 0.2f * Time.deltaTime); // d�nme animasyonu 90 dereceden az kald�ysa yava��a ilerle
                }





                //---------------�SKELEN�N TAM YANINDA MI DOT �LE KONTROL----------------
                hedefNokta = Vector3.Normalize(GameObject.FindWithTag("Player").gameObject.transform.position - transformDock[i].position);

                DotForward = Vector3.Dot(transformDock[i].right, hedefNokta);// ilerideyse -1, gerideyse 1, tam yan�nda 0 d�ner.





                //-------------------ROTASYON TAMAMLANDI MI KONTROL----------------------
                //Rotation tamamland���nda ilerleme animasyonu da duruyo  ��nk� blo�u false yap�p kapat�yoruz.
                if (((((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) <= 1f && ((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) >= -1f)
                        || (((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) >= 359f && ((playerObject.gameObject.transform.rotation.eulerAngles.y) - (transformDock[i].rotation.eulerAngles.y - 90)) <= 361f))
                        && (((Vector3.Distance(playerObject.transform.position, offsetEnd)) <= 5f) || (DotForward <= 0.1f && DotForward >= -0.1f))) //rotasyon tamamland�ysa ve hedefin tam yan�na geldiyse //DotForward <= 0.1f  &&  DotForward >= -0.1f buydu de�i�tik
                {
                    Debug.Log("Rotation is finished");
                    playerObject.GetComponent<SmoothPlayerMovement>().isStartDockAnimate = false;

                    canStartAnimate = false;
                    canGetClose = false;

                    playerObject.GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = true; // menu a��kken ba�ka bir yere gidemesin
                    GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = true; // kamera hareket edemesin
                    IslandMenuObjectScript.GetComponent<IslandMenuScript>().canIslandMenuOpen = true; //men�y� a�


                }



            }

        }
    }

    public void SetDestToDock()
    {


        playerObject = GameObject.FindWithTag("Player");

        float dockDistance = 0;
        int j = 0;
        foreach (Transform tr in transformDock)
        {
            float newdockDistance = Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(tr.position.x, 0, tr.position.z));
            if (newdockDistance < dockDistance || dockDistance == 0)
            {
                dockDistance = newdockDistance;
                i = j;
            }
            j++;
        }

        if (playerObject.GetComponent<SmoothPlayerMovement>().isStartDockAnimate == false) //iskele animasyonu ba�lam��sa ba�ka iskeleye gidemesin
        {

            //A�a��da yazaca��m kod ile hedefPozisyon'a g�re benimPozisyonum sa�da m� solda m� diye kontrol
            Vector3 dirToTarget = Vector3.Normalize(playerObject.transform.position - transformDock[i].position); // playerdan benim pozisyonumu ��kar�p normalize ettim. Normalize etmezsen uzakl�k ne kadar olursa olsun 1 olurm�

            Dot = Vector3.Dot(transformDock[i].forward, dirToTarget);//sa��ndaysa 1, solundaysa -1, tam kar��s�/arkas� 0 d�ner.
                                                                     // Debug.Log("Dot right/left: " + Dot);


            if (Dot >= 0) // sa�daysa
            {
                // offset = new Vector3(70, 0, -70); //Z koordinat� iskelenin ne kadar gerisinda animasyonun ba�layaca��n� belirler. x koordinat� sa�da m� solda m� animasyonun ba�layaca��n� belirler.

                offset = transformDock[i].right * 70 + transformDock[i].forward * 50;
                //   offsetEnd = transformDock.right *
            }
            else if (Dot < 0) // soldaysa veya tam kar��s�ndaysa
            {
                // offset = new Vector3(-40, 0, -70);
                offset = transformDock[i].right * 70 + transformDock[i].forward * -50;
            }



            playerObject.GetComponent<SmoothPlayerMovement>().isDestinationSet = true;



            //iskeleye uzakl���m�z
            //  Debug.Log("PPP UZAKLIK: "+Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock.position.x, 0, transformDock.position.z)));

            if (Vector3.Distance(new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z), new Vector3(transformDock[i].position.x, 0, transformDock[i].position.z)) > 95) //PLAYER iskeleye yak�n de�ilse iskeleye git
            {                                                                                                                                                   //e�er iskeledeyse tekrar iskeleye y�nelme
                                                                                                                                                                //  Debug.Log("PPP DISARDAdeyiz");

                playerObject.GetComponent<SmoothPlayerMovement>().SetDestinationPlus(new Vector3(transformDock[i].position.x + offset.x, 0, transformDock[i].position.z + offset.z));
                // Debug.Log("Destination set to: " + transformDock.position + offset);


                canGetClose = true;

            }
            else
            {
                if (playerObject.GetComponent<SmoothPlayerMovement>().PathLocations != new Vector3[0])  //iskele i�indeyken ba�ka bir y�ne gitmeye �al���rken  ada tu�una t�klan�rsa orda kals�n gitmeye �al��t��� yer iptal olsun.
                {
                    playerObject.GetComponent<SmoothPlayerMovement>().PathLocations = new Vector3[0];


                    playerObject.GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = true; // menu a��kken ba�ka bir yere gidemesin
                    GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = true; // kamera hareket edemesin
                    IslandMenuObjectScript.GetComponent<IslandMenuScript>().canIslandMenuOpen = true; //men�y� a�

                }
            }


        }


    }

}
