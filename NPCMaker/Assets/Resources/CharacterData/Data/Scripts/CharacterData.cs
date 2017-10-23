using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Repleaced for a generic class: CharacterClass")]
public class CharacterData : ScriptableObject {

	public GameObject prefab;
	public float maxHealth;
	public float maxEnergy;
	public float critChance;
	public string name;
}
