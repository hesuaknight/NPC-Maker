using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StrategyType))]
public class StrategyTypeCustomEditor : Editor {
    StrategyType _target;

    private void OnEnable() {
        _target = (StrategyType)target;
    }

    public override void OnInspectorGUI() {
        SetupCustomEditor.DrawCustomInspector(_target, "Strategy type");

        Repaint();
    }
}
