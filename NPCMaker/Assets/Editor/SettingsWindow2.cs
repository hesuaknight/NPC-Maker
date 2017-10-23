using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SettingsWindow2 : EditorWindow {

	[MenuItem("Custom/Settings")]
	public static void OpenWindow()
	{
		SettingsWindow2 window = (SettingsWindow2)GetWindow(typeof(SettingsWindow2));
		window.Show();
	}

    private void OnGUI()
	{
        DrawSettings((NPCData)NPCMakerWindow.npcInfo);
    }
    
	public void DrawSettings(NPCData charData)
	{
        EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Prefab", "Enter the prefan for the character"));
		charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Name", "Enter the Name of the character"));
		charData.characterName = EditorGUILayout.TextField(charData.characterName);
		EditorGUILayout.EndHorizontal();

        List<string> propertyNames = new List<string>();
        foreach (var item in charData.npcClassType.intVariables) 
            propertyNames.Add(item.Key);
        
        for(int i= 0; i < propertyNames.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            charData.npcClassType.intVariables[propertyNames[i]] = EditorGUILayout.IntField(propertyNames[i] + ":", charData.npcClassType.intVariables[propertyNames[i]]);
            EditorGUILayout.EndVertical();
        }

        propertyNames.Clear();

        foreach (var item in charData.npcClassType.floatVariables)
            propertyNames.Add(item.Key);

        for (int i = 0; i < propertyNames.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            charData.npcClassType.floatVariables[propertyNames[i]] = EditorGUILayout.FloatField(propertyNames[i] + ":", charData.npcClassType.floatVariables[propertyNames[i]]);
            EditorGUILayout.EndVertical();
        }

        if(charData.characterName == null || charData.characterName.Length < 1)
		{
			EditorGUILayout.HelpBox("Name cannot be empty", MessageType.Warning);
		}else if(charData.prefab == null) {
            EditorGUILayout.HelpBox("Prefab cannot be empty", MessageType.Warning);
        }else if (GUILayout.Button("Save and Exit"))
		{
			SaveData();
			this.Close();
		}
    }
    
    void SaveData()
	{
		string	prefabPath;
		var newprefabPath = "Assets/Prefabs/Character/NPC/" + NPCMakerWindow.npcInfo.characterName + ".prefab";
		var dataPath = "Assets/Resources/CharacterData/Data/" + NPCMakerWindow.npcInfo.characterName + ".asset";

		AssetDatabase.CreateAsset(NPCMakerWindow.npcInfo, dataPath);
		prefabPath = AssetDatabase.GetAssetPath(NPCMakerWindow.npcInfo.prefab);
		AssetDatabase.CopyAsset(prefabPath, newprefabPath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		GameObject npcPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newprefabPath,typeof(GameObject));
		if (!npcPrefab.GetComponent<NPC>())
			npcPrefab.AddComponent<NPC>();

		npcPrefab.GetComponent<NPC>().npcData = NPCMakerWindow.npcInfo;

	}

}
