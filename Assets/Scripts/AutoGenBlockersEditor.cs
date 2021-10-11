using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AutoGenBlockersScript))]
public class AutoGenBlockersEditor : Editor
{
    //https://learn.unity.com/tutorial/editor-scripting
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoGenBlockersScript myScript = (AutoGenBlockersScript)target;
        if (GUILayout.Button("AutoGenBlockers"))
        {
            myScript.AutoGenBlockers();
        }
    }
}