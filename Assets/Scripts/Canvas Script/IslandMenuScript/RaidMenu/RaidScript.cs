using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidScript : MonoBehaviour
{
    [SerializeField] private float increaseSpeed = 0.05f; // Slider'ın artış hızı (değer/saniye)

    void Update()
    {

        gameObject.GetComponent<Slider>().value += increaseSpeed * Time.unscaledDeltaTime;

    }
}
