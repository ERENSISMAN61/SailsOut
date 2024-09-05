using UnityEngine;
using UnityEngine.EventSystems;

public class BlockClicks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Tıklamayı 'kullan' yani başka bir yere geçmesini engelle
        eventData.Use();
    }
}
