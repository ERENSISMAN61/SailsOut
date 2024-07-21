using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro için gerekli namespace

public class EnemyUnitsCountTextGenerate : MonoBehaviour
{
    public GameObject enemyPrefab; // Editor'den atanacak prefab
    private SphereOfView sphereOfView; // Editor'den atanacak SphereOfView scripti

    private Dictionary<Collider, GameObject> instantiatedEnemies = new Dictionary<Collider, GameObject>();

    void Start()
    {
        sphereOfView = GameObject.FindGameObjectWithTag("Player").GetComponent<SphereOfView>();
    }

    void Update()
    {
        HashSet<Collider> currentEnemies = sphereOfView.GetEnemies();

        foreach (Collider enemy in currentEnemies)
        {
            if (!instantiatedEnemies.ContainsKey(enemy))
            {
                GameObject instantiatedEnemy = Instantiate(enemyPrefab, enemy.transform.position, Quaternion.identity, transform);

                RectTransform rectTransform = instantiatedEnemy.GetComponent<RectTransform>();
                rectTransform.anchoredPosition3D = Vector3.zero; // pozisyonları sıfırla

                // Instantiate edilen objenin child'ında TextMeshProUGUI componentini bul
                TextMeshProUGUI textComponent = instantiatedEnemy.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    // enemy collider'ının parent'ından EnemyUnits componentini al
                    EnemyUnits enemyUnits = enemy.GetComponentInParent<EnemyUnits>();
                    if (enemyUnits != null)
                    {
                        // GetEnemyUnitCount metodunu çağır ve dönen değeri al
                        int enemyCount = enemyUnits.GetEnemyUnitCount();
                        // Alınan değeri TextMeshProUGUI componentinin textine yaz
                        textComponent.text = enemyCount.ToString();
                    }
                }
                //yazının gemiyi takip etmesi için
                if (instantiatedEnemy.TryGetComponent<FloatingUnitCountText>(out FloatingUnitCountText floatingUCountText))
                {
                    floatingUCountText.SetLookAt(enemy.transform.parent.transform);
                }

                instantiatedEnemies.Add(enemy, instantiatedEnemy);
            }
        }

        List<Collider> enemiesToRemove = new List<Collider>();
        foreach (var enemy in instantiatedEnemies.Keys)
        {
            if (!currentEnemies.Contains(enemy))
            {
                Destroy(instantiatedEnemies[enemy]);
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (var enemy in enemiesToRemove)
        {
            instantiatedEnemies.Remove(enemy);
        }
    }
}