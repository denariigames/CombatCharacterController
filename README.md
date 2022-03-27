# Combat Character Controller

![combatcharactercontroller](https://user-images.githubusercontent.com/755461/160267622-6a8506d1-7b3f-4e76-9dfc-5542046dcf4f.png)

Enables directional attack animations (high, low, left, right) with an onscreen directional indicator controlled by mouse movement when the left mouse button is depressed. Release button to perform the attack. Directional damage is supported with out-of-the-box DamageableHitBox.

The overall design goal of this addon was to be minimally invasive to the Kit, repurposing the random attack animations with the player selected attack direction.

The character controller is based on the MMORPG Kit ShooterPlayerController as was tested with Kit release 1.72c5.


### Demo

![combatcharactercontroller_build](https://user-images.githubusercontent.com/755461/160267757-3a43db9a-f9cc-472b-8641-41b961f90208.png)

A demo is provided to showcase Combat (MMO only). You will need to first download the free asset [Melee Axe Pack](https://assetstore.unity.com/packages/3d/animations/melee-axe-pack-35320) to get the additional directional attack animations needed.

1. ensure the following OneHandSword animations are referenced in Right Hand Attack Animations on the **CombatFemale** and/or **CombatFemale_CC prefabs**:
- Element 0: standing_melee_attack_downward
- Element 1: standing_melee_attack_backhand
- Element 2: standing_melee_attack_360_high
- Element 3: standing_melee_attack_horiztonal

2. add Combat-00Init_MMO and Combat-Map001 scenes to top of **Build settings**
3. build and launch server
4. run **Combat-00Init_MMO_ClientOnly** and create character with CombatFemale race


### Setup

For existing builds, you need to make the following changes to enable Combat.

_Controller_

Replace reference to PlayerCharacterController in GameInstance component found in **Init scenes**. You can alternatively set the Contoller prefab directly in the Player Character Entity in the player prefab.

![combatcharactercontroller_instance](https://user-images.githubusercontent.com/755461/160268020-5e864117-29e5-4132-83a6-e168cc515da8.png)

_Player prefab_

1. replace DefaultCharacterAttackComponent component with CombatCharacterAttackComponent.

![combatcharactercontroller_attack](https://user-images.githubusercontent.com/755461/160267937-ca950528-e8cc-4abb-b7dd-bc65bf7258c1.png)

2. add weapon animations in the same order as the CombatAnim enum (High, Low, Left, Right) in Character Model component.

![combatcharactercontroller_weapon](https://user-images.githubusercontent.com/755461/160267696-c34420f3-9721-4684-a622-77a07ebf6f46.png)

3. remove any default Charge state animations in Character Model (currently not supported).

_Weapon Items_

Modify **Equipment settings** to hide Crosshair and Fire type: Fire on Release.

![combatcharactercontroller_weaponitem](https://user-images.githubusercontent.com/755461/160267843-4900bfce-d419-4763-ab61-5bc733340d7f.png)

_Character and Monster prefabs_

If you want to add directional damage, add a Unity Ragdoll to the models (GameObject > 3D Object > Ragdoll) and CombatDamageableHitBox to all bones with colliders. This is out-of-the-box Kit functionality. You can adjust the Damage Rate by location accordingly, for example 2 on the head bone, 0.01 for shield, etc. The CombatDamageableHitBox adds Combat text which can optionally be displayed, for example on a Headshot, Block with shield or Backstab.

![combatcharactercontroller_hitbox](https://user-images.githubusercontent.com/755461/160267805-a9ad8f4a-65c2-4dbe-b312-5a788ee2b525.png)


### Settings

Combat has two settings found on the GameInstance component:

- Enable Rigid Body Combat Attack
- UI Combat Text String (utility to display any string as Combat Text)


### Credits

- arrow icon by [jojooid](https://www.flaticon.com/free-icons/ui) at Flaticon