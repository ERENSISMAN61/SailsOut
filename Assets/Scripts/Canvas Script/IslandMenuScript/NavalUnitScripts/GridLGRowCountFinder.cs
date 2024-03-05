using UnityEngine;
using UnityEngine.UI;

public class GridLGRowCountFinder : MonoBehaviour  //Grid Layout Group bileþeninin Constraint Count Column yani kaç sütunlu grid oluþturacaksak ona göre bir menü oluþturmak için


//-------------!!!!  6 sütunlu yapmak istiyosak buraya 6 prefab koymamýz ve Constrainrt Count'u da 6 yapmamýz gerekiyor.   /þu anlýk 5.  !!!!!!----------


{
    public GameObject[] prefablar; // Prefablarý buraya sürükleyin (a_prefab, b_prefab, c_prefab, vb.)
    public GameObject emptyPrefab; // Boþ prefabý buraya sürükleyin

    void Start()
    {
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        int toplamSutun = gridLayoutGroup.constraintCount;

        // Her sütun için rastgele ürün sayýlarý oluþturun
        int[] urunSayilari = new int[toplamSutun];
        for (int i = 0; i < toplamSutun; i++)
        {
            urunSayilari[i] = Random.Range(1, 4); // Sütun baþýna 1 ila 3 arasýnda rastgele ürün sayýsý
        }

        int prefabIndeksi = 0; // a_prefab ile baþlayýn

        for (int satir = 0; satir < 3; satir++) // 3 satýr
        {
            for (int sutun = 0; sutun < toplamSutun; sutun++)
            {
                int urunSayisi = urunSayilari[sutun];

                if (urunSayisi > 0)
                {
                    // Prefabý oluþturun
                    GameObject yeniNesne = Instantiate(prefablar[prefabIndeksi]);

                    // Prefabýn adýný ayarlayýn (isteðe baðlý)
                    yeniNesne.name = $"{prefablar[prefabIndeksi].name} (Sýra {sutun + 1}, {satir + 1})";

                    // Bir sonraki prefabý seçin
                    prefabIndeksi = (prefabIndeksi + 1) % prefablar.Length;

                    // Yeni nesneyi Grid Layout Group'un altýna ekleyin
                    yeniNesne.transform.SetParent(transform);

                    // Bu sütun için ürün sayýsýný azaltýn
                    urunSayilari[sutun]--;
                }
                else
                {
                    // Boþ bir prefabý oluþturun
                    GameObject bosNesne = Instantiate(emptyPrefab);

                    // Boþ prefabýn adýný ayarlayýn (isteðe baðlý)
                    bosNesne.name = $"Boþ (Sýra {sutun + 1}, {satir + 1})";

                    // Boþ nesneyi Grid Layout Group'un altýna ekleyin
                    bosNesne.transform.SetParent(transform);
                }
            }
        }
    }
}

//void Start()
//{
//    // Grid Layout Group bileþenini alýn
//    GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

//    // Constraint Count deðerini bulun
//    int constraintCount = gridLayoutGroup.constraintCount;

//    // Sonucu konsola yazdýrýn
//    Debug.Log($"Constraint Count: {constraintCount}");
//}

