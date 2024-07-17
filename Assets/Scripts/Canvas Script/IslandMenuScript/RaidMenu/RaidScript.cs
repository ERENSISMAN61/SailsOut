using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidScript : MonoBehaviour
{
    [SerializeField] private float increaseSpeed = 0.03f; // Slider'ın artış hızı (değer/saniye)

    [SerializeField] private TextMeshProUGUI RaidFinishedText;
    private float faceDilateSpeed = 0.5f;

    void Start()
    {
        GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(2);  //ZAMANI HIZLANDIR
    }
    void Update()
    {

        gameObject.GetComponent<Slider>().value += increaseSpeed * Time.deltaTime;


        //particle effect. Canvas Screen Space - Camera olmalı
        //   [SerializeField] private GameObject fxObject;
        // fxObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -gameObject.GetComponent<Slider>().value * 360));

        if (gameObject.GetComponent<Slider>().value >= 1)
        {
            GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeAndDateScript>().SetTimeSpeed(0); //ZAMANI DURDUR
            //Face'teki Dilate'i arttırarak 0 yapıcaz.
            if (RaidFinishedText.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate) < 0)
            {
                RaidFinishedText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, RaidFinishedText.fontMaterial.GetFloat(ShaderUtilities.ID_FaceDilate) + (faceDilateSpeed * Time.unscaledDeltaTime));
            }
            else
            {
                RaidFinishedText.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0);
            }

        }
    }
}
