using System.Collections;
using System.Collections.Generic;
using Generics.Dictionaries;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SerializedDictionary))]
public class SerializedDictionaryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (((SerializedDictionary)target).modifyValues)
        {
            if (GUILayout.Button("Save changes"))
            {
                ((SerializedDictionary)target).DeserializeDictionary();
            }

        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Print Dictionary"))
        {
            ((SerializedDictionary)target).PrintDictionary();
        }
    }
}