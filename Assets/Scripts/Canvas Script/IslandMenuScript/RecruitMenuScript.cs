using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RecruitMenuScript : MonoBehaviour
{
    private GameObject recruitMenuObject;
    private GameObject spawnrecruitMenuPrefab;
    private GameObject playerObject;

    public bool canrecruitMenuOpen = false; //�ehirler aras� recruitMenu a�t���nda hatalar ��kabilir. ��karsa ada butonuna setMenutrue fnksiyonu ekleyip bir bool de�eri true yaparak iki bool ile ��z�lebilir

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
            
            // playerObject.GetComponent<SmoothPlayerMovement>().isrecruitMenuOpened = true;   // e�er recruitMenu instanciate edilmeyen bir sistem yaparsam sadece recruitMenui kapa a� sistem� yaparsam bunlar�n de�i�mesi
            ////        recruitMenu �C�N S�L�ND� MENU DE KULLANILDI CUNKU                                                                         //gerekir. �u anl�k bu �ekilde yapt�m.

            onceOpened = true;

            canrecruitMenuOpen = false;

        }




    }

    private void LateUpdate()
    {
        if (onceOpened && spawnrecruitMenuPrefab == null) //recruitMenu bir kere a��lm��sa ve kapat�lm��sa
        {

            Debug.Log("recruitMenu is closed");
       //     GameObject.FindGameObjectWithTag("CameraSystem").GetComponent<CameraSystem>().isCameraStopped = false;
        //    playerObject.GetComponent<SmoothPlayerMovement>().isrecruitMenuOpened = false;                                // recruitMenu �C�N S�L�ND� MENU DE KULLANILDI CUNKU
            isrecruitMenuOpen = false;

            onceOpened = false;
        }


    }

    public void SetMenuTrue()
    {
        canrecruitMenuOpen = true;
    }


}
