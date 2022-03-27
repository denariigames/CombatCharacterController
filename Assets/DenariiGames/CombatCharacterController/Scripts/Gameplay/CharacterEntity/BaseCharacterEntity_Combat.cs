/**
 * BaseCharacterEntity_Combat
 * Author: Denarii Games
 * Version: 1.0
 */

using UnityEngine;

namespace MultiplayerARPG
{
	public partial class BaseCharacterEntity
	{
		protected int combatAnim;
		public int CombatAnim { get { return combatAnim; } set { combatAnim = value; } }
	}
}
