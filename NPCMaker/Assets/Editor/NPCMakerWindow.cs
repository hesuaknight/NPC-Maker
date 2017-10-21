using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCMakerWindow : EditorWindow
{

	Texture2D heatherTexture;
	Texture2D npcTexture;

	Rect heatherSection;
	Rect npcSection;

	Color heatherSectionColor = Color.gray;

	static NPCData npcData;

	public static NPCData npcInfo { get { return npcData; } }

	[MenuItem("Custom/NPC Maker")]
	static void OpenWindow()
	{
		NPCMakerWindow window = (NPCMakerWindow)GetWindow(typeof(NPCMakerWindow));
		window.minSize = new Vector2(300, 300);
		window.Show();
	}

	//ES COMO EL START
	private void OnEnable()
	{
		heatherTexture = new Texture2D(1, 1);
		heatherTexture.SetPixel(0, 0, heatherSectionColor);
		heatherTexture.Apply();

		npcData = (NPCData)ScriptableObject.CreateInstance(typeof(NPCData));
	}

	//ES COMO EL UPDATE SE LLAMA UNA VEZ POR INTERACCION
	private void OnGUI()
	{
		DrawLayouts();
		DrawHeather();
		DrawNPCSection();
	}

	private void DrawLayouts()
	{
		heatherSection.x = 0;
		heatherSection.y = 0;
		heatherSection.width = Screen.width;
		heatherSection.height = 50;

		npcSection.x = 0;
		npcSection.y = 50;
		npcSection.width = Screen.width;
		npcSection.height = Screen.height;

		GUI.DrawTexture(heatherSection, heatherTexture);
	}

	private void DrawHeather()
	{
		GUILayout.BeginArea(heatherSection);
		GUILayout.Label(new GUIContent("NPC Maker", "Make your custom NPC"));
		GUILayout.EndArea();
	}

	private void DrawNPCSection()
	{
		GUILayout.BeginArea(npcSection);
		GUILayout.BeginVertical();
		GUILayout.Label(new GUIContent("NPC section"));

		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Class", "Select the class"));
		npcData.npcClassType = (Types.NPCCLassType)EditorGUILayout.EnumPopup(npcData.npcClassType);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Weapon", "Select a Wepon"));
		npcData.npcWpnType = (Types.NPCWpnType)EditorGUILayout.EnumPopup(npcData.npcWpnType);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Damage Type", "Select a damage Type"));
		npcData.npcDmgType = (Types.NPCDmgType)EditorGUILayout.EnumPopup(npcData.npcDmgType);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(new GUIContent("Strategy Behabour", "Select a behabour"));
		npcData.npcStrategyType = (Types.NPCStrategyType)EditorGUILayout.EnumPopup(npcData.npcStrategyType);
		GUILayout.EndHorizontal();

		if (GUILayout.Button("Generate"))
		{
			SettingsWindow.OpenWindow();
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();

	}


}