using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IslandMenuScript : MonoBehaviour
{
    private GameObject islandMenuObject;
    private GameObject spawnIslandMenuPrefab;
    private GameObject playerObject;

    public bool canIslandMenuOpen = false; //�ehirler aras� menu a�t���nda hatalar ��kabilir. ��karsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool de�eri true yaparak iki bool ile ��z�lebilir

    public bool isIslandMenuOpen = false;

    private bool onceOpened = false;

    [SerializeField] private float minFontSize = 30;//Island Text Font Size
    [SerializeField] private float maxFontSize = 100; //Island Text Font Size

    private float minYPosition = 0;//Island Text Font Size icin Camera Y pozisyonu
    private float maxYPosition = 1500;//Island Text Font Size icin Camera Y pozisyonu
    void Start()
    {
        islandMenuObject = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Menu Prefab/IslandMenu");
        playerObject = GameObject.FindGameObjectWithTag("Player");

        minYPosition = GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().GetFollowOffsetMinY();
        maxYPosition = GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().GetFollowOffsetMaxY();

    }
    void Update()
    {

        if (canIslandMenuOpen)
        {
            spawnIslandMenuPrefab = Instantiate(islandMenuObject, GameObject.Find("Canvas").transform);

            isIslandMenuOpen = true;

            GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = true;
            playerObject.GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = true;   // e�er menu instanciate edilmeyen bir sistem yaparsam sadece menu kapa a� sistem� yaparsam bunlar�n de�i�mesi
                                                                                           //gerekir. �u anl�k bu �ekilde yapt�m.

            onceOpened = true;

            canIslandMenuOpen = false;

            GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(0); //ZAMANI DURDUR

        }


        //KAMERA YUKSEKLIK DEGISIMINE GORE ISLAND TEXT FONT SIZE DEGISIMI
        /// !!!!!!!!!!!!!! GetChild(2) su an prefabde cizgi objeleri oldugu icin 3. obje su an island text. cizgi objeleri silinirse duzelt. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Kameranın Y pozisyonunu al
        float cameraYPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;

        // Kameranın Y pozisyonuna bağlı olarak font boyutunu hesapla
        float fontSize = Mathf.Lerp(minFontSize, maxFontSize, Mathf.InverseLerp(maxYPosition, minYPosition, cameraYPosition));

        // Hesaplanan font boyutunu ayarla
        transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = fontSize;


    }

    private void LateUpdate()
    {
        if (onceOpened && spawnIslandMenuPrefab == null) //menu bir kere a��lm��sa ve kapat�lm��sa
        {

            Debug.Log("Island Menu is closed");
            GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = false;
            playerObject.GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = false;
            isIslandMenuOpen = false;

            onceOpened = false;

            GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(1);//ZAMANI DEVAM ETTIR
        }


    }

    //public void SetMenuTrue()
    //{
    //    canmenuOpen = true;
    //}


}
