using UnityEngine;
using UnityEngine.UI;

public class GridLGRowCountFinder : MonoBehaviour  //Grid Layout Group bile�eninin Constraint Count Column yani ka� s�tunlu grid olu�turacaksak ona g�re bir men� olu�turmak i�in


//-------------!!!!  6 s�tunlu yapmak istiyosak buraya 6 prefab koymam�z ve Constrainrt Count'u da 6 yapmam�z gerekiyor.   /�u anl�k 5.  !!!!!!----------


{
    public GameObject[] prefablar; // Prefablar� buraya s�r�kleyin (a_prefab, b_prefab, c_prefab, vb.)
    public GameObject emptyPrefab; // Bo� prefab� buraya s�r�kleyin

    void Start()
    {
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        int toplamSutun = gridLayoutGroup.constraintCount;

        // Her s�tun i�in rastgele �r�n say�lar� olu�turun
        int[] urunSayilari = new int[toplamSutun];
        for (int i = 0; i < toplamSutun; i++)
        {
            urunSayilari[i] = Random.Range(1, 4); // S�tun ba��na 1 ila 3 aras�nda rastgele �r�n say�s�
        }

        int prefabIndeksi = 0; // a_prefab ile ba�lay�n

        for (int satir = 0; satir < 3; satir++) // 3 sat�r
        {
            for (int sutun = 0; sutun < toplamSutun; sutun++)
            {
                int urunSayisi = urunSayilari[sutun];

                if (urunSayisi > 0)
                {
                    // Prefab� olu�turun
                    GameObject yeniNesne = Instantiate(prefablar[prefabIndeksi]);

                    // Prefab�n ad�n� ayarlay�n (iste�e ba�l�)
                    yeniNesne.name = $"{prefablar[prefabIndeksi].name} (S�ra {sutun + 1}, {satir + 1})";

                    // Bir sonraki prefab� se�in
                    prefabIndeksi = (prefabIndeksi + 1) % prefablar.Length;

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
                    bosNesne.name = $"Bo� (S�ra {sutun + 1}, {satir + 1})";

                    // Bo� nesneyi Grid Layout Group'un alt�na ekleyin
                    bosNesne.transform.SetParent(transform);
                }
            }
        }
    }
}

//void Start()
//{
//    // Grid Layout Group bile�enini al�n
//    GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

//    // Constraint Count de�erini bulun
//    int constraintCount = gridLayoutGroup.constraintCount;

//    // Sonucu konsola yazd�r�n
//    Debug.Log($"Constraint Count: {constraintCount}");
//}

