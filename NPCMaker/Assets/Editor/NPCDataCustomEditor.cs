using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCData))]
public class NPCDataCustomEditor : Editor {

    public override void OnInspectorGUI() {
        GUI.enabled = false;
        base.OnInspectorGUI();
        GUI.enabled = true;
    }
}
