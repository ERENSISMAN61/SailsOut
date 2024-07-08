using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(DestroylessManager))]
public class UnitsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Mevcut Inspector'ı çiz

        DestroylessManager manager = (DestroylessManager)target; // Düzenlenen UnitsManager objesini al

        if (GUILayout.Button("Refresh Units Info"))
        {
            // Bu butona basıldığında UnitsContainers listesindeki bilgileri log'la
            Debug.Log("Refreshing Units Info...");
            for (int i = 0; i < manager._UnitsContainers.Count; i++)
            {
                UnitsContainer unit = manager._UnitsContainers[i];
                Debug.Log($"Unit {i}: Rank = {unit.rank}, Health = {unit.health}, Attack Power = {unit.attackPower}");
            }
        }

        // Eğer UnitsContainers listesi boş değilse, listeyi ve içindeki değerleri göster
        if (manager._UnitsContainers != null && manager._UnitsContainers.Count > 0)
        {
            EditorGUILayout.LabelField("Units Info", EditorStyles.boldLabel);
            for (int i = 0; i < manager._UnitsContainers.Count; i++)
            {
                EditorGUILayout.LabelField($"Unit {i}:");
                EditorGUILayout.LabelField($"Rank: {manager._UnitsContainers[i].rank}");
                EditorGUILayout.LabelField($"Health: {manager._UnitsContainers[i].health}");
                EditorGUILayout.LabelField($"Attack Power: {manager._UnitsContainers[i].attackPower}");
            }
        }
    }
}