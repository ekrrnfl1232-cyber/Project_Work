# SwordAttack 1·2·3 편집 준비

에디터 도구: `Assets/Editor/EditableSwordAttackClips.cs`

Unity가 새 스크립트를 컴파일하면 FBX의 공격 클립을 아래 경로로 복제하고 플레이어 Animator의 해당 Motion 참조를 교체한다.

- `Assets/7.Animator/Player/SwordAttack1.anim`
- `Assets/7.Animator/Player/SwordAttack2.anim`
- `Assets/7.Animator/Player/SwordAttack3.anim`

자동 실행되지 않으면 Play 모드를 종료하고 `Tools > Player > Prepare Editable SwordAttack 1-3`을 실행한다. 컴파일 오류가 있으면 먼저 해당 오류를 해결해야 메뉴와 자동 실행이 동작한다.

복제본이 존재하면 다시 덮어쓰지 않으므로 사용자가 추가한 이벤트와 키프레임을 보존한다. 원본 FBX 클립과 Animator 파라미터·전환 조건은 유지한다. Animation 창에서 복제된 `.anim`을 선택하고 이벤트 위치를 설정한다. 이벤트 수신 함수는 Animator가 붙은 GameObject의 MonoBehaviour에 있어야 한다.

## 현재 이벤트 설정 (2026-10-07)

Unity MCP로 현재 Lobby의 플레이어 리그와 Animator를 조회하고, 격리된 미리보기에서 세 클립을 프레임별로 샘플링해 검의 동작을 확인했다. 프레임 번호는 0부터 시작하며 클립은 30fps다. 이벤트의 Int 인자는 각 타 번호다.

| 클립 | AttackHit | AttackComboCheck | AttackEnd |
|---|---|---|---|
| SwordAttack1 | 10프레임 / 0.333333초 | 18프레임 / 0.600000초 | 19프레임 / 0.633333초 |
| SwordAttack2 | 3프레임 / 0.100000초 | 10프레임 / 0.333333초 | 15프레임 / 0.500000초 |
| SwordAttack3 | 7프레임 / 0.233333초 | 없음 | 27프레임 / 0.900000초 |

1타는 앞쪽을 가르는 프레임, 2타는 반대 방향으로 앞쪽을 가르는 프레임, 3타는 찌르기가 앞으로 뻗은 프레임에 타격 이벤트를 배치했다. 2·3타 종료는 마지막 한 프레임 전을 유지했다.

1타 단독 종료는 콤보 판정 다음 프레임(19프레임)에 AttackEnd를 호출하고 SwordAttack1→Idle 전환은 Has Exit Time을 끈 0.25초 블렌딩으로 복귀한다. 1타 클립의 마지막 자세는 2타 시작 자세이므로 단타에서는 마지막까지 기다리지 않고 회복 동작 중 Idle을 섞는다. 2타가 예약되면 18프레임의 콤보 이벤트가 먼저 타 번호를 2로 바꾸므로 이전 1타의 AttackEnd는 무시된다.

현재 Animator의 상태 이름은 SwordAttack1/2/3이며 모두 speed=1이다. Sword01/02/03 파라미터 이름을 유지한다. SwordAttack1→2 및 SwordAttack2→3 전환은 Has Exit Time을 끄고 Fixed Duration 0.05초로 설정했다. 콤보 이벤트가 발생할 때 즉시 전환을 시작하며, 타격 이벤트가 긴 전환 블렌딩 중 먼저 호출되는 것을 피한다.

PlayerAttackState.BeginAttack은 Speed를 0으로 설정하고 이전 ReturnIdle 트리거를 정리한다. 이동 상태의 Speed가 남아 1타의 종료 이벤트 전에 Run으로 전환되는 것을 방지한다.

## 검증 범위

- Unity에서 저장된 AnimationEvent 함수명과 Int 인자, Animator의 Motion 참조를 확인했다.
- Animator가 붙은 동일 GameObject에 Player.AttackHit(int), AttackComboCheck(int), AttackEnd(int)가 존재함을 확인했다.
- 격리된 리그의 실제 Animator를 수동 평가하여 이동 상태에서 시작한 단타, 2연타, 3연타가 각각 Idle로 복귀함을 확인했다. 새 타격 시점은 전환 블렌딩이 끝난 뒤에 도달했다.
- 이 전환 검증에서는 fireEvents=false로 두고 이벤트 시점을 읽어 콤보/종료 트리거를 모의 처리했다. 실제 플레이 입력, 이벤트 콜백 전달, 적 피해 및 피격/대시 중단은 Play 모드에서 별도로 확인해야 한다.
- 이후 단타 복귀 조정(1타 AttackEnd=19프레임, Idle 블렌딩=0.25초)은 MCP의 Unity 세션 연결이 끊긴 상태에서 파일에 적용했다. 이 조정 후의 Unity 재임포트 및 시각/실행 검증은 아직 완료하지 않았다.
