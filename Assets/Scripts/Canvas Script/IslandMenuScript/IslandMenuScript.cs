using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMenuScript : MonoBehaviour
{
    private GameObject islandMenuObject;
    private GameObject spawnIslandMenuPrefab;
    private GameObject playerObject;

    public bool canIslandMenuOpen = false; //þehirler arasý menu açtýðýnda hatalar çýkabilir. çýkarsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool deðeri true yaparak iki bool ile çözülebilir

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
            playerObject.GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = true;   // eðer menu instanciate edilmeyen bir sistem yaparsam sadece menu kapa aç sistemþ yaparsam bunlarýn deðiþmesi
                                                                                       //gerekir. þu anlýk bu þekilde yaptým.

            onceOpened = true;

            canIslandMenuOpen = false;

        }




    }

    private void LateUpdate()
    {
        if (onceOpened && spawnIslandMenuPrefab == null) //menu bir kere açýlmýþsa ve kapatýlmýþsa
        {

            Debug.Log("Island Menu is closed");
            GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = false;
            playerObject.GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = false;
            isIslandMenuOpen = false;

            onceOpened = false;
        }


    }

    //public void SetMenuTrue()
    //{
    //    canmenuOpen = true;
    //}


}
