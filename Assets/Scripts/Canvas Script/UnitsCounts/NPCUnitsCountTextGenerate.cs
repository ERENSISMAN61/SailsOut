using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro için gerekli namespace

public class NPCUnitsCountTextGenerate : MonoBehaviour
{
    public GameObject unitTextPrefab; // Editor'den atanacak text/bar prefab
    private SphereOfView sphereOfView; // Editor'den atanacak SphereOfView scripti .player'a gore bakılması icin

    private Dictionary<Collider, GameObject> instantiatedNPCs = new Dictionary<Collider, GameObject>();

    void Start()
    {
        sphereOfView = GameObject.FindGameObjectWithTag("Player").GetComponent<SphereOfView>();
    }

    void Update()
    {
        HashSet<Collider> currentNPCs = sphereOfView.GetNPCs();

        foreach (Collider NPC in currentNPCs)
        {
            if (!instantiatedNPCs.ContainsKey(NPC))
            {
                GameObject instantiatedNPC = Instantiate(unitTextPrefab, NPC.transform.position, Quaternion.identity, transform);

                RectTransform rectTransform = instantiatedNPC.GetComponent<RectTransform>();
                rectTransform.anchoredPosition3D = Vector3.zero; // pozisyonları sıfırla

                // Instantiate edilen objenin child'ında TextMeshProUGUI componentini bul
                TextMeshProUGUI textComponent = instantiatedNPC.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    // NPC collider'ının parent'ından NPCUnits componentini al
                    NPCUnits NPCUnits = NPC.GetComponentInParent<NPCUnits>();
                    if (NPCUnits != null)
                    {
                        // GetNPCUnitCount metodunu çağır ve dönen değeri al
                        int NPCCount = NPCUnits.GetNPCUnitCount();
                        // Alınan değeri TextMeshProUGUI componentinin textine yaz
                        textComponent.text = NPCCount.ToString();
                    }
                }
                //yazının gemiyi takip etmesi için
                if (instantiatedNPC.TryGetComponent<FloatingUnitCountText>(out FloatingUnitCountText floatingUCountText))
                {
                    floatingUCountText.SetLookAt(NPC.transform.parent.transform);
                }

                int nationColor = int.Parse(NPC.transform.parent.parent.tag); // npc geminin parentindaki ulke numarasi tagini al
                IslandTextPrefabController ıslandTextPrefabController = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<IslandTextPrefabController>();
                textComponent.color = ıslandTextPrefabController.GetNationColor(nationColor - 1); //Canvastaki IslandsNames'te renkler var ordan cektik


                instantiatedNPCs.Add(NPC, instantiatedNPC);
            }
        }

        List<Collider> NPCsToRemove = new List<Collider>();
        foreach (var NPC in instantiatedNPCs.Keys)
        {
            if (!currentNPCs.Contains(NPC))
            {
                Destroy(instantiatedNPCs[NPC]);
                NPCsToRemove.Add(NPC);
            }
        }

        foreach (var NPC in NPCsToRemove)
        {
            instantiatedNPCs.Remove(NPC);
        }
    }
}