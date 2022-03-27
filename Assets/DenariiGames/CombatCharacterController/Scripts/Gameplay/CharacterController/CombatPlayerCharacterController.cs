/**
 * CombatPlayerCharacterController
 * Author: Denarii Games
 * Version: 1.0
 *
 * Replace ShooterPlayerCharacterController on PlayerCharacterController prefab
 */

using UnityEngine;

namespace MultiplayerARPG
{
	public partial class CombatPlayerCharacterController : ShooterPlayerCharacterController
	{
		// PROPERTIES: ----------------------------------------------------------------------------

		[Header("Combat")]

		[SerializeField]
		protected RectTransform directionRect;
		[SerializeField]
		protected string xRotationAxisName = "Mouse Y";
		[SerializeField]
		protected string yRotationAxisName = "Mouse X";

		bool combat_primaryAttack = false;
		bool combat_isBlocking = false;
		CombatAnim combatAnim = CombatAnim.Down;

		// INITIALIZERS: --------------------------------------------------------------------------

		protected override void Setup(BasePlayerCharacterEntity characterEntity)
		{
			base.Setup(characterEntity);

			//reset attack direction indicator
			if (directionRect != null)
			{
				directionRect.gameObject.SetActive(false);
				for (int i = 0; i < directionRect.transform.childCount; i++)
				{
					directionRect.transform.GetChild(i).gameObject.SetActive(false);
				}
			}
		}

		protected override void Update()
		{
			base.Update();

			//determine if enter/exit block/crawl state
			if (PlayerCharacterEntity.MovementState.Has(MovementState.IsGrounded))
			{
				if (combat_isBlocking)
				{
					if (!InputManager.GetButton("Crawl"))
					{
						Debug.Log("block ended");
						combat_isBlocking = false;
					}
				}
				else
				{
					if (InputManager.GetButton("Crawl"))
					{
						Debug.Log("block started");
						combat_isBlocking = true;
					}
				}
			}
		}

		// PUBLIC METHODS: ------------------------------------------------------------------------

		public override void Attack(bool isLeftHand)
		{
			if (pauseFireInputFrames > 0) return;

			//set attack direction from combat ui
			PlayerCharacterEntity.CombatAnim = (int)combatAnim;

			if (PlayerCharacterEntity.Attack(isLeftHand))
				updateAttackingCrosshair = true;
		}

		//fire2 repurposed for block
		public override bool GetSecondaryAttackButton()
		{
			return false;
		}

		public virtual bool GetSecondaryAttackButtonUp()
		{
			return false;
		}

		public virtual bool GetSecondaryAttackButtonDown()
		{
			return false;
		}

		// PRIVATE METHODS: -----------------------------------------------------------------------

		protected override void UpdateTarget_BattleMode()
		{
			base.UpdateTarget_BattleMode();

			//update combat ui
			if (rightHandWeapon.FireType != FireType.FireOnRelease) return;

			if (GetPrimaryAttackButtonDown())
			{
				if (!combat_primaryAttack)
					UpdateCombatUI(true, Vector2.zero);
			}

			if (GetPrimaryAttackButtonUp())
				UpdateCombatUI(false, Vector2.zero);

			if (combat_primaryAttack)
			{
				Vector2 direction = new Vector2(InputManager.GetAxis(yRotationAxisName, false), InputManager.GetAxis(xRotationAxisName, false));
				if (direction != Vector2.zero) UpdateCombatUI(true, direction);
			}
		}

		protected void UpdateCombatUI(bool isAttack, Vector2 direction)
		{
			if (directionRect == null) return;

			if (isAttack)
			{
				//init attack
				if (!combat_primaryAttack)
				{
					combat_primaryAttack = true;
					directionRect.gameObject.SetActive(true);
				}
				else
				{
					//clear previous direction indicator
					directionRect.transform.GetChild((int)combatAnim).gameObject.SetActive(false);

					//set attack direction
					if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
					{
						if (direction.x > 0)
							combatAnim = CombatAnim.Right;
						else
							combatAnim = CombatAnim.Left;
					}
					else
					{
						if (direction.y > 0)
							combatAnim = CombatAnim.Up;
						else
							combatAnim = CombatAnim.Down;
					}
				}

				//update direction indicator
				directionRect.transform.GetChild((int)combatAnim).gameObject.SetActive(true);
			}
			else
			{
				combat_primaryAttack = false;
				directionRect.gameObject.SetActive(false);
			}
		}
	}
}
