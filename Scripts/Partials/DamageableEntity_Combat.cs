/**
 * DamageableEntity_Combat
 * Author: Denarii Games
 * Version: 1.1
 *
 * @todo update HitBoxes on equip shield
 */

using UnityEngine;
using LiteNetLibManager;
using LiteNetLib;

namespace MultiplayerARPG
{
	public partial class DamageableEntity
	{
		public void CallAllAppendCombatTextString(string combatText)
		{
			RPC(AllAppendCombatTextString, 0, DeliveryMethod.Unreliable, combatText);
		}

		/// <summary>
		/// This will be called on clients to display generic combat texts
		/// </summary>
		/// <param name="text"></param>
		[AllRpc]
		protected void AllAppendCombatTextString(string text)
		{
			if (!IsClient || BaseUISceneGameplay.Singleton.combatTextTransform == null || GameInstance.Singleton.uiCombatTextString == null) return;

			UICombatTextString combatText = Instantiate(GameInstance.Singleton.uiCombatTextString, BaseUISceneGameplay.Singleton.combatTextTransform);
			combatText.transform.localScale = Vector3.one;
			combatText.gameObject.GetOrAddComponent<UIFollowWorldObject>().TargetObject = this.CombatTextTransform;
			combatText.Text = text;
			combatText.gameObject.SetActive(true);
		}
	}
}
