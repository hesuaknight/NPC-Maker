using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public sealed class StrategyType : Setup {

    [MenuItem("Custom/Setup/Strategy Setup")]
    public static void CreateWeaponType() {
        StrategyType asset = ScriptableObject.CreateInstance<StrategyType>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/CharacterData/Data/NPCSetup/" + typeof(StrategyType).ToString() + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeObject = asset;
    }
}
