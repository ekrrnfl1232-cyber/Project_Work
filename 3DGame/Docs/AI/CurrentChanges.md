# 변경 기록 및 현재 상태 (2026-10-05)

비교 기준: 갱신 직전 ChatGPT_Project_Code.md의 자체 코드. 기존 코드 문서는 최신 원문으로 덮어썼으며 이전 원문 사본은 보관하지 않는다. Git 저장소가 없어 커밋 기준 변경 추적은 불가능하며 파일 수정 시각만으로 변경을 단정하지 않았다.

자체 코드 82개. 추가 6개, 변경 19개, 삭제 1개.

## 확인한 주요 변화

- 로딩: Loding의 플레이어 HP/경험치 상태 캡처·복원과 영속 UI 참조 재연결 코드가 현재 원문에 없다. 대상 씬 활성화 후 2초 대기하고 Loading 씬을 언로드한다.
- UI 유지: UIConstroller는 현재 DontDestroyOnLoad(this)를 호출한다. 이전 문서의 Main Canvas 분리 보존, 중복 루트 제거 및 OnDestroy 정리 코드가 없다. 현재 Singleton<T>도 인스턴스를 검색하는 기능만 제공한다.
- 빌드 씬: Title, Loading, Lobby, Dungeon1, TestRoom이 활성 등록되어 있다. SceneType에는 Dungeon2와 BossRoom도 남아 있지만 빌드 등록 목록에는 없다.
- 에디터 시작: 새 PlayFromTitle.cs의 실제 시작 씬은 이름과 달리 TestRoom이다. UnityEditor API를 사용하는 파일이 Editor 폴더 밖에 있어 플레이어 빌드에서 확인이 필요하다.
- 던전: CombatSensor → EnPlayer 이벤트 → MonsterSpawn → ObjectPoolManager → DungeonManager 등록 → DeadMonster 이벤트 → ClearCombat 이벤트 구조가 추가되었다.
- 몬스터: 문자열 상태 키가 MonsterState enum으로 바뀌었으며 Target, data, NavMeshAgent, 상태 전환 및 재생성 흐름이 수정되었다. MonsterView가 풀에서 HP 바를 가져온다.
- 이벤트: 전투방 진입, 스폰, 추적, 전투 종료 및 플레이어 사망 관련 이벤트가 GameEvents에 추가되었다.
- 이벤트 구독: PlayerStat, Inventory, QuestManager가 다시 람다로 구독/해제하는 형태다. 별도로 생성한 람다는 기존 구독을 제거하지 못하므로 씬 전환·재활성화 시 누적 가능성이 있다. DungeonManager에도 현재 해제 코드가 없다.
- NPC: QuestNPC의 유지된 QuestManager 자동 재연결 코드가 현재 없다.
- 플레이어: 이전 내보내기 대비 Player.cs, PlayerDashState.cs, PlayerCooldown.cs 및 PlayerView.cs는 동일하다. HP 바는 200 × HP 비율로 계산하는 수정이 유지되어 있으며 위치는 화면 투영 후 150픽셀 오프셋이다.
- 카메라: ChaseCamera.cs에 CameraChase 클래스가 추가되었으며 SmoothDamp로 타겟을 따라간다. 파일명과 MonoBehaviour 클래스명이 다르므로 에디터 컴포넌트 연결 확인이 필요하다.
- MonsterWolf는 아직 Start/Update가 빈 테스트 스크립트다. StatUI는 미완성 상태로 취급한다.

## 현재 코드에서 추가로 확인한 사항

- ObjectPoolManager는 HpBar enum을 사용하지만 SettingPool 사전에 HpBar 큐가 없다. pool[PoolType.HpBar] 접근은 실패할 수 있다. 미리 생성하는 HP 바의 부모도 uiParent가 아니라 매니저 transform이다.
- MonsterSpawn은 풀에서 얻은 몬스터에 data를 설정하지만 Monster.Awake는 이미 data를 읽는다. 프리팹 초기 데이터 연결 상태에 따라 초기화 문제가 발생할 수 있다.
- 몬스터 HP 바에는 여전히 현재 너비에 체력 비율을 곱하는 코드가 있다. 플레이어 HP 바 수정과 별개다.
- DungeonManager.IsKillMonster는 비어 있는 리스트에서도 RemoveAt(0)을 호출할 수 있다.
- 위 사항은 정적 원문 근거이며 실행 재현 결과가 아니다. 이번 작업에서는 게임 코드와 자산을 수정하지 않았다.

## 씬·프리팹 변화 비교 범위

기존 문서에는 씬·프리팹 원문 또는 해시 기준점이 없으므로 이전 대비 모든 자산 변경을 정확히 복원할 수 없다. 현재 설정은 ScenePrefabSnapshot.md에 기록하고 ExportManifest.json에 SHA-256 기준점을 만들었다. 다음 내보내기부터 이 기준과 비교할 수 있다.

## 파일별 코드 변화

| 상태 | 경로 |
|---|---|
| 변경 | Assets/0.Script/0.Game/0.Manager/Dungeon/DungeonManager.cs |
| 변경 | Assets/0.Script/0.Game/0.Manager/GameEvents.cs |
| 변경 | Assets/0.Script/0.Game/1.Player/PlayerStat.cs |
| 변경 | Assets/0.Script/0.Game/1.Player/PlayerState/PlayerAreaSkillState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterAttackState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterDeadState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterHitState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterIdleState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterMoveState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterPatrolState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster State/MonsterReviveState.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/Monster.cs |
| 변경 | Assets/0.Script/0.Game/2.Monster/MonsterView.cs |
| 변경 | Assets/0.Script/0.Game/4.Entity/NPC/QuestNPC.cs |
| 추가 | Assets/0.Script/0.Game/Camera/ChaseCamera.cs |
| 추가 | Assets/0.Script/0.Game/Dungeon/CombatSensor.cs |
| 추가 | Assets/0.Script/0.Game/Dungeon/MonsterSpawn.cs |
| 추가 | Assets/0.Script/0.Game/Dungeon/ObjectPoolManganer.cs |
| 추가 | Assets/0.Script/0.Game/MonsterTest/MonsterWolf.cs |
| 변경 | Assets/0.Script/1.UI/Inventory/Inventory.cs |
| 변경 | Assets/0.Script/1.UI/Manager/QuestManager.cs |
| 변경 | Assets/0.Script/1.UI/Manager/UIConstroller.cs |
| 변경 | Assets/0.Script/Loding.cs |
| 추가 | Assets/0.Script/PlayFromTitle.cs |
| 변경 | Assets/0.Script/SceneLoader.cs |
| 삭제 | Assets/0.Script/0.Game/4.Entity/Potal.cs |
