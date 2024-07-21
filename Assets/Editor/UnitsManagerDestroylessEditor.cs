using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(UnitsManager))]
public class UnitsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Mevcut Inspector'ı çiz

        UnitsManager manager = (UnitsManager)target; // Düzenlenen UnitsManager objesini al

        if (GUILayout.Button("Refresh Units Info"))
        {
            // Bu butona basıldığında UnitsContainers listesindeki bilgileri log'la
            Debug.Log("Refreshing Units Info...");
            for (int i = 0; i < manager.UnitsContainers.Count; i++)
            {
                UnitsContainer unit = manager.UnitsContainers[i];
                Debug.Log($"Unit {i}: Rank = {unit.rank}, Health = {unit.health}, Attack Power = {unit.attackPower}");
            }
        }

        // Eğer UnitsContainers listesi boş değilse, listeyi ve içindeki değerleri göster
        if (manager.UnitsContainers != null && manager.UnitsContainers.Count > 0)
        {
            EditorGUILayout.LabelField("Units Info", EditorStyles.boldLabel);
            for (int i = 0; i < manager.UnitsContainers.Count; i++)
            {
                EditorGUILayout.LabelField($"Unit {i}:");
                EditorGUILayout.LabelField($"Rank: {manager.UnitsContainers[i].rank}");
                EditorGUILayout.LabelField($"Health: {manager.UnitsContainers[i].health}");
                EditorGUILayout.LabelField($"Attack Power: {manager.UnitsContainers[i].attackPower}");
            }
        }
    }
}