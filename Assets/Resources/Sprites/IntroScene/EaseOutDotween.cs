using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CanvasAnimator : MonoBehaviour
{
    [SerializeField] private CanvasGroup flagCanvasGroup;
    [SerializeField] private CanvasGroup dialogueCanvasGroup;
    [SerializeField] private CanvasGroup PLAYCanvasGroup;
    [SerializeField] private float duration = 1f; // Animasyon süresi
    [SerializeField] private float waitBeforeTransition = 0.5f; // İki animasyon arasında bekleme süresi

    private void Start()
    {
        // Başlangıçta canvasların durumunu ayarla
        dialogueCanvasGroup.gameObject.SetActive(true);
        flagCanvasGroup.gameObject.SetActive(false);
        PLAYCanvasGroup.gameObject.SetActive(false);
    }

    public void OpenFlagCanvas()
    {
        // Flag canvas'ı kapanırken ve Dialogue canvas'ı açılırken animasyon
        StartCoroutine(AnimateCanvasTransition());
    }

    public void OpenPlayCanvas()
    {
        StartCoroutine(AnimateCanvasTransition2());
    }

    private IEnumerator AnimateCanvasTransition()
    {
        // Flag canvas'ı animasyonla kapat
        PLAYCanvasGroup.DOFade(0f, duration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                PLAYCanvasGroup.gameObject.SetActive(false);
            });

        // Bir süre bekle
        yield return new WaitForSeconds(waitBeforeTransition);

        // Dialogue canvas'ı animasyonla aç
        flagCanvasGroup.gameObject.SetActive(true);
        flagCanvasGroup.DOFade(1f, duration).SetEase(Ease.InExpo);
    }

    private IEnumerator AnimateCanvasTransition2()
    {
        // Flag canvas'ı animasyonla kapat
        dialogueCanvasGroup.DOFade(0f, duration)
            .SetEase(Ease.OutExpo)
            .OnComplete(() =>
            {
                dialogueCanvasGroup.gameObject.SetActive(false);
            });

        // Bir süre bekle
        yield return new WaitForSeconds(waitBeforeTransition);

        // Dialogue canvas'ı animasyonla aç
        PLAYCanvasGroup.gameObject.SetActive(true);
        PLAYCanvasGroup.alpha = 0; // Başlangıçta görünmez yap
        PLAYCanvasGroup.DOFade(1f, duration).SetEase(Ease.InExpo);
    }
}
