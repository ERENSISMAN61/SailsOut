using TMPro;
using UnityEngine;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.05f; // Her karakter arasındaki gecikme
    public float waitBeforeNextText = 1f; // Metinler arasında bekleme süresi
    private TextMeshProUGUI textMeshPro;
    private string currentText = "";

    private string[] konusmalar;

    void Start()
    {
        // Metinleri dizide saklayalım
        konusmalar = new string[]
        {
            "Welcome, brave sailor! We are delighted to welcome you as a hero who has stepped into the depths of the seas.",
            "I have a big secret to tell you, and it has to do with an ancient legend that echoes in every corner of the seas.I can't wait to share it with you because this legend will merge with your adventure.",
            "Once upon a time, there was the most famous captain of the seas. His name echoed around the world for his fearlessness and cunning.However, this great captain lost his life in a treacherous ambush in battle.",
            "Yes, as the son of this famous captain, you and I will embark on a great journey.You will take bold steps to avenge his death and honour your father's name. These seas will be shaped by your determination and courage.",
            "Choose your country and begin this great adventure.You must be ready for the great battles, difficulties and surprises that will take place with you in the depths of the seas.Your adventure begins, put on your courage and set off for the sovereignty of the seas!"

        };

        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeTextCoroutine());
    }

    IEnumerator TypeTextCoroutine()
    {
        foreach (string konusma in konusmalar)
        {
            currentText = ""; // Metni sıfırla
            textMeshPro.text = currentText; // Ekranı temizle

            // Her bir metni yazdır
            foreach (char letter in konusma.ToCharArray())
            {
                currentText += letter;
                textMeshPro.text = currentText;
                yield return new WaitForSeconds(delay);
            }

            // Metinler arasında bekleme süresi
            yield return new WaitForSeconds(waitBeforeNextText);
        }
    }
}
