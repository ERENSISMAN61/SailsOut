using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerTarget : MonoBehaviour
{
    private float radius = 500f;
    private float distance = 80f; // gorus alaninin objenin ne kadar onunde olacagi
    private LayerMask playerLayer;

    private HashSet<Collider> objectsInSphere = new HashSet<Collider>();

    public bool targetCoolDown = false;

    private GameObject playerPiratesDialog, playerCountryNPCDialog, spawnPlayerDialog;
    private bool TekKullan = false;
    private Collider EnemyCollider;

    public Camera mainCamera; // Kameranı buraya referans al
    private Collider selectedCollider;  // Collider'a göre seçimi takip edeceğiz
    private Vector3 mouseDownPosition;
    private bool isDragging;

    private bool atWar = false;

    void Start()
    {
        playerLayer = LayerMask.GetMask("OutlineFalse", "OutlineTrue");
        playerPiratesDialog = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/PlayerPiratesDialog");
        playerCountryNPCDialog = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/PlayerCountryNPCDialog");
    }

    void Update()
    {
        OneClickControl();
        TargetCalculate();


        Debug.Log("Selected Collider: " + selectedCollider);

    }

    private void OneClickControl()
    {
        // Sol tık başladığında fare pozisyonunu kaydet
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            isDragging = false;
        }

        // Sol tık sırasında fare hareket ederse, sürüklemeyi kontrol et
        if (Input.GetMouseButton(0))
        {
            if (Vector3.Distance(mouseDownPosition, Input.mousePosition) > 5f)
            {
                isDragging = true; // Eğer belli bir mesafeyi aştıysa, sürükleme olarak algıla
            }
        }

        // Sol tık bırakıldığında tıklama veya sürüklemeyi kontrol et
        if (Input.GetMouseButtonUp(0))
        {
            if (!isDragging)
            {
                // Eğer sürükleme yapılmadıysa, tıklamayı işle
                HandleClick();
            }
        }
    }

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Eğer bir Collider'a tıklanmışsa, o Collider'ı seç
        if (Physics.Raycast(ray, out hit))
        {
            SelectCollider(hit.collider);  // Collider'ı seçiyoruz
        }
        else
        {
            // Boş bir yere tıklanmışsa seçimi kaldır
            DeselectCollider();
        }
    }


    private void TargetCalculate()
    {
        string shipCountryNum = GameObject.FindGameObjectWithTag("DiplomacyManager").transform.GetChild(0).GetComponent<GetYourCountry>().GetCountryNum().ToString();
        // Görüş alanındaki tüm player nesnelerini bir diziye ata
        Collider[] enemies = Physics.OverlapSphere(transform.position + transform.forward * distance, radius, playerLayer);

        HashSet<Collider> newObjectsInSphere = new HashSet<Collider>();

        foreach (Collider enemy in enemies)
        {
            if ((enemy.CompareTag("EnemyParts") || enemy.CompareTag("NPCParts")) && enemy != transform.GetComponentInChildren<Collider>()) // Kendini görmesin
            {
                if (!targetCoolDown)
                {
                    newObjectsInSphere.Add(enemy);

                    if (!objectsInSphere.Contains(enemy) && selectedCollider == enemy) // Collider bazlı karşılaştırma
                    {
                        Debug.Log("Targeted Collider: " + enemy.name);
                        if (GetComponent<SmoothPlayerMovement>() != null)
                        {
                            if (Vector3.Distance(enemy.transform.parent.position, transform.position) <= 115f)
                            {
                                if (spawnPlayerDialog == null)
                                {
                                    EnemyCollider = enemy;

                                    if (enemy.CompareTag("EnemyParts"))
                                    {
                                        SpawnDialogPirates();
                                    }
                                    else if (enemy.CompareTag("NPCParts"))
                                    {
                                        atWar = GameObject.FindGameObjectWithTag("DiplomacyManager").GetComponent<DiplomacyManager>().AreAtWar(shipCountryNum, enemy.transform.parent.parent.tag);
                                        SpawnDialogCountryNPC();
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void SpawnDialogPirates()
    {
        spawnPlayerDialog = Instantiate(playerPiratesDialog, new Vector3(+960, +540, 0), Quaternion.identity, GameObject.Find("Canvas").transform);

        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(0);

        GameObject AttackButtonObject = GameObject.Find("AttackButton");

        AttackButtonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => AttackButton());

    }

    private void SpawnDialogCountryNPC()
    {

        spawnPlayerDialog = Instantiate(playerCountryNPCDialog, new Vector3(+960, +540, 0), Quaternion.identity, GameObject.Find("Canvas").transform);

        GameObject AttackButtonObject = GameObject.Find("AttackButtonBg");
        GameObject NothingButtonObject = GameObject.Find("NothingButtonBg");

        AttackButtonObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => AttackButton());
        NothingButtonObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => NothingButton());

        if (atWar)
        {
            spawnPlayerDialog.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "I'll fight with you for my country";

            NothingButtonObject.SetActive(false);



        }
        else
        {
            spawnPlayerDialog.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "What do you want?";

            AttackButtonObject.SetActive(false);

        }
        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(0);


    }

    private void AttackButton()
    {
        if (!TekKullan)
        {
            if (EnemyCollider.transform.parent.GetComponent<EnemyUnits>() != null)
            {


                GameObject.FindGameObjectWithTag("Destroyless").GetComponent<EnemyDestroylessManager>()._EnemyToFightUnitsContainers
                    = EnemyCollider.transform.parent.GetComponent<EnemyUnits>().GetEnemyUnits();
            }
            else if (EnemyCollider.transform.parent.GetComponent<NPCUnits>() != null)
            {
                GameObject.FindGameObjectWithTag("Destroyless").GetComponent<EnemyDestroylessManager>()._EnemyToFightUnitsContainers
                    = EnemyCollider.transform.parent.GetComponent<NPCUnits>().GetNPCUnits();
            }

            GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(1);
            Time.timeScale = 1;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("BattleScene");
            TekKullan = true;
        }
    }

    private void NothingButton()
    {
        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(1);
        Time.timeScale = 1;
        Destroy(spawnPlayerDialog);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void SelectCollider(Collider collider)
    {
        if (selectedCollider != null)
        {
            // Önceden seçili olan Collider'ı iptal edelim
            DeselectCollider();
        }

        // Yeni Collider'ı seç
        selectedCollider = collider;
        // Burada Collider'a seçili olma efektleri ekleyebilirsin
        Debug.Log("Seçilen Collider: " + selectedCollider.name);
    }

    void DeselectCollider()
    {
        if (selectedCollider != null)
        {
            // Seçili Collider'ı iptal et
            Debug.Log("Seçimi kaldırılan Collider: " + selectedCollider.name);
            selectedCollider = null;
            // Seçili olmayı kaldıran işlemleri buraya ekleyebilirsin
        }
    }
}