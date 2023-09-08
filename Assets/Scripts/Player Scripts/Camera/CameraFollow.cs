using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Takip edilecek gemi GameObject'i için bir referans oluşturalım
    public Transform target;

    // Kameranın gemiden uzaklığını belirleyelim
    public float distance = 10f;

    // Kameranın geminin üzerindeki yüksekliğini belirleyelim
    public float height = 5f;

    // Kameranın gemiyi takip etme hızını belirleyelim
    public float smoothSpeed = 0.5f;

    // Kameranın açısını değiştirmek için fare hareketlerini okuyalım
    public float mouseSensitivity = 100f;

    // Kameranın yatay ve dikey açılarını saklayalım
    private float xAngle = 0f;
    private float yAngle = 0f;

    // Oyun başladığında fare imlecini gizleyelim
    

    // Her karede çalışacak fonksiyonu tanımlayalım
    private void Update()
    {
        // Fare hareketlerini okuyalım ve kameranın açılarını güncelleyelim
        xAngle += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        yAngle -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Kameranın dikey açısını sınırlayalım
        yAngle = Mathf.Clamp(yAngle, -10f, 20f);

        // Kameranın rotasyonunu hesaplayalım
        
        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0f);

        // Kameranın pozisyonunu hesaplayalım
        Vector3 direction = new Vector3(0f, height, -distance);
        Vector3 position = target.position + rotation * direction;

        // Kameranın pozisyonunu ve rotasyonunu yumuşak bir şekilde güncelleyelim
        transform.position = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smoothSpeed);

        // Kameranın gemiyi görmesini sağlayalım
        transform.LookAt(target);
    }
}
