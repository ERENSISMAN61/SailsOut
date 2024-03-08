using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floaters : MonoBehaviour
{

    // Gemiyi yüzdüren noktaları belirleyen bir dizi oluşturalım
    public Transform[] FloaterPoints;

    // Geminin suya girdiğinde uygulanacak kaldırma kuvvetinin büyüklüğünü belirleyelim
    public float FloatingPower = 2f;
    public float FloatingLevel = 1.8f;
    // Suyun yüzeyinin konumunu ve eğimini belirleyen bir nesne oluşturalım
    public WaterSurface water;

    // Suyun yüzeyine bir nokta yansıtmak için kullanılan parametreleri ve sonuçları tutan nesneler oluşturalım
    WaterSearchParameters Search;
    WaterSearchResult SearchResult;

    // Geminin fiziksel davranışını belirleyen bir nesne oluşturalım
    Rigidbody Rb;

    [Tooltip("Approxative radius of object for buoyancy.")]
    public float sphereRadiusApproximation = 0.25f;

    // Geminin suyun altında kalan noktalarının sayısını tutan bir değişken oluşturalım
    int FloatersUnderWater;

    // Height of the sphere that is underwater.
    private float h, hNormalized = 0;
    private Vector3 waterPosition;

    // Oyun başladığında çalışacak fonksiyonu tanımlayalım
    void Start()
    {
        // Geminin fiziksel davranışını alalım
        Rb = this.GetComponent<Rigidbody>();
    }

    public float GetNormalizedHeightOfSphereBelowSurface()
    {
        if (water != null)
        {
            // height of the object that is below the surface. 0 means overwater
            h = Mathf.Clamp(waterPosition.y - (transform.position.y - sphereRadiusApproximation), 0, 2 * sphereRadiusApproximation);

            // normalized height of the sphere that is underwater. 0 means overwater, 1 fully below water, in between [0,1]
            hNormalized = h * 1 / (2 * sphereRadiusApproximation);
        }
        return hNormalized;
    }

    // Her karede çalışacak fonksiyonu tanımlayalım
    void FixedUpdate()
    {
        // Suyun altında kalan noktaların sayısını sıfırlayalım
        FloatersUnderWater = 0;

        // Gemiyi yüzdüren noktaların her biri için
        for (int i = 0; i < FloaterPoints.Length; i++)
        {
            // Suyun yüzeyine noktayı yansıtmak için kullanılacak başlangıç noktasını noktanın konumu olarak belirleyelim
            Search.startPositionWS = FloaterPoints[i].position;

            // Suyun yüzeyine noktayı yansıtalım ve sonucu alalım
            water.ProjectPointOnWaterSurface(Search, out SearchResult);

            // Noktanın suyun yüzeyinden ne kadar aşağıda olduğunu hesaplayalım
            float diff = FloaterPoints[i].position.y - SearchResult.projectedPositionWS.y - FloatingLevel;

            // Eğer nokta suyun altındaysa
            if (diff < 0)
            {
                // Noktaya, suyun yüzeyine doğru bir kaldırma kuvveti uygulayalım
                float displacementMulti = Mathf.Clamp01(SearchResult.projectedPositionWS.y - transform.position.y / 2) * 10;
                Rb.AddForceAtPosition(FloatingPower * Mathf.Abs(diff) * Vector3.up, FloaterPoints[i].position, ForceMode.Acceleration);

                // Suyun altında kalan noktaların sayısını arttıralım
                FloatersUnderWater += 1;


            }
        }


    }



}