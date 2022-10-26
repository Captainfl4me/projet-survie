using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryUIManager))]
public class InventoryUIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        InventoryUIManager script = (InventoryUIManager) target;
        if (GUILayout.Button("Update UI"))
        {
            script.UpdateUI();
        }
    }
}
