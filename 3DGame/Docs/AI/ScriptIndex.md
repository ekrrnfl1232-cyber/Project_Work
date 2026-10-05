# 전체 스크립트 색인 (2026-10-05)

자체 C# 스크립트 82개. 원문은 ChatGPT_Project_Code.md와 ChatGPT_Project_Bundle.md에 포함되어 있습니다.

| 경로 | 선언된 타입 |
|---|---|
| Assets/0.Script/0.Game/0.Manager/Cooldown.cs | Cooldown |
| Assets/0.Script/0.Game/0.Manager/Dungeon/DungeonManager.cs | DungeonManager |
| Assets/0.Script/0.Game/0.Manager/GameEvents.cs | GameEvents |
| Assets/0.Script/0.Game/0.Manager/GameManager.cs | GameState, GameManager |
| Assets/0.Script/0.Game/0.Manager/InputManger.cs | InputManger |
| Assets/0.Script/0.Game/0.Manager/Interaction/IInterectable.cs | IInterectable |
| Assets/0.Script/0.Game/0.Manager/SaveManager.cs | GameSaveData, InvenData, EquipData, SaveManager |
| Assets/0.Script/0.Game/0.Manager/Singleton/Singleton.cs | Singleton |
| Assets/0.Script/0.Game/0.Manager/State/IState.cs | IState |
| Assets/0.Script/0.Game/1.Player/Player.cs | PlayerState, Player |
| Assets/0.Script/0.Game/1.Player/PlayerCooldown.cs | PlayerCool, PlayerCooldown |
| Assets/0.Script/0.Game/1.Player/PlayerStat.cs | PlayerStat |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerAreaSkillState.cs | PlayerAreaSkillState |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerAttackState.cs | PlayerAttackState |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerBladeSkill.cs | PlayerBladeSkill |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerDashState.cs | PlayerDashState |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerDeadState.cs | PlayerDeadState |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerHitState.cs | PlayerHitState |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerIdle.cs | PlayerIdle |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerJumpState.cs | PlayerJumpState |
| Assets/0.Script/0.Game/1.Player/PlayerState/PlayerMoveState.cs | PlayerMoveState |
| Assets/0.Script/0.Game/1.Player/PlayerView.cs | PlayerSkill, PlayerView |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterAttackState.cs | MonsterAttackState |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterDeadState.cs | MonsterDeadState |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterHitState.cs | MonsterHitState |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterIdleState.cs | MonsterIdleState |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterMoveState.cs | MonsterMoveState |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterPatrolState.cs | MonsterPatrolState |
| Assets/0.Script/0.Game/2.Monster/Monster State/MonsterReviveState.cs | MonsterReviveState |
| Assets/0.Script/0.Game/2.Monster/Monster.cs | MonsterState, Monster |
| Assets/0.Script/0.Game/2.Monster/MonsterModel.cs | MonsterModel |
| Assets/0.Script/0.Game/2.Monster/MonsterView.cs | MonsterView |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossAreaAttack.cs | BossAreaAttack |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossChargeAttack.cs | BossChargeAttack |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossChaseState.cs | BossChaseState |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossDeadState.cs | BossDeadState |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossIdleState.cs | BossIdleState |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossNormalAttackState.cs | BossNormalAttackState |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossPhaseChangeState.cs | BossPhaseChangeState |
| Assets/0.Script/0.Game/3.Boss/Boss State/BossSelectAttack.cs | BossSelectAttack |
| Assets/0.Script/0.Game/3.Boss/Boss.cs | BossState, Boss |
| Assets/0.Script/0.Game/3.Boss/BossStat.cs | BossStat |
| Assets/0.Script/0.Game/3.Boss/BossView.cs | BossView |
| Assets/0.Script/0.Game/4.Entity/Box.cs | Box |
| Assets/0.Script/0.Game/4.Entity/Door.cs | Door |
| Assets/0.Script/0.Game/4.Entity/NPC/NPC.cs | NPC |
| Assets/0.Script/0.Game/4.Entity/NPC/QuestNPC.cs | QuestNPC |
| Assets/0.Script/0.Game/Camera/ChaseCamera.cs | CameraChase |
| Assets/0.Script/0.Game/Dungeon/CombatSensor.cs | CombatSensor |
| Assets/0.Script/0.Game/Dungeon/MonsterSpawn.cs | MonsterSpawn |
| Assets/0.Script/0.Game/Dungeon/ObjectPoolManganer.cs | PoolType, ObjectPoolManager |
| Assets/0.Script/0.Game/IDamageable.cs | IDamageable |
| Assets/0.Script/0.Game/MonsterTest/MonsterWolf.cs | MonsterWolf |
| Assets/0.Script/1.UI/DamageFontManager.cs | DamageFontManager |
| Assets/0.Script/1.UI/Inventory/EquipmentSlot.cs | EquipmentSlot |
| Assets/0.Script/1.UI/Inventory/EquipSystem.cs | EquipSystem |
| Assets/0.Script/1.UI/Inventory/Inventory.cs | Inventory |
| Assets/0.Script/1.UI/Inventory/InventoryItem.cs | InventoryItem |
| Assets/0.Script/1.UI/Inventory/InventoryUI.cs | InventoryUI |
| Assets/0.Script/1.UI/Manager/QuestManager.cs | QuestManager |
| Assets/0.Script/1.UI/Manager/UIConstroller.cs | UIConstroller |
| Assets/0.Script/1.UI/PopUp/ListPopup.cs | ListPopup |
| Assets/0.Script/1.UI/PopUp/PopupController.cs | PopupController |
| Assets/0.Script/1.UI/PopUp/ToastPopup.cs | ToastPopup |
| Assets/0.Script/1.UI/Quest/QuestProgress.cs | QuestProgress |
| Assets/0.Script/1.UI/Quest/QuestUi.cs | QuestUi |
| Assets/0.Script/1.UI/Stat/PlusStatSlot.cs | PlusStatSlot |
| Assets/0.Script/1.UI/Stat/StatUI.cs | StatUI |
| Assets/0.Script/1.UI/Stat/StatUIView.cs | StatUIView |
| Assets/0.Script/9.Addressable/AddressableLoader.cs | AddressableLoader |
| Assets/0.Script/Audio/AudioManager.cs | ClipType, AudioManager, Clip |
| Assets/0.Script/Audio/ClickSount.cs | ClickSount |
| Assets/0.Script/Loding.cs | Loding |
| Assets/0.Script/PlayFromTitle.cs | PlayFromTitle |
| Assets/0.Script/SceneLoader.cs | SceneType, SceneLoader |
| Assets/0.Script/VFX/VFXManager.cs | VFXtype, VFXManager, VFXData |
| Assets/6.Data/DataScript/ItemScriptable.cs | ItemType, EquipType, ItemScriptable |
| Assets/6.Data/DataScript/MonsterData.cs | MonsterData |
| Assets/6.Data/DataScript/PlayerData.cs | PlayerData |
| Assets/6.Data/DataScript/QuestData.cs | QuestState, QuestType, QuestData |
| Assets/6.Data/Json/Testjson.cs | Testjson |
| Assets/6.Data/Json/TestSaveData.cs | TestSaveData |
