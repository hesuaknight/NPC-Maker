using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCMakerWindow : EditorWindow {

    Texture2D heatherTexture;
    Texture2D npcTexture;

    Rect heatherSection;
    Rect npcSection;

    Color heatherSectionColor = Color.gray;

    WeaponType _npcWeaponType;
    DamageType _npcDamageType;
    StrategyType _npcStrategyType;
    Dictionary<string, CharacterClass> _allClass;

    static NPCData npcData;

    public static NPCData npcInfo { get { return npcData; } }

    [MenuItem("Custom/NPC Maker")]
    static void OpenWindow() {
        NPCMakerWindow window = (NPCMakerWindow)GetWindow(typeof(NPCMakerWindow));
        window.minSize = new Vector2(300, 300);
        window.Show();
    }

    //ES COMO EL START
    private void OnEnable() {
        heatherTexture = new Texture2D(1, 1);
        heatherTexture.SetPixel(0, 0, heatherSectionColor);
        heatherTexture.Apply();

        npcData = (NPCData)ScriptableObject.CreateInstance(typeof(NPCData));

        _npcWeaponType = (WeaponType)AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/NPCSetup/WeaponType.asset", typeof(WeaponType));
        _npcDamageType = (DamageType)AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/NPCSetup/DamageType.asset", typeof(DamageType));
        _npcStrategyType = (StrategyType)AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/NPCSetup/StrategyType.asset", typeof(StrategyType));
        LoadAllCharacterClass();
    }

    //ES COMO EL UPDATE SE LLAMA UNA VEZ POR INTERACCION
    private void OnGUI() {
        DrawLayouts();
        DrawHeather();
        if (ValidateSetup())
            DrawNPCSection();
    }

    private void DrawLayouts() {
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

    private void DrawHeather() {
        GUILayout.BeginArea(heatherSection);
        GUILayout.Label(new GUIContent("NPC Maker", "Make your custom NPC"));
        GUILayout.EndArea();
    }

    private void DrawNPCSection() {

        GUILayout.BeginArea(npcSection);
        GUILayout.BeginVertical();
        GUILayout.Label(new GUIContent("NPC section"));

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Class", "Select the class"));
        npcData.npcClassType = _allClass[GetAllCharacterClassNames()[EditorGUILayout.Popup(GetAllCharacterClassNames().IndexOf(npcData.npcClassType.name), GetAllCharacterClassNames().ToArray())]];
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Weapon", "Select a Wepon"));
        npcData.npcWeaponType = _npcWeaponType.config[EditorGUILayout.Popup(_npcWeaponType.config.IndexOf(npcData.npcWeaponType), _npcWeaponType.config.ToArray())];
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Damage Type", "Select a damage Type"));
        npcData.npcDamageType = _npcDamageType.config[EditorGUILayout.Popup(_npcDamageType.config.IndexOf(npcData.npcDamageType), _npcDamageType.config.ToArray())];
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Strategy Behabour", "Select a behabour"));
        npcData.npcStrategyType = _npcStrategyType.config[EditorGUILayout.Popup(_npcStrategyType.config.IndexOf(npcData.npcStrategyType), _npcStrategyType.config.ToArray())];
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Generate")) {
            SettingsWindow2.OpenWindow();
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    /// <summary>
    /// Validate if all necessary configurations for the creation of npc exists.
    /// </summary>
    /// <returns></returns>
    private bool ValidateSetup() {

        if (_allClass.Count <= 0) {
            GUILayout.BeginArea(npcSection);
            EditorGUILayout.HelpBox("No data class found", MessageType.Error);
            if (GUILayout.Button("Create new class")) {
                CharacterClass.CreateCharacterClass();
                Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/Character Class/CharacterClass.asset", typeof(CharacterClass));
                this.Close();
            }
            GUILayout.EndArea();
            return false;
        }

        if (_npcWeaponType == null) {
            GUILayout.BeginArea(npcSection);
            EditorGUILayout.HelpBox("No setup for weapons", MessageType.Error);
            if (GUILayout.Button("Create Setup")) {
                WeaponType.CreateWeaponType();
                _npcWeaponType = (WeaponType)AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/NPCSetup/WeaponType.asset", typeof(WeaponType));
            }
            GUILayout.EndArea();
            return false;
        } else if (_npcDamageType == null) {
            GUILayout.BeginArea(npcSection);
            EditorGUILayout.HelpBox("No setup for Damage type", MessageType.Error);
            if (GUILayout.Button("Create Setup")) {
                DamageType.CreateWeaponType();
                _npcDamageType = (DamageType)AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/NPCSetup/DamageType.asset", typeof(DamageType));
            }
            GUILayout.EndArea();
            return false;
        } else if (_npcStrategyType == null) {
            GUILayout.BeginArea(npcSection);
            EditorGUILayout.HelpBox("No setup for Strategy type", MessageType.Error);
            if (GUILayout.Button("Create Setup")) {
                StrategyType.CreateWeaponType();
                _npcStrategyType = (StrategyType)AssetDatabase.LoadAssetAtPath("Assets/Resources/CharacterData/Data/NPCSetup/StrategyType.asset", typeof(StrategyType));
            }
            GUILayout.EndArea();
            return false;
        }

        return true;
    }

    private void LoadAllCharacterClass() {
        _allClass = new Dictionary<string, CharacterClass>();
        string[] allPaths = AssetDatabase.FindAssets("t:CharacterClass");
        CharacterClass data = null;
        for (int i = 0; i < allPaths.Length; i++) {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);
            data = ((CharacterClass)AssetDatabase.LoadAssetAtPath(allPaths[i], typeof(CharacterClass)));
            _allClass.Add(data.name, data);
        }
        npcData.npcClassType = data;
    }

    private List<string> GetAllCharacterClassNames() {
        List<string> classNames = new List<string>();

        foreach (var item in _allClass)
            classNames.Add(item.Key);

        return classNames;
    }

}