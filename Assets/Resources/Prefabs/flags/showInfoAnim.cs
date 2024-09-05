using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class showInfoAnim : MonoBehaviour
{
    private Animator animator;
    public GameObject showCountryInfoPanel;
    public GameObject saydamPanel;
    private Animator countryInfoAnimator;
    private int isClicked = 0;
    void Start()
    {
        // Animator componentlerini al
        animator = GetComponent<Animator>();
        countryInfoAnimator = showCountryInfoPanel.GetComponent<Animator>();
        // Başlangıçta tüm animasyonları durdur
        ResetAnimationStates();
    }

    public void ResetAnimationStates()
    {
        animator.SetBool("isClicked", false);
        animator.SetBool("isClicked2", false);
        countryInfoAnimator.SetBool("isOpen", false);
        countryInfoAnimator.SetBool("isOpen2", false);
    }

    // Bu method butona bağlanacak
    public void ToggleAnimation()
    {
        ResetAnimationStates();
        StartCoroutine(ActivateAnimations());
    }

    public IEnumerator ActivateAnimations()
    {
        // İlk animasyonu başlat
        animator.SetBool("isClicked", true);
        animator.SetBool("isClicked2", false);
        yield return new WaitForSeconds(2f); // 2 saniye bekleyin
        // İkinci animasyonu aktif et
        countryInfoAnimator.SetBool("isOpen", true);
        countryInfoAnimator.SetBool("isOpen2", false);
    }

    public void ToggleAnimationOut()
    {
        ResetAnimationStates();
        StartCoroutine(DeactivateAnimations());
    }

    public IEnumerator DeactivateAnimations()
    {
        countryInfoAnimator.SetBool("isOpen2", true);
        countryInfoAnimator.SetBool("isOpen", false);
        yield return new WaitForSeconds(2f); // 2 saniye bekleyin
        // İkinci animasyonu başlat
        animator.SetBool("isClicked", false);
        animator.SetBool("isClicked2", true);
    }



}
