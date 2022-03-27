/**
 * CombatDamageableHitBox
 * Author: Denarii Games
 * Version: 1.0
 *
 * Use instead of DamageableHitBox on Ragdoll character objects.
 */

using UnityEngine;
using System.Collections.Generic;

namespace MultiplayerARPG
{
	public class CombatDamageableHitBox : DamageableHitBox
	{
		public string combatText;

		public override void ReceiveDamage(Vector3 fromPosition, EntityInfo instigator, Dictionary<DamageElement, MinMaxFloat> damageAmounts, CharacterItem weapon, BaseSkill skill, short skillLevel, int randomSeed)
		{
			base.ReceiveDamage(fromPosition, instigator, damageAmounts, weapon, skill, skillLevel, randomSeed);

			//testing
			if (combatText.Length == 0) combatText = gameObject.name;

			if (GameInstance.Singleton.uiCombatTextString == null || combatText.Length == 0) return;
			DamageableEntity.CallAllAppendCombatTextString(combatText);
		}
	}
}
