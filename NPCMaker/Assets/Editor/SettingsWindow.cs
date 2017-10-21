using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SettingsWindow : EditorWindow {


	[MenuItem("Custom/Settings")]
	public static void OpenWindow()
	{
		SettingsWindow window = (SettingsWindow)GetWindow(typeof(SettingsWindow));
		window.Show();
	}

}
