using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private GameObject upgradeMenu;
    private GameObject InsUpgradeMenu;

    public void OpenUpgradeMenu()
    {
        upgradeMenu = Resources.Load<GameObject>("Prefabs/Canvas Prefabs/Menu Prefab/UpgradeMenu");

        InsUpgradeMenu = Instantiate(upgradeMenu, GameObject.Find("Canvas").transform);
    }
}
