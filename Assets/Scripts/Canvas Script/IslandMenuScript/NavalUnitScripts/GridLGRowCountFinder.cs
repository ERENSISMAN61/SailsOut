using UnityEngine;
using UnityEngine.UI;

public class GridLGRowCountFinder : MonoBehaviour  //Unit say�s�na g�re column say�s�n� otomatik belirliyoruz.


//-------------!!!!  6 s�tunlu yapmak istiyosak root  objesindeki RecruimentUnitScript componentine bir tane daha scriptable obje eklememiz yeterli !!!!!!----------


{

    [SerializeField] private GameObject FillablePrefab; // Doldurulabilir prefab� buraya s�r�kleyin
    [SerializeField] private GameObject emptyPrefab; // Bo� prefab� buraya s�r�kleyin

    void Start()
    {

        int unitCount = transform.parent.parent.parent.GetComponent<RecruimentUnitScript>().GetUnitCount();  //Unit say�s�n� almak i�in

        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = unitCount; //Constraint Count'u unit say�s�na e�itlemek i�in  (Yani Unit Say�s�na g�re column say�s�n� otomatik belirliyoruz)
        int toplamSutun = gridLayoutGroup.constraintCount;


        // Her s�tun i�in rastgele �r�n say�lar� olu�turun
        int[] urunSayilari = new int[toplamSutun];
        for (int i = 0; i < toplamSutun; i++)
        {
            urunSayilari[i] = Random.Range(1, 4); // S�tun ba��na 1 ila 3 aras�nda rastgele �r�n say�s�
        }

        int prefabIndeksi = 0; // 1. unit ile baslamak icin

        for (int satir = 0; satir < 3; satir++) // 3 sat�r
        {
            for (int sutun = 0; sutun < toplamSutun; sutun++)
            {
                int urunSayisi = urunSayilari[sutun];

                if (urunSayisi > 0)
                {
                    // Prefab� olu�turun
                    GameObject yeniNesne = Instantiate(FillablePrefab);  // doldurulabilir prefab� olu�turmak i�in

                    //Recruiment (Root) objesindeki �r�nleri scriptableobject olarak almak i�in                                                             
                    yeniNesne.GetComponent<NavalUnitConfig>().SetNavalUnitContainer(transform.parent.parent.parent.GetComponent<RecruimentUnitScript>().GetUnits(prefabIndeksi));





                    // Prefab�n ad�n� ayarlay�n (iste�e ba�l�)
                    yeniNesne.name = $"{FillablePrefab.name} (Sira {sutun + 1}, {satir + 1})";

                    // Bir sonraki prefab� se�in
                    prefabIndeksi = (prefabIndeksi + 1) % unitCount;

                    // Yeni nesneyi Grid Layout Group'un alt�na ekleyin
                    yeniNesne.transform.SetParent(transform);

                    // Bu s�tun i�in �r�n say�s�n� azalt�n
                    urunSayilari[sutun]--;
                }
                else
                {
                    // Bo� bir prefab� olu�turun
                    GameObject bosNesne = Instantiate(emptyPrefab);

                    // Bo� prefab�n ad�n� ayarlay�n (iste�e ba�l�)
                    bosNesne.name = $"Bos (Sira {sutun + 1}, {satir + 1})";

                    // Bo� nesneyi Grid Layout Group'un alt�na ekleyin
                    bosNesne.transform.SetParent(transform);

                    // Bir sonraki prefab� se�in
                    prefabIndeksi = (prefabIndeksi + 1) % unitCount;

                }
            }
        }
    }

    // Ürünü silip yerine boş bir prefab ekleyen metod
    public void RemoveProduct(int productIndex)
    {
        // Ürünün silindiğini varsayalım ve yerine boş bir prefab ekleyelim
        if (transform.childCount > productIndex)
        {
            // Silinecek ürünün GameObject'ini al
            GameObject productToRemove = transform.GetChild(productIndex).gameObject;

            // Ürünü sahneden kaldır
            Destroy(productToRemove);

            // Boş bir prefab oluştur
            GameObject emptyObject = Instantiate(emptyPrefab);
            emptyObject.transform.SetParent(transform, false);
            emptyObject.transform.SetSiblingIndex(productIndex); // Boş prefabı silinen ürünün yerine koy
            emptyObject.name = $"Boş (Yer {productIndex + 1})";
        }
    }
}

