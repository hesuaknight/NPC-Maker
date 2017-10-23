using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DamageType))]
public class DamageTypeCustomEditor : Editor {
    DamageType _target;

    private void OnEnable() {
        _target = (DamageType)target;
    }

    public override void OnInspectorGUI() {
        SetupCustomEditor.DrawCustomInspector(_target, "Damage Type");

        Repaint();
    }
}
