using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Setup))]
public class SetupCustomEditor : Editor {
    static int _currentTabSelected;
    static string _newType;

    public static void DrawCustomInspector(Setup setup, string title) {
        EditorGUILayout.LabelField(title, EditorStyles.boldLabel);

        _currentTabSelected = GUILayout.Toolbar(_currentTabSelected, new string[] { "Show", "Add", "Remove" });

        if(_currentTabSelected == 0) {
            DrawTypes(setup);
        }else if(_currentTabSelected == 1) {
            AddType(setup);
        }else if(_currentTabSelected == 2) {
            RemoveType(setup);
        }
    }

    private static void DrawTypes(Setup setup) {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        if (setup.config.Count <= 1)
            EditorGUILayout.LabelField("Empty List.", EditorStyles.boldLabel);

        for (int i = 1; i < setup.config.Count; i++)
            EditorGUILayout.LabelField(setup.config[i]);

        EditorGUILayout.EndVertical();
    }

    private static void AddType(Setup setup) {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        _newType = EditorGUILayout.TextField("Weapon type:", _newType);

        if (setup.config.Contains(_newType)) {
            EditorGUILayout.HelpBox("The current weapon type is alredy exists", MessageType.Error);
        } else {
            if (GUILayout.Button("Save")) {
                setup.config.Add(_newType);
                _newType = "";
            }
        }
        EditorGUILayout.EndVertical();
    }

    private static void RemoveType(Setup setup) {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        if (setup.config.Count <= 1) {
            EditorGUILayout.LabelField("Empty list.", EditorStyles.boldLabel);
        }

        for (int i = 1; i < setup.config.Count; i++) {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(setup.config[i]);
            if (GUILayout.Button((Texture)(Resources.Load("Images/ButtonDelete")), GUILayout.ExpandWidth(false)))
                setup.config.RemoveAt(i);
            GUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }
}
