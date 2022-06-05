// Unite 2017 - Game Architecture with Scriptable Objects
// Author: Ryan Hipple
// Date:   10/04/17

using Events;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventData), editorForChildClasses: true)]
public class EditorRaiseEvent : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEventData e = target as GameEventData;
        if (GUILayout.Button("Raise"))
            e.Raise();
    }
}
