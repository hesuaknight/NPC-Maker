using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public sealed class DamageType : Setup {

    [MenuItem("Custom/Setup/Damage Setup")]
    public static void CreateWeaponType() {
        DamageType asset = ScriptableObject.CreateInstance<DamageType>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/CharacterData/Data/NPCSetup/" + typeof(DamageType).ToString() + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeObject = asset;
    }
}
