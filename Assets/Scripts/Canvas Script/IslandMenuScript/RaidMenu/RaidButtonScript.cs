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

        raidInsObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 55); //ekranda ortalamak icin

        GameObject.FindGameObjectWithTag("CloseButton").transform.GetChild(0).GetComponent<CloseIslandMenu>().DestroyIslandMenu();

        GameObject.FindGameObjectWithTag("Player").GetComponent<SmoothPlayerMovement>().isIslandMenuOpened = false;// gemimiz hareket edemesin raid varken
    }


}
