using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterClass))]
public class CharacterClassCustomEditor : Editor {
    CharacterClass _target;

    bool _displayIntVariables;
    bool _displayFloatVariables;

    int _currentTabSelected;

    string _variableName = "variable";
    int _variableType;

    private void OnEnable() {
        _target = (CharacterClass)target;
    }

    public override void OnInspectorGUI() {
        _currentTabSelected = GUILayout.Toolbar(_currentTabSelected, new string[] { "Show", "Add", "Delete" });

        if (_currentTabSelected == 0) {
            DrawVariables();
        } else if (_currentTabSelected == 1) {
            AddVariable();
        } else if (_currentTabSelected == 2) {
            DeleteVariable();
        }

        Repaint();
    }

    private void DrawVariables() {
        string[] StringName = AssetDatabase.GetAssetPath(_target).Split('/');
        _target.name = StringName[StringName.Length - 1].Split('.')[0];

        EditorGUILayout.LabelField(_target.name, EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Variables", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        _displayIntVariables = EditorGUILayout.Foldout(_displayIntVariables, (_displayIntVariables ? "Hide" : "Show") + " int variables");

        if (_displayIntVariables) {
            if (_target.intVariables.Count <= 0)
                EditorGUILayout.LabelField("Empty list", EditorStyles.boldLabel);
            else
                foreach (var item in _target.intVariables)
                    EditorGUILayout.LabelField(item.Key);
        }

        _displayFloatVariables = EditorGUILayout.Foldout(_displayFloatVariables, (_displayFloatVariables ? "Hide" : "Show") + " float variables");

        if (_displayFloatVariables) {
            if (_target.floatVariables.Count <= 0)
                EditorGUILayout.LabelField("Empty list", EditorStyles.boldLabel);
            else
                foreach (var item in _target.floatVariables)
                    EditorGUILayout.LabelField(item.Key);
        }
        EditorGUILayout.EndVertical();
    }

    private void AddVariable() {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        _variableName = EditorGUILayout.TextField("Name:", _variableName);
        _variableType = EditorGUILayout.Popup("Varialbe Type:", _variableType, new string[] { "int", "float" });

        if (_variableName == "variable" || _target.intVariables.ContainsKey(_variableName) || _target.floatVariables.ContainsKey(_variableName))
            EditorGUILayout.HelpBox("Name of variable alredy exist or is incorrenct", MessageType.Error);
        else {
            if (GUILayout.Button("Add")) {
                if (_variableType == 0)
                    _target.intVariables.Add(_variableName, 0);
                else
                    _target.floatVariables.Add(_variableName, 0);

                _variableName = "variable";
            }
        }

        EditorGUILayout.EndVertical();
    }

    private void DeleteVariable() {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        if (_target.intVariables.Count <= 0 && _target.floatVariables.Count <= 0)
            EditorGUILayout.LabelField("Empty List", EditorStyles.boldLabel);

        List<string> intVariables = new List<string>();
        List<string> floatVariables = new List<string>();

        foreach (var item in _target.intVariables)
            intVariables.Add(item.Key);

        foreach (var item in _target.floatVariables)
            floatVariables.Add(item.Key);

        for (int i = 0; i < intVariables.Count; i++) {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(intVariables[i]);
            if (GUILayout.Button((Texture)(Resources.Load("Images/ButtonDelete")), GUILayout.ExpandWidth(false))) 
                _target.intVariables.Remove(intVariables[i]);
            
            GUILayout.EndHorizontal();
        }

        for (int i = 0; i < floatVariables.Count; i++) {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(floatVariables[i]);
            if (GUILayout.Button((Texture)(Resources.Load("Images/ButtonDelete")), GUILayout.ExpandWidth(false))) 
                _target.floatVariables.Remove(floatVariables[i]);

            GUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }
}
