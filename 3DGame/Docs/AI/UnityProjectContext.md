# Unity 프로젝트 컨텍스트

분석일: 2026-10-05 (Asia/Seoul). 프로젝트 루트: `C:/Users/User/Desktop/포폴백업/3DGame`. 이 폴더는 Git 저장소가 아니므로 분석 커밋은 없음. 이전 코드 내보내기와 비교한 변화는 CurrentChanges.md에서 확인한다.

## 확인된 환경

- Unity 6000.5.0f1 (`ProjectSettings/ProjectVersion.txt`)
- URP 17.5.0 및 Shader Graph 사용 (`Packages/manifest.json`). `Assets/Settings`에 렌더 설정 자산이 있음.
- Input System 1.19.0 사용. `Assets/InputSystem_Actions.cs`가 생성된 입력 래퍼이며 `ProjectSettings/ProjectSettings.asset`의 `activeInputHandler: 2`는 구형 입력 API도 활성화된 상태를 뜻함. `Player`의 `Input.mousePosition` 및 `InputManger`의 Input System 사용으로 코드에서도 둘 다 확인됨.
- Addressables 2.9.1, AI Navigation 2.0.13, Cinemachine 2.10.7, DOTween(`Assets/Plugins/Demigiant`), uGUI, TextMesh Pro 사용. Multiplayer Center 패키지는 있지만 게임 코드의 네트워크 사용은 확인되지 않음.
- Unity Test Framework 패키지는 설치되어 있으나 프로젝트 자체 테스트/asmdef는 확인되지 않음. Unity Editor 연결 도구는 현재 세션에서 확인되지 않아 실행 상태·콘솔·빌드는 미검증.

## 프로젝트 구조

- `Assets/0.Script`: 주요 게임 코드 76개. `0.Game/0.Manager`는 전역 매니저·상태/상호작용 인터페이스, `1.Player`, `2.Monster`, `3.Boss`는 각 캐릭터와 상태, `4.Entity`는 상호작용 대상. `0.Game/Dungeon`에는 전투방 센서·스폰·풀, `Camera`에는 추적 카메라가 있다. `1.UI`는 인벤토리·장비·퀘스트·스탯·팝업, 그 외 오디오·VFX·씬 로딩·Addressables. `Assets/6.Data` 코드 6개를 합쳐 자체 C#은 82개다.
- `Assets/6.Data/DataScript`: `PlayerData`, `MonsterData`, `QuestData`, `ItemScriptable` ScriptableObject 정의. `Assets/6.Data`와 `Assets/Resources/ItemData`에 인스턴스가 있음. `Assets/6.Data/Json`에는 테스트성 JSON 코드가 있음.
- `Assets/9.Scenes`: 빌드 장면. `Assets/1.Prefab`, `5.Resource`, `7.Animator`, `8.meterial`, `Resources`, `Settings`, `AddressableAssetsData`: 게임 자산 및 설정.
- `Assets/Huscarl`, `GhostCharacter_Free`, `UnityTechnologies`, `GabrielAguiarProductions`, `Plugins` 등은 외부/샘플 자산으로 보임. `Library`, `Logs`, `UserSettings`는 생성·로컬 데이터로 분석 범위에서 제외.
- 자체 asmdef/asmref는 확인되지 않음. 자체 C# 코드는 기본 `Assembly-CSharp`에 속하는 것으로 보이며 외부 플러그인과 생성 코드는 별도 취급.

## 실행 및 시스템 흐름

1. `ProjectSettings/EditorBuildSettings.asset`의 활성 장면 순서: `Title` → `Loading` → `Lobby` → `Dungeon1` → `TestRoom`. 빌드 시작 장면은 `Title`이며 새 `PlayFromTitle.cs`는 에디터 Play 시작 씬을 `TestRoom`으로 설정한다.
2. `SceneLoader.LoadGame/LoadingScene` → `Loding.LoadScene` → `Loading` 장면 진입 → 대상 장면 비동기 Additive 로드 → 활성 장면 설정 → 2초 대기 → `Loading` 언로드. `SceneType`에는 Lobby·Dungeon1·Dungeon2·BossRoom·TestRoom이 있다. Dungeon2와 BossRoom은 현재 활성 빌드 목록에 없다.
3. `InputManger`가 `InputSystem_Actions`를 생성·활성화. `Player`는 액션 이벤트와 이동 입력을 받아 `IState` 구현체로 전환하고 `PlayerStat`·`PlayerView`를 사용.
4. `Monster`는 `MonsterModel`, `MonsterView`, `NavMeshAgent`, `MonsterState` enum 키의 상태 사전을 사용. `Boss`는 `BossStat`, `BossView`, `NavMeshAgent`, enum 키의 상태 사전과 공격 쿨다운을 사용.
5. 몬스터 사망 → `GameEvents.PlayerKill` → 경험치(`PlayerStat`), 골드/퀘스트/UI 관련 구독자. 새 전투방 흐름은 `CombatSensor` → `EnPlayer` → `MonsterSpawn` → `ObjectPoolManager` → `DungeonManager` 등록 → `DeadMonster` → `ClearCombat` → 벽 해제다. `UIConstroller`의 현재 원문은 `DontDestroyOnLoad(this)`만 호출하며 별도 Canvas 분리 및 중복 제거가 없다.
6. `UIConstroller`는 인벤토리·장비·퀘스트 UI를 열고 `SaveManager`에 데이터를 저장/로드. 저장 형식은 `JsonUtility`와 `Application.persistentDataPath/save.Json`.
7. `AddressableLoader`는 `Skeleton` 프리팹을 비동기 로드하지만 현재 구현에서는 결과 프리팹을 인스턴스화하지 않음.

## 코드 관례와 작업 시 주의

- 대부분 전역 네임스페이스, MonoBehaviour와 `Singleton<T>`, Inspector 직렬화 참조를 사용. 상태는 `Enter/Tick/Exit` 인터페이스를 따름.
- 정적 `GameEvents`는 직접 `Action` 필드를 공개한다. `Inventory`, `PlayerStat`, `QuestManager`는 현재 람다로 구독 및 해제를 시도하므로 실제 구독이 남을 수 있다. `DungeonManager`는 Awake에서 DeadMonster를 구독하며 해제 코드가 없다.
- `Loding`의 이전 상태 캡처·복원 및 플레이어/UI 참조 재연결 구현이 현재 코드에는 없다. `PlayerStat`의 HP는 Awake에서 기본 HP로 초기화된다. 레벨·경험치는 정적 필드로 첫 초기화 이후 유지한다.
- `UIConstroller`의 중복 제거 및 Main Canvas 분리 보존, `QuestNPC`의 유지된 QuestManager 재연결 코드가 현재 원문에서 빠져 있다. 씬 전환 시 이전에 해결했다고 기록한 기능이 유지된다고 가정하지 않아야 한다.
- HUD와 UI 컴포넌트의 현재 직렬화 필드는 ScenePrefabSnapshot.md에서 확인한다. 프리팹 인스턴스 오버라이드를 포함한 원문은 텍스트 자산 ZIP에 있다.
- 플레이어 HP 바는 `200f * Clamp01(hp/maxHp)`로 계산해 연속 피격 시 너비가 누적해서 줄어드는 것을 방지한다. 위치는 화면 투영 후 150픽셀 오프셋이며 장식 디자인·머리 본 배치는 원복 상태다. 몬스터 HP 바에는 같은 계산 수정이 적용되어 있지 않다.
- ObjectPoolManager의 HpBar 큐 누락, 몬스터 생성 후 data 할당 순서, Editor 폴더 밖 PlayFromTitle의 UnityEditor 참조, ChaseCamera.cs와 CameraChase 클래스명 불일치는 CurrentChanges.md에 근거를 기록했다. 실행·빌드 검증은 하지 않았다.
- `PlayerDeadState` 파일은 존재하지만 `Player.SettingState()` 사전에 포함되지 않음. 의도된 미사용 코드인지 확인 필요.
- `UIConstroller.OnLoad()`는 저장 파일이 없으면 반환되는 `null`을 검사하지 않음. 장비 로드는 주석 처리되어 있음.
- `QuestManager.CompleteQuest()`는 완료 후에도 `false`를 반환함. 수정 요청 시 호출부와 기대 동작을 함께 확인.
- 이 항목들은 정적 코드 관찰이며 Unity 실행 검증 결과가 아님.

## 전체 코드 찾기

자체 스크립트별 경로와 타입 선언은 [ScriptIndex.md](ScriptIndex.md)에 정리했다. 현재 전체 원문은 ChatGPT_Project_Code.md, 일반 ChatGPT 첨부용 통합 자료는 ChatGPT_Project_Bundle.md다. 통합 자료는 구조·변경 기록·코드·씬 연결·빌드 설정·생성 입력 래퍼를 포함한다. 일반 ChatGPT에서 확인하려면 파일 자체를 첨부해야 하며 로컬 폴더가 자동 연결되는 것은 아니다. 갱신 도구는 RefreshChatExport.ps1이다.

## 확인 근거

`ProjectSettings/ProjectVersion.txt`, `ProjectSettings/EditorBuildSettings.asset`, `ProjectSettings/ProjectSettings.asset`, `Packages/manifest.json`, `Assets/0.Script`의 핵심 매니저·캐릭터·UI 스크립트, `Assets/6.Data/DataScript/*.cs`를 확인함. 씬·프리팹·데이터 자산의 자체 스크립트 직렬화 필드와 Canvas 설정을 정적으로 확인했다. 플레이 모드 동작, 테스트 및 빌드는 확인하지 않았다.
