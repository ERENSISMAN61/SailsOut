using UnityEngine;
using UnityEngine.UI;

public class GridLGRowCountFinder : MonoBehaviour  //Unit sayýsýna göre column sayýsýný otomatik belirliyoruz.


//-------------!!!!  6 sütunlu yapmak istiyosak root  objesindeki RecruimentUnitScript componentine bir tane daha scriptable obje eklememiz yeterli !!!!!!----------


{

    [SerializeField] private GameObject FillablePrefab; // Doldurulabilir prefabý buraya sürükleyin
    [SerializeField] private GameObject emptyPrefab; // Boþ prefabý buraya sürükleyin

    void Start()
    {

       int unitCount= transform.parent.parent.parent.GetComponent<RecruimentUnitScript>().GetUnitCount();  //Unit sayýsýný almak için

        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = unitCount; //Constraint Count'u unit sayýsýna eþitlemek için  (Yani Unit Sayýsýna göre column sayýsýný otomatik belirliyoruz)
        int toplamSutun = gridLayoutGroup.constraintCount;


        // Her sütun için rastgele ürün sayýlarý oluþturun
        int[] urunSayilari = new int[toplamSutun];
        for (int i = 0; i < toplamSutun; i++)
        {
            urunSayilari[i] = Random.Range(1, 4); // Sütun baþýna 1 ila 3 arasýnda rastgele ürün sayýsý
        }

        int prefabIndeksi = 0; // 1. unit ile baslamak icin

        for (int satir = 0; satir < 3; satir++) // 3 satýr
        {
            for (int sutun = 0; sutun < toplamSutun; sutun++)
            {
                int urunSayisi = urunSayilari[sutun];

                if (urunSayisi > 0)
                {
                    // Prefabý oluþturun
                    GameObject yeniNesne = Instantiate(FillablePrefab);  // doldurulabilir prefabý oluþturmak için

                    //Recruiment (Root) objesindeki ürünleri scriptableobject olarak almak için                                                             
                    yeniNesne.GetComponent<NavalUnitConfig>().SetNavalUnitContainer(transform.parent.parent.parent.GetComponent<RecruimentUnitScript>().GetUnits(prefabIndeksi));
                      




                    // Prefabýn adýný ayarlayýn (isteðe baðlý)
                    yeniNesne.name = $"{FillablePrefab.name} (Sýra {sutun + 1}, {satir + 1})";

                    // Bir sonraki prefabý seçin
                    prefabIndeksi = (prefabIndeksi + 1) % unitCount;

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

                    // Bir sonraki prefabý seçin
                    prefabIndeksi = (prefabIndeksi + 1) % unitCount;

                }
            }
        }
    }
}

