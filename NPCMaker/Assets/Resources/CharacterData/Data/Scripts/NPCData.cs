using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName ="New NPC Data", menuName= "CharacterData / NPC Data") ]
public class NPCData : CharacterData {

	public NPCDmgType npcDmgType;
	public NPCWpnType npcWpnType;
	public NPCCLassType npcClassType;
	public NPCStrategyType npcStrategyType;
}
