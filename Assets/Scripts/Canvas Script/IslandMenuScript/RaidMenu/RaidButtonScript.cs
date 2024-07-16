using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject raidObject;


    public void StartRaid()
    {
        GameObject raidInsObj = Instantiate(raidObject, transform.position, Quaternion.identity);

        raidInsObj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

        raidInsObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //ekranda ortalamak icin

        GameObject.FindGameObjectWithTag("CloseButton").transform.GetChild(0).GetComponent<CloseIslandMenu>().DestroyIslandMenu();

    }


}
