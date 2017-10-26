using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterClass : ScriptableObject {

    new public string name;

    public Dictionary<string, int> intVariables;
    public Dictionary<string, float> floatVariables;

    public CharacterClass() {
        name = typeof(CharacterClass).ToString();
        intVariables = new Dictionary<string, int>();
        floatVariables = new Dictionary<string, float>();
    }
	
    [MenuItem("Custom/Setup/Character Class Setup")]
    public static void CreateCharacterClass() {
        CharacterClass asset = ScriptableObject.CreateInstance<CharacterClass>();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/CharacterData/Data/Character Class/" + typeof(CharacterClass).ToString() + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Selection.activeObject = asset;
    }

}
