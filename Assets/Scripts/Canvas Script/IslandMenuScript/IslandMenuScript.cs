using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMenuScript : MonoBehaviour
{
    private GameObject islandMenuObject;
    private GameObject spawnIslandMenuPrefab;
    private GameObject playerObject;

    public bool canIslandMenuOpen = false; //�ehirler aras� menu a�t���nda hatalar ��kabilir. ��karsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool de�eri true yaparak iki bool ile ��z�lebilir

    public bool isIslandMenuOpen = false;

    private bool onceOpened = false;
    void Start()
    {
        islandMenuObject = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Menu Prefab/IslandMenu");
        playerObject = GameObject.FindGameObjectWithTag("Player");
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

            Time.timeScale = 1; //ZAMANI DEVAM ETTIR
        }


    }

    //public void SetMenuTrue()
    //{
    //    canmenuOpen = true;
    //}


}
