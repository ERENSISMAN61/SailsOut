using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IslandTextPrefabController : MonoBehaviour
{
    [SerializeField] private GameObject islandTextPrefab;

    [SerializeField] private Color[] NationColors;
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Island"))
        {
            GameObject islandTextP = Instantiate(islandTextPrefab, transform.position, Quaternion.identity);
            // Instantiate nedir? Instantiate fonksiyonu, bir GameObject'in kopyasını oluşturur ve bu kopyayı sahneye ekler.

            islandTextP.name = obj.name.Split("-")[0].Trim() + " Text All";
            islandTextP.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = obj.name.Split("-")[0].Trim(); // Adanın ismini alıp text'e atadık.

            islandTextP.transform.SetParent(transform); // Parent olarak bu scriptin bağlı olduğu objeyi seçtik. Seçmeseydik, bu obje sahne hiyerarşisinde en üstte olacaktı.
            islandTextP.transform.localPosition = new Vector3(0, 0, 0);
            islandTextP.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            islandTextP.GetComponent<FloatingScreenText>().SetLookAt(obj.transform);

            islandTextP.transform.GetChild(0).gameObject.GetComponent<SetDestinationIsland>().IslandMenuObjectScript = islandTextP;  //Island Menu Parent Objesini ekledik

            foreach (Transform dock in obj.transform)
            {
                if (dock.CompareTag("Dock"))
                {
                    islandTextP.transform.GetChild(0).gameObject.GetComponent<SetDestinationIsland>().transformDock.Add(dock.transform);  // iskeleleri ekledik
                }
            }

            // Ülke renklerini textlerdeki 4 cizgiye atadık.
            islandTextP.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = NationColors[int.Parse(obj.transform.parent.parent.name.Split("-")[0].Trim()) - 1];
            islandTextP.transform.GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = NationColors[int.Parse(obj.transform.parent.parent.name.Split("-")[0].Trim()) - 1];
            islandTextP.transform.GetChild(0).GetChild(3).GetComponent<UnityEngine.UI.Image>().color = NationColors[int.Parse(obj.transform.parent.parent.name.Split("-")[0].Trim()) - 1];
            islandTextP.transform.GetChild(0).GetChild(4).GetComponent<UnityEngine.UI.Image>().color = NationColors[int.Parse(obj.transform.parent.parent.name.Split("-")[0].Trim()) - 1];
            // Ülke rengini atadık.

        }

    }

    public Color GetNationColor(int nationColor)
    {
        return NationColors[nationColor];
    }
}
