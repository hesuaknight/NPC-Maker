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
		DrawSettings((CharacterData)NPCMakerWindow.npcInfo);
	}

	public void DrawSettings(CharacterData charData)
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Name", "Enter the Name of the character"));
		charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Max Health", "Enter a value for the Max Health for the character"));
		charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Max Energy", "Enter a value for the Max Energy for the character"));
		charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("% Crit Chance", "Select the % of crit chance"));
		charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, 100);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Name", "Enter the Name of the character"));
		charData.name = EditorGUILayout.TextField(charData.name);
		EditorGUILayout.EndHorizontal();

		if(charData.name == null || charData.name.Length < 1)
		{
			EditorGUILayout.HelpBox("Name & Prefab field cannot be empty", MessageType.Warning);
		}else if (GUILayout.Button("Save and Exit"))
		{
			SaveData();
			this.Close();
		}
	}

	void SaveData()
	{
		string	prefabPath;
		var newprefabPath = "Assets/Prefabs/Character/NPC/" + NPCMakerWindow.npcInfo.name + ".prefab";
		var dataPath = "Assets/Resources/CharacterData/Data/" + NPCMakerWindow.npcInfo.name + ".asset";

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
