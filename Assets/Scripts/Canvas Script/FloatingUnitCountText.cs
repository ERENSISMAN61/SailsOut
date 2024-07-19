using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingUnitCountText : MonoBehaviour
{
    [SerializeField] private Transform target; // Güç puanını göstermek istediğiniz 3D obje
    [SerializeField] private Vector3 offset;   // Text'in 3D obje ile arasındaki mesafe (opsiyonel)
    private RectTransform uiText;              // UI Text elemanının RectTransform'u

    void Start()
    {
        // Text elemanının RectTransform'unu al
        uiText = transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Update()
    {
        // Geminin altına sabit bir pozisyon belirleme
        Vector3 worldPosition = target.TransformPoint(offset);
        // Bu dünya pozisyonunu ekran pozisyonuna çevirme
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Text elemanını ekran pozisyonuna yerleştirme
        uiText.position = screenPosition;

        // Text'in rotasyonunu sabit tutma
        uiText.rotation = Quaternion.identity;
    }

    public void SetLookAt(Transform target)
    {
        this.target = target;
    }
}
