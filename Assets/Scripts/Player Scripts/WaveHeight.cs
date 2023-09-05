using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class WaveHeight : MonoBehaviour
{
    public float speed = 10f; // geminin ileri doğru hızı
    public float turnSpeed = 5f; // geminin dönüş hızı
    public float buoyancy = 9.81f; // yerçekimine karşı uygulanan yukarı doğru kuvvet
    public float drag = 5f; // suyun sürükleme kuvveti
    public float angularDrag = 0.05f; // suyun açısal sürükleme kuvveti

    public WaterSurface water; // Water Surface nesnesine referans
    WaterSearchParameters searchParams; // arama parametreleri yapısı
    WaterSearchResult searchResult; // arama sonucu yapısı

    private Rigidbody rb;
    private MeshCollider mc;
    private Vector3[] vertices; // mesh çarpıştırıcının köşe noktaları

    void Awake()
    {
        rb = gameObject.GetComponentInParent<Rigidbody>();
        mc = gameObject.GetComponent<MeshCollider>();
        vertices = mc.sharedMesh.vertices;
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f); // kütle merkezini ayarla
    }

    void FixedUpdate()
    {
        // ileri doğru kuvvet uygula
        rb.AddForce(transform.forward * speed * Time.fixedDeltaTime);

        // girdiye göre dön
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddTorque(transform.up * horizontalInput * turnSpeed * Time.fixedDeltaTime);

        // her köşe noktasına göre kaldırma kuvveti uygula
        foreach (Vector3 vertex in vertices)
        {
            // köşe noktasının dünya koordinatlarını hesapla
            Vector3 worldPoint = transform.TransformPoint(vertex);

            // arama parametrelerini oluştur

            // su seviyesini ve normalini bul
            water.FindWaterSurfaceHeight(searchParams, out searchResult);

            // köşe noktasının su seviyesinin altında olup olmadığını kontrol et
            if (worldPoint.y < searchResult.height)
            {
                // kaldırma kuvvetinin büyüklüğünü hesapla
                float displacement = Mathf.Clamp01(searchResult.height - worldPoint.y);
                float forceMagnitude = displacement * buoyancy;

                // kaldırma kuvvetini uygula
                float force = searchResult.height * forceMagnitude;
                rb.AddForceAtPosition(new Vector3(0f, force, 0f),worldPoint, ForceMode.Acceleration);

                // sürükleme kuvvetlerini uygula
                rb.AddForce(displacement * -rb.velocity * drag * Time.fixedDeltaTime, ForceMode.VelocityChange);
                rb.AddTorque(displacement * -rb.angularVelocity * angularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }
    }
}
