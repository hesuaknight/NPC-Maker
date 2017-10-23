using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName ="New NPC Data", menuName= "CharacterData / NPC Data") ]
public class NPCData : ScriptableObject {
	public CharacterClass npcClassType;

    public string characterName;
    public GameObject prefab;

    public string npcWeaponType = "none";
    public string npcDamageType = "none";
    public string npcStrategyType = "none";

}
