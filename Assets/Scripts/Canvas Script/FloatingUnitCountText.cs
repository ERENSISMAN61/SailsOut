using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingUnitCountText : MonoBehaviour
{

    public Transform target; // Health bar'ın görüneceği hedef transform

    private Camera mainCamera;

    //chil text/bar objesi için otomatik yükseklik ayarı
    private RectTransform uiElement; // Ayarlanacak UI elemanı
    [SerializeField] private float minCameraY = 80f; // Minimum kamera Y değeri
    [SerializeField] private float maxCameraY = 1000f; // Maksimum kamera Y değeri
    [SerializeField] private float minPosY = -70f; // Minimum UI elemanı Y pozisyonu
    [SerializeField] private float maxPosY = -20f; // Maksimum UI elemanı Y pozisyonu

    void Start()
    {
        mainCamera = Camera.main;
        uiElement = transform.GetChild(0).GetComponent<RectTransform>();

        TargetText();
    }

    void Update()
    {
        TargetText();
    }
    private void TargetText()
    {
        if (target != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position);
            transform.position = new Vector3(screenPosition.x, screenPosition.y, 0);
        }

        // Kamera Y konumunu al
        float cameraY = mainCamera.transform.position.y;

        // Kamera Y değeri aralığını ve UI elemanı Y pozisyonu aralığını normalleştir
        float t = Mathf.InverseLerp(minCameraY, maxCameraY, cameraY);
        float newPosY = Mathf.Lerp(minPosY, maxPosY, t);

        // UI elemanının Y pozisyonunu ayarla
        Vector3 newPosition = uiElement.localPosition;
        newPosition.y = newPosY;
        uiElement.localPosition = newPosition;
    }

    public void SetLookAt(Transform target)
    {
        this.target = target;
    }
}
