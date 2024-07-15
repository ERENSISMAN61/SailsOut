using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScreenText : MonoBehaviour
{

    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;

    private Camera mainCamera;


    float kacUzaklikGorsun = 70; // Kameran�n ka� birim uzakl���ndaki noktalar� kontrol edece�ini belirleyen de�i�ken

    void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        Vector3 pos = mainCamera.WorldToScreenPoint(lookAt.position + offset);
        //  Debug.Log("pos: "+pos);
        if (transform.position != pos)
        {
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }



        KameraGoruyorMu(lookAt.transform.position);

    }

    bool KameraGoruyorMu(Vector3 koordinat)
    {

        // Koordinat�n 30 birim uzakl���ndaki noktalar� da kontrol edelim
        Vector3 uzakNokta = koordinat + kacUzaklikGorsun * Vector3.forward;
        Vector3 ekranUzakNoktasi = Camera.main.WorldToScreenPoint(uzakNokta);
        bool kameraGoruyorUzak = ekranUzakNoktasi.z > 0 && ekranUzakNoktasi.x > 0 && ekranUzakNoktasi.x < Screen.width && ekranUzakNoktasi.y > 0 && ekranUzakNoktasi.y < Screen.height;

        if (kameraGoruyorUzak)
        {
            //     Debug.Log("Kamera+++++++++++++++++++++++++++++.");
            gameObject.transform.GetChild(0).gameObject.SetActive(kameraGoruyorUzak);
        }
        else
        {
            //   Debug.Log("Kamera-------------------------------");
            gameObject.transform.GetChild(0).gameObject.SetActive(kameraGoruyorUzak);
        }


        return kameraGoruyorUzak;
    }

    public void SetLookAt(Transform lookAt)
    {
        this.lookAt = lookAt;
    }

}
