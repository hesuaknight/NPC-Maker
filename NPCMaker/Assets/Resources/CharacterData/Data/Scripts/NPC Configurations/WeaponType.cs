using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// Class for storing configuration of the types of weapons available.
/// </summary>
public sealed class WeaponType : Setup {

    [MenuItem("Custom/Setup/Weapon Setup")]
    public static void CreateWeaponType() {
        WeaponType asset = ScriptableObject.CreateInstance<WeaponType>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/CharacterData/Data/NPCSetup/" + typeof(WeaponType).ToString() + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeObject = asset;
    }
}
