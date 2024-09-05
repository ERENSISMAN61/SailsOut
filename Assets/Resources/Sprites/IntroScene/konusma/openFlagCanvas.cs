using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openFlagCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject flagCanvas;
    [SerializeField]
    private GameObject dialogueCanvas;

    private void Start()
    {
        dialogueCanvas.SetActive(true);
        flagCanvas.SetActive(false);
    }

    public void OpenFlagCanvas()
    {
        

        flagCanvas.SetActive(false);
       

        dialogueCanvas.SetActive(true);
    }
}
