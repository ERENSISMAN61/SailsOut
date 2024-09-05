using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpgradeMenu : MonoBehaviour
{
    public void CloseMenu()
    {
        Destroy(transform.parent.parent.gameObject);
    }
}
