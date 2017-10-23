using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Types
{
    [System.Obsolete("Use DamageType Class")]
    public enum NPCDmgType
	{
		FIRE,
		ICE,
		EARTH,
		AIR,
		LIGHT

	}

    [System.Obsolete("Use WeaponType Class")]
    public enum NPCWpnType
	{
		STAFF,
		SWORD_AND_SHIELD,
		TWO_HANDED_SWORD,
		SWORD_AND_DAGGER,
		AXE,
		BOW_AND_ARROW,
		CROSSBOW
	}

	public enum NPCCLassType
	{
		MAGE,
		WARRIOR,
		ROGUE

	}

    [System.Obsolete("Use StrategyType Class")]
    public enum NPCStrategyType
	{
		STEALTH,
		AGRESSIVE,
		PROTECT,
		UTILITY

	}
}
