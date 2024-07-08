using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    // Gemiyi yüzdüren noktaları belirleyen bir dizi oluşturalım
    public Transform[] Floaters;

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

    
    // Geminin suyun altında kalan noktalarının sayısını tutan bir değişken oluşturalım
    int FloatersUnderWater;

    // Oyun başladığında çalışacak fonksiyonu tanımlayalım
    void Start()
    {
        // Geminin fiziksel davranışını alalım
        Rb = this.GetComponent<Rigidbody>();
    }

    

    // Her karede çalışacak fonksiyonu tanımlayalıms
    void FixedUpdate()
    {
        // Suyun altında kalan noktaların sayısını sıfırlayalım
        FloatersUnderWater = 0;

        // Gemiyi yüzdüren noktaların her biri için
        for (int i = 0; i < Floaters.Length; i++)
        {
            // Suyun yüzeyine noktayı yansıtmak için kullanılacak başlangıç noktasını noktanın konumu olarak belirleyelim
            Search.startPositionWS = Floaters[i].position;

            // Suyun yüzeyine noktayı yansıtalım ve sonucu alalım
            water.ProjectPointOnWaterSurface(Search, out SearchResult);

            // Noktanın suyun yüzeyinden ne kadar aşağıda olduğunu hesaplayalım
            float diff = Floaters[i].position.y - SearchResult.projectedPositionWS.y - FloatingLevel;

            // Eğer nokta suyun altındaysa
            if (diff < 0)
            {
                // Noktaya, suyun yüzeyine doğru bir kaldırma kuvveti uygulayalım
                float displacementMulti = Mathf.Clamp01(SearchResult.projectedPositionWS.y - transform.position.y / 2) * 10;
                Rb.AddForceAtPosition(FloatingPower * Mathf.Abs(diff) * Vector3.up, Floaters[i].position, ForceMode.Acceleration);
               
                // Suyun altında kalan noktaların sayısını arttıralım
                FloatersUnderWater += 1;

                
            }
        }

       
    }

    
   
}
