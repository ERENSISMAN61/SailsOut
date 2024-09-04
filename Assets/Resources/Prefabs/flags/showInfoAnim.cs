using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class showInfoAnim : MonoBehaviour
{
    private Animator animator;
    public GameObject showCountryInfoPanel;
    public GameObject saydamPanel;
    private int isClicked = 0;
    

    void Start()
    {
        showCountryInfoPanel.SetActive(false);
        // Animator componentini al
        animator = GetComponent<Animator>();

        // Başlangıçta animasyonu durdur
        animator.SetBool("isClicked", false);
        animator.SetBool("isClicked2", false);
    }

    // Bu method butona bağlanacak
    public void ToggleAnimation()
    {
        
        // Animasyonun durumunu tersine çevir
        animator.SetBool("isClicked", true);
        animator.SetBool("isClicked2", false);
        Invoke("showInfoActive", 2.5f);


    }

    public void ToggleAnimationOut()
    {
        // Mevcut durumu al
        bool isCurrentlyPlaying = animator.GetBool("isClicked2");
        showInfoPasive();
        // Animasyonun durumunu tersine çevir
        animator.SetBool("isClicked", false);
        animator.SetBool("isClicked2", true);
       
    }

    void OnButtonClick()
    {
        Debug.Log("Button tıklandı!");
    }

    private void showInfoActive()
    {
        showCountryInfoPanel.SetActive(true);
    }

    private void showInfoPasive()
    {
        showCountryInfoPanel.SetActive(false);
    }
}
