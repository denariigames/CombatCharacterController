/**
 * GameInstance_Combat
 * Author: Denarii Games
 * Version: 1.0
 */

using UnityEngine;

namespace MultiplayerARPG
{

	public partial class GameInstance
	{
		[Header("Combat")]
		public bool enableRigidBodyCombatAttack = true;
		public UICombatTextString uiCombatTextString;
	}
}
