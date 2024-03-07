using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RecruitMenuScript : MonoBehaviour
{
    private GameObject recruitMenuObject;
    private GameObject spawnrecruitMenuPrefab;
    private GameObject playerObject;

    public bool canrecruitMenuOpen = false; //þehirler arasý recruitMenu açtýðýnda hatalar çýkabilir. çýkarsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool deðeri true yaparak iki bool ile çözülebilir

    public bool isrecruitMenuOpen = false;

    private bool onceOpened = false;
    void Start()
    {
        recruitMenuObject = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Menu Prefab/RecruimentMenu/RecruimentMenu");
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if (canrecruitMenuOpen)
        {
            spawnrecruitMenuPrefab = Instantiate(recruitMenuObject, GameObject.Find("Canvas").transform);


            isrecruitMenuOpen = true;

            //GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = true;
            
            // playerObject.GetComponent<SmoothPlayerMovement>().isrecruitMenuOpened = true;   // eðer recruitMenu instanciate edilmeyen bir sistem yaparsam sadece recruitMenui kapa aç sistemþ yaparsam bunlarýn deðiþmesi
            ////        recruitMenu ÝCÝN SÝLÝNDÝ MENU DE KULLANILDI CUNKU                                                                         //gerekir. þu anlýk bu þekilde yaptým.

            onceOpened = true;

            canrecruitMenuOpen = false;

        }




    }

    private void LateUpdate()
    {
        if (onceOpened && spawnrecruitMenuPrefab == null) //recruitMenu bir kere açýlmýþsa ve kapatýlmýþsa
        {

            Debug.Log("recruitMenu is closed");
       //     GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = false;
        //    playerObject.GetComponent<SmoothPlayerMovement>().isrecruitMenuOpened = false;                                // recruitMenu ÝCÝN SÝLÝNDÝ MENU DE KULLANILDI CUNKU
            isrecruitMenuOpen = false;

            onceOpened = false;
        }


    }

    public void SetMenuTrue()
    {
        canrecruitMenuOpen = true;
    }


}
