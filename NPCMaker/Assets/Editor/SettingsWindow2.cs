using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SettingsWindow2 : EditorWindow {

    Texture2D bodyTexture;
    Color bodyColor;
    Rect bodyPosition;

    GUISkin _skin;

	[MenuItem("Custom/Settings")]
	public static void OpenWindow()
	{
		SettingsWindow2 window = (SettingsWindow2)GetWindow(typeof(SettingsWindow2));
		window.Show();
	}

    private void OnEnable() {
        bodyColor = new Color(0.1f, 0.47f, 0.48f);
        bodyTexture = new Texture2D(1, 1);
        bodyTexture.SetPixel(1, 1, bodyColor);
        bodyTexture.Apply();

        _skin = Resources.Load<GUISkin>("GUIStyles/NPCMakerSkin");
    }

    private void OnGUI()
	{
        DrawLayouts();
        DrawSettings((NPCData)NPCMakerWindow.npcInfo);
    }

    private void DrawLayouts() {
        bodyPosition = new Rect(0, 0, position.width, position.height);
        GUI.DrawTexture(bodyPosition, bodyTexture);
    }

    public  void DrawSettings(NPCData charData)
	{
        minSize = maxSize = new Vector2(515, (135 + (26 * (charData.npcClassType.intVariables.Count + charData.npcClassType.floatVariables.Count))));

        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Name", "Enter the Name of the character"), _skin.GetStyle("Body"), GUILayout.Width(200));
        charData.characterName = EditorGUILayout.TextField(charData.characterName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Prefab", "Enter the prefan for the character"), _skin.GetStyle("Body"), GUILayout.Width(200));
		charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
		EditorGUILayout.EndHorizontal();

        List<string> propertyNames = new List<string>();
        foreach (var item in charData.npcClassType.intVariables) 
            propertyNames.Add(item.Key);


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Propertys", _skin.GetStyle("Title"));
        EditorGUILayout.Space();

        for (int i= 0; i < propertyNames.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(propertyNames[i], _skin.GetStyle("Body"), GUILayout.Width(200));
            charData.npcClassType.intVariables[propertyNames[i]] = EditorGUILayout.IntField(charData.npcClassType.intVariables[propertyNames[i]]);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        propertyNames.Clear();

        foreach (var item in charData.npcClassType.floatVariables)
            propertyNames.Add(item.Key);

        for (int i = 0; i < propertyNames.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(propertyNames[i], _skin.GetStyle("Body"), GUILayout.Width(200));
            charData.npcClassType.floatVariables[propertyNames[i]] = EditorGUILayout.FloatField(charData.npcClassType.floatVariables[propertyNames[i]]);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();

        if (charData.characterName == null || charData.characterName.Length < 1)
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
