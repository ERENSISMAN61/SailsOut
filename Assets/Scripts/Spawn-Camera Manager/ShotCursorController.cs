using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotCursorController : MonoBehaviour
{
    private RectTransform rectTransform;
    private PlayerFire playerFire;
    private bool isSecondCameraActive = false;  // İkinci kameranın aktif olup olmadığını kontrol eden flag

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        playerFire = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFire>();
        this.gameObject.GetComponent<Image>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButtonUp(1))  // Sağ tıklama kontrolü
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            
            //isSecondCameraActive = !isSecondCameraActive;  // Kamera durumunu değiştir
            Cursor.visible = false;  // İkinci kamera aktifse imleci gizle, değilse göster
            //Debug.Log("Second Camera Active: " + isSecondCameraActive);
        }
        if(Input.GetMouseButtonUp(1))  // Sağ tıklama bırakıldığında
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            //Cursor.visible = false;
        }

        if (Cursor.visible == false)
        {
            Vector2 cursorPosition = Input.mousePosition;  // Fare pozisyonunu al
            Debug.Log("Cursor Position: " + cursorPosition);
            rectTransform.position = cursorPosition;  // İmlecin pozisyonunu güncelle
        }
    }

    
}
