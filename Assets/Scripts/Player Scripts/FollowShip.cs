using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    public Transform parentTransform; // Ana geminin Transform bileşeni

    void Update()
    {
        if (parentTransform != null)
        {
            // Çocuk objenin rotasyonunu geminin rotasyonuna eşitle
            Vector3 newRotation = parentTransform.rotation.eulerAngles;
            newRotation.y = transform.rotation.eulerAngles.y; // Sadece Y ekseni rotasyonunu kopyala
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
