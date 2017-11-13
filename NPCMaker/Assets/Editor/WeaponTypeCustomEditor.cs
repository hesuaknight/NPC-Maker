using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponType))]
public class WeaponTypeCustomEditor : Editor {
    WeaponType _target;

    private void OnEnable() {
        _target = (WeaponType)target;
    }

    public override void OnInspectorGUI() {
        SetupCustomEditor.DrawCustomInspector(_target, "Weapon Type");

        Repaint();
    }
}
