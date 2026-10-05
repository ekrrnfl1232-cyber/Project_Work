# 3DGame 전체 자체 코드 (2026-10-05)

Assets/0.Script와 Assets/6.Data의 C# 82개 전체 원문. UTF-8 또는 CP949를 판별해 UTF-8 문서로 내보냈습니다. 외부 플러그인 및 Unity 생성 코드는 별도 범위입니다. 스냅샷은 원본과 자동 동기화되지 않습니다.

## Assets/0.Script/0.Game/0.Manager/Cooldown.cs

```csharp
using UnityEngine;

[System.Serializable]
public class Cooldown
{
    private float cooldownTime;
    private float timer;


    public Cooldown(float cooldwonTime)
    {
        this.cooldownTime = cooldwonTime;
        timer = 0f;
    }
    public bool IsReady
    {
        get { return timer <= 0f; }
    }

    public void Start()
    {
        timer = cooldownTime;
    }

    public void Tick(float time)
    {
        if (timer > 0f)
            timer -= time;
    }
    public void Reset()
    {
        timer = 0f;
    }
}
```

## Assets/0.Script/0.Game/0.Manager/Dungeon/DungeonManager.cs

```csharp
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    private List<GameObject> monster;

    private void Awake()
    {
        monster = new();
        GameEvents.DeadMonster += IsKillMonster;
    }

    public void Add(GameObject mon)
    {
        monster.Add(mon);
    }

    public void IsKillMonster()
    {
        monster.RemoveAt(0);
        if(monster.Count == 0)
        {
            GameEvents.RaiseClearCombat();
        }
    }
}
```

## Assets/0.Script/0.Game/0.Manager/GameEvents.cs

```csharp
using System;
using UnityEngine;

public static class GameEvents
{
    // Action = 일반함수(), Action<자료형> = 매개함수(변수)

    public static Action OnQuestChanged;
    // 플레이어가 몬스터 잡았을때의 이벤트
    public static Action<MonsterData, int, float> PlayerKill;
    public static Action DeadMonster;
    public static Action<int, float> ChangeCurrency;
    public static Action ChangeEXPUpdate;
    // 플레이어가 전투방 진입 시
    public static Action<Transform> EnPlayer;
    public static Action<Transform> SpawnMonster;
    public static Action<Transform> ChaseMonster;
    public static Action ClearCombat;
    public static Action DeadPlayer;

    public static void RaiseQuestChanged()
    {
        OnQuestChanged?.Invoke();
    }

    public static void RaiseKillChange(MonsterData data, int gold, float exp)
    {
        PlayerKill?.Invoke(data, gold, exp);
    }

    public static void RaiseChangeDeadMonster()
    {
        DeadMonster?.Invoke();
    }

    public static void RaiseChangeCurrency(int gold, float exp)
    {
        ChangeCurrency?.Invoke(gold, exp);
    }

    public static void RaiseChangeEXPUpdate()
    {
        ChangeEXPUpdate?.Invoke();
    }

    public static void RaiseChangeEnPlayer(Transform combat)
    {
        EnPlayer?.Invoke(combat);
    }

    public static void RaiseChangeSpawnMonster(Transform mon)
    {
        SpawnMonster?.Invoke(mon);
    }

    public static void RaiseChaseMonster(Transform target)
    {
        ChaseMonster?.Invoke(target);
    }

    public static void RaiseClearCombat()
    {
        ClearCombat?.Invoke();
    }

    public static void RaiseChangeDeadPlayer()
    {
        DeadPlayer?.Invoke();
    }
}
```

## Assets/0.Script/0.Game/0.Manager/GameManager.cs

```csharp
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing, Pause, Stop
}

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        State = GameState.Playing;
        DontDestroyOnLoad(gameObject);
    }

    public int Score { get; set; }

    public GameState State { get; set; }



}
```

## Assets/0.Script/0.Game/0.Manager/InputManger.cs

```csharp
using System;
using UnityEngine;

public class InputManger : Singleton<InputManger>
{
    public InputSystem_Actions input {  get; private set; }

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input?.Enable();
    }
    private void OnDisable()
    {
        input?.Disable();
    }
}
```

## Assets/0.Script/0.Game/0.Manager/Interaction/IInterectable.cs

```csharp
using System.Collections.Generic;
using UnityEngine;

public interface IInterectable
{
    void Interact();
}
```

## Assets/0.Script/0.Game/0.Manager/SaveManager.cs

```csharp
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSaveData
{
    public int Hp;
    public int level;
    public float exp;
    public int gold;
    public InvenData[] invendata;
    public EquipData[] equipDatas;
}
[System.Serializable]
public class InvenData
{
    public int id;
    public uint stack;
}
[System.Serializable]
public class EquipData
{
    public int id;
}

public class SaveManager : Singleton<SaveManager>
{
    public string savePath;
    
    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.Json");
    }
    public void Save(GameSaveData data)
    {
        if (data == null)
        {
            Debug.Log("데이터 누락");
            return;
        }
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
        File.WriteAllText(savePath, json);
    }
    [ContextMenu("Load")]
    public GameSaveData Load()
    {
        if(!File.Exists(savePath))
        {
            Debug.Log("Save File 누락");
            return null;
        }
        try
        {
            string json = File.ReadAllText(savePath);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
            return data;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }
    [ContextMenu("Delete")]
    public void Delete()
    {
        if(!File.Exists(savePath))
        {
            return;
        }

        Debug.Log($"{savePath} 삭제");
        File.Delete(savePath);
    }
}
```

## Assets/0.Script/0.Game/0.Manager/Singleton/Singleton.cs

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindAnyObjectByType(typeof(T));
            }
            return instance;
        }
    }
}
```

## Assets/0.Script/0.Game/0.Manager/State/IState.cs

```csharp
using UnityEngine;

public interface IState
{
    void Enter();

    void Tick();
    void Exit();
    
}
```

## Assets/0.Script/0.Game/1.Player/Player.cs

```csharp
using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public enum PlayerState
{
    idleState,
    moveState,
    attackState,
    jumpState,
    dashState,
    hitState,
    AreaState
}

public class Player : MonoBehaviour, IDamageable
{
    [Header("Animator")]
    public Animator animator;
    [Header("Script")]
    public PlayerView view { get; private set; }
    public PlayerStat stat;
    public PlayerData data;

    [Header("Controller")]
    public CharacterController controll;
    public BoxCollider sword;

    private bool isTargeting = false;
    public bool IsTargeting { get { return isTargeting; } set { isTargeting = value; } }

    // 상태
    private IState currentState;
    private PlayerState currentKey;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }

    // 쿨타임
    private PlayerCooldown cooldown = new PlayerCooldown();
    public PlayerCooldown Cool { get { return cooldown; } }

    private void Awake()
    {
        SettingState();
    }

    private void Start()
    {
        view = GetComponent<PlayerView>();
        view.ExpUpdata();
        view.CreateHp();
        InputManger.Instance.input.Player.Get().actionTriggered += OnAction;
        ChangeState(PlayerState.idleState);
    }

    private void Update()
    {
        
        view.HPbar(transform.position);
        if (InputManger.Instance.input.Player.Move.IsPressed())
        {
            stat.MoveDir = InputManger.Instance.input.Player.Move.ReadValue<Vector2>();
        }
        else
        {
            stat.MoveDir = Vector2.zero;
        }
        stat.Movement = new Vector3(stat.MoveDir.x, 0, stat.MoveDir.y).normalized;
        if (InputManger.Instance.input.Player.enabled)
        {
            Interect();
            Look();
        }
        Gravity();
        Cool.TIck(Time.deltaTime);
        currentState?.Tick();
    }

    public void ChangeState(PlayerState state)
    {
        prevState = currentKey;
        currentState?.Exit();
        currentState = States[state];
        currentKey = state;
        currentState?.Enter();
    }

    private void SettingState()
    {
        States = new Dictionary<PlayerState, IState>()
        {
            { PlayerState.idleState, new PlayerIdle(this) },
            { PlayerState.moveState, new PlayerMoveState(this) },
            { PlayerState.attackState, new PlayerAttackState(this) },
            { PlayerState.jumpState, new PlayerJumpState(this) },
            { PlayerState.dashState, new PlayerDashState(this) },
            { PlayerState.hitState, new PlayerHitState(this) },
            { PlayerState.AreaState, new PlayerAreaSkillState(this) }
        };
    }

    public void TakeDamage(int damage)
    {
        if (stat.Hp > 0)
        {
            stat.Hp -= damage;
            DamageFontManager.Instance.CreateText(damage, transform.position);
            ChangeState(PlayerState.hitState);
        }
    }

    private void Gravity()
    {
        if(controll.isGrounded)
        {
            if(stat.VerticalVelo < 0f)
                stat.VerticalVelo = -2f;
        }
        else
        {
            stat.VerticalVelo += Physics.gravity.y * Time.deltaTime;
        }
        stat.Gravity = new Vector3 (0, stat.VerticalVelo, 0 );
        controll.Move(stat.Gravity * Time.deltaTime);
    }

    void Interect()
    {
        Vector3 posInter = transform.position;
        posInter.y += 1f;
        Collider[] colls = Physics.OverlapSphere(posInter, stat.InterationScale);
        bool isFind = false;
        foreach (var col in colls)
        {
            if (col.TryGetComponent<IInterectable>(out IInterectable interact))
            {
                isFind = true;
                if (InputManger.Instance.input.Player.Interact.triggered)
                {
                    interact.Interact();
                }
                break;
            }
        }
        view.CheckBox(isFind);
    }
    private void Look()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    private void OnAction(InputAction.CallbackContext contxt)
    {
        if(!controll.isGrounded)
        {
            return;
        }
        if(!contxt.performed)
        {
            return;
        }
        switch (contxt.action.name)
        {
            case "Attack":
                if(Cool.IsReady(PlayerCool.Attack) && IsTargeting == false)
                {
                    ChangeState(PlayerState.attackState);
                }
                break;
            case "Jump":
                if (IsTargeting == false)
                {
                    ChangeState(PlayerState.jumpState);
                }
                break;
            case "Sprint":
                if (Cool.IsReady(PlayerCool.Dash) && IsTargeting == false)
                {
                    ChangeState(PlayerState.dashState);
                }
                break;
            case "Area":
                if(Cool.IsReady(PlayerCool.Area))
                {
                    IsTargeting = true;
                    ChangeState(PlayerState.AreaState);
                }
                break;
        }
    }

    private void OnDisable()
    {
        InputManger.Instance.input.Player.Get().actionTriggered -= OnAction;
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerCooldown.cs

```csharp
using UnityEngine;

public enum PlayerCool
{
    Attack,
    Dash,
    Area
}

public class PlayerCooldown
{
    private Cooldown atkCool;
    private Cooldown dashcool;
    private Cooldown area;

    public PlayerCooldown ()
    {
        atkCool = new Cooldown(1f);
        dashcool = new Cooldown(1f);
        area = new Cooldown(2f);
    }

    public void Start(PlayerCool cool)
    {
        if (cool == PlayerCool.Attack)
        {
            atkCool.Start();
        }
        if (cool == PlayerCool.Dash)
        {
            dashcool.Start();
        }
        if(cool == PlayerCool.Area)
        {
            area.Start();
        }
    }

    public void TIck(float time)
    {
        atkCool.Tick(time);
        dashcool.Tick(time);
        area.Tick(time);
    }

    public void Reset(PlayerCool cool)
    {
        if (cool == PlayerCool.Attack)
        {
            atkCool.Reset();
        }
        if (cool == PlayerCool.Dash)
        {
            dashcool.Reset();
        }
        if (cool == PlayerCool.Area)
        {
            area.Reset();
        }
    }

    public bool IsReady(PlayerCool cool)
    {
        if(cool == PlayerCool.Attack)
        {
            return atkCool.IsReady;
        }
        if(cool == PlayerCool.Dash)
        {
            return dashcool.IsReady;
        }
        if(cool == PlayerCool.Area)
        {
            return area.IsReady;
        }
        return false;
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerStat.cs

```csharp
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private PlayerData data;

    [SerializeField]
    private int hp;

    [SerializeField]
    private static int level;

    [SerializeField]
    private static float exp;

    [SerializeField]
    private static float maxExp;

    [SerializeField]
    private int baseAttack;

    [SerializeField]
    private int baseDefence;

    [SerializeField]
    private float baseSpeed;

    [SerializeField]
    private int areaDamage;

    private static bool isInit = false;

    public int Hp { get { return hp; } set { hp = value; } }
    public int BaseMaxHp { get; set; }
    public static int Level {get { return level; } set { level = value; } }
    public static float Exp { get { return exp; } set { exp = value; } }
    public static float MaxExp { get { return maxExp; } set { maxExp = value; } }

    public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }
    public int BaseDefence { get { return baseDefence; } private set { baseDefence = value; } }
    public float BaseSpeed { get { return baseSpeed; } private set { baseSpeed = value; } }
    public int AreaDamage { get { return areaDamage; } private set { areaDamage = value; } }

    public float InterationScale { get; set; }
    public float VerticalVelo { get; set; }
    public Vector3 Movement { get; set; }
    public Vector3 Gravity { get; set; }
    public Vector2 MoveDir { get; set; }


    private void Awake()
    {
        GameEvents.PlayerKill += (data, gold, exp) => AddExp(exp);
        GameEvents.ChangeCurrency += (gold, exp) => AddExp(exp);
        Hp = BaseMaxHp = data.Maxhp;
        InterationScale = data.InterationScale;
        Gravity = Vector2.zero;

        BaseAttack = data.Wdamage;
        BaseSpeed = data.MoveForce;
        AreaDamage = data.AreaDmg;

        if(!isInit)
        {
            Level = 1;
            Exp = 0f;
            MaxExp = 500f;
            isInit = true;
        }

        VerticalVelo = 0f;
        ResetStat();
    }

    private void AddExp(float addExp)
    {
        Exp += addExp;
        if (Exp >= MaxExp)
        {
            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                Level += 1;
                MaxExp += 100f;
            }
        }
        GameEvents.RaiseChangeEXPUpdate();
    }

    public void ResetStat()
    {
        Hp = BaseMaxHp = data.Maxhp;
        BaseAttack = baseAttack;
        BaseDefence = baseDefence;
        BaseSpeed = baseSpeed;
    }

    public int TotalDamage()
    {
        return EquipSystem.Instance.EquipTotalDamage() + BaseAttack;
    }

    public int TotalDefence()
    {
        return EquipSystem.Instance.EquipTotalDamage() + BaseDefence;
    }

    public int TotalHP()
    {
        return EquipSystem.Instance.EquipTotalHP() + BaseMaxHp;
    }

    public float TotalSpeed()
    {
        return EquipSystem.Instance.EquipTotalSpeed() + BaseSpeed;
    }

    private void OnDestroy()
    {
        GameEvents.PlayerKill -= (data, gold, exp) => AddExp(exp);
        GameEvents.ChangeCurrency -= (gold, exp) => AddExp(exp);
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerAreaSkillState.cs

```csharp
using UnityEngine;

public class PlayerAreaSkillState : IState
{
    private Player player;
    private GameObject range;

    public PlayerAreaSkillState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        range = player.view.area;
        range.SetActive(true);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 pos = hit.point;
            pos.y = range.transform.position.y;
            range.transform.position = pos;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] targetCheck = Physics.OverlapSphere
            (range.transform.position, 2f, LayerMask.GetMask("Monster"));
            foreach(var tar in targetCheck)
            {
                if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    damage.TakeDamage(player.stat.AreaDamage);
                }
            }
            range.SetActive(false);
            player.IsTargeting = false;
            player.Cool.Start(PlayerCool.Area);
            player.ChangeState(PlayerState.idleState);
        }
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerAttackState.cs

```csharp
using UnityEngine;

public class PlayerAttackState : IState
{
    private Player player;
    public PlayerAttackState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.Cool.Reset(PlayerCool.Attack);
        player.animator.SetTrigger("Sword01");
        Vector3 posAttack = player.transform.position + player.transform.forward * 1f;
        posAttack.y += 0.5f;
        Collider[] targetCheck = Physics.OverlapBox
            (posAttack, new Vector3(1.4f, 1.4f, 1f), player.transform.rotation, 
            LayerMask.GetMask("Monster"));
        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(player.stat.TotalDamage());
                break;
            }
        }
        player.Cool.Start(PlayerCool.Attack);
        player.ChangeState(PlayerState.idleState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerBladeSkill.cs

```csharp
using UnityEngine;

public class PlayerBladeSkill : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerDashState.cs

```csharp
using UnityEngine;

public class PlayerDashState : IState
{
    private Player player;

    private float timer;
    private Vector3 dashDir;

    public PlayerDashState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Cool.Reset(PlayerCool.Dash);
        player.animator.SetBool("ShieldRush", true);
        timer = 0f;
        dashDir = player.transform.forward;
    }
    
    public void Exit()
    {
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        player.controll.Move(dashDir * player.data.DashForce * Time.deltaTime);

        if(timer >= 0.2f)
        {
            player.animator.SetBool("ShieldRush", false);
            player.Cool.Start(PlayerCool.Dash);
            player.ChangeState(PlayerState.idleState);
        }

    }

   

}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerDeadState.cs

```csharp
using UnityEngine;

public class PlayerDeadState : IState
{
    private Player player;

    public PlayerDeadState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Player Dead");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerHitState.cs

```csharp
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitState : IState
{
    private Player player;

    public PlayerHitState(Player player)
    {
        this.player = player;
        
    }
    public void Enter()
    {
        player.animator.SetTrigger("Hit");
        player.view.HpUpdate(player.stat.Hp, player.data.Maxhp);
        player.ChangeState(player.prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    { 
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerIdle.cs

```csharp
using UnityEngine;

public class PlayerIdle : IState
{
    private Player player;
    public PlayerIdle(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetFloat("Speed", 0);
        player.animator.SetTrigger("ReturnIdle");
    }

    public void Tick()
    {
        if (player.stat.MoveDir != Vector2.zero && player.IsTargeting == false)
        {
            player.ChangeState(PlayerState.moveState);
        }
    }

    public void Exit()
    {
    }

    
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerJumpState.cs

```csharp
using UnityEngine;

public class PlayerJumpState : IState
{
    private Player player;
    private float timer;

    public PlayerJumpState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetFloat("Speed", 0);
        player.animator.SetTrigger("ReturnIdle");
        player.stat.VerticalVelo = Mathf.Sqrt(player.data.JumpHeight * player.stat.Gravity.y * -2f);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (player.controll.isGrounded && player.stat.VerticalVelo < 0f)
            player.ChangeState(PlayerState.idleState);
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerState/PlayerMoveState.cs

```csharp
using UnityEngine;

public class PlayerMoveState : IState
{
    private Player player;
    public PlayerMoveState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.animator.SetFloat("Speed", player.data.MoveForce);
    }

    public void Exit()
    {

    }

    public void Tick()
    {
        player.controll.Move(player.stat.Movement * player.stat.TotalSpeed() * Time.deltaTime);
        if (player.stat.MoveDir == Vector2.zero)
        {
            player.ChangeState(PlayerState.idleState);
        }
    }
}
```

## Assets/0.Script/0.Game/1.Player/PlayerView.cs

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSkill
{
    Area
}

public class PlayerView : MonoBehaviour
{

    [Header("Skill")]
    [SerializeField] public GameObject area;

    [Header("UI")]
    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHP;
    [SerializeField] private GameObject UiCheckBox;

    [SerializeField] private Image expImg;
    [SerializeField] public TMP_Text exptext;
    [Header("Animator")]
    [SerializeField] private Animator animator;
    private GameObject hpBG;
    private Image hpImg;
    [SerializeField]private PlayerStat stats;

    public void CheckBox(bool isFind)
    {
        UiCheckBox.SetActive(isFind);
    }

    public void CreateHp()
    {
        hpBG = Instantiate(prefabHP, isParent);
        hpImg = hpBG.transform.GetChild(0).GetComponent<Image>();
    }


    public void HPbar(Vector3 pos)
    {
        Vector3 targetPos = Camera.main.WorldToScreenPoint(pos);
        targetPos.y += 150f;
        hpBG.transform.position = targetPos;
    }

    public void HpUpdate(int hp, int maxHp)
    {
        float ratio = maxHp > 0 ? Mathf.Clamp01((float)hp / maxHp) : 0f;
        hpImg.rectTransform.sizeDelta = new Vector2(200f * ratio, 30f);
    }

    public void ExpUpdata()
    {
        exptext.text = $"LV.{PlayerStat.Level} {PlayerStat.Exp / PlayerStat.MaxExp * 100f}% ({PlayerStat.Exp} / {PlayerStat.MaxExp})";
        expImg.rectTransform.sizeDelta = new Vector2(1920f * (PlayerStat.Exp / PlayerStat.MaxExp), 20f);
    }

    public void Area(Vector3 pos)
    {
        area.SetActive(true);
        area.transform.position = pos;
    }

    private void OnEnable()
    {
        GameEvents.ChangeEXPUpdate += ExpUpdata;
    }

    private void OnDisable()
    {
        GameEvents.ChangeEXPUpdate -= ExpUpdata;
    }
}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterAttackState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterAttackState : IState
{
    Monster monster;
    public MonsterAttackState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("공격중");
        Vector3 posAttack = monster.transform.position + monster.transform.forward * 1f;
        posAttack.y += 0.7f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.2f, 1.4f, 0.8f), monster.transform.rotation, LayerMask.GetMask("Player"));
        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(monster.data.Mdamage);
                monster.MonsterAni.SetTrigger("Attack");
                DamageFontManager.Instance.CreateText(monster.data.Mdamage, tar.transform.position);
                monster.attackCool.Start();
                break;
            }
        }
        monster.ChangeState(MonsterState.idle);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterDeadState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterDeadState : IState
{
    Monster monster;
    float time;
    public MonsterDeadState (Monster monster)
    {
        this.monster = monster;
        time = 2f;
    }

    public void Enter()
    {
        monster.agent.ResetPath();
        monster.MonsterAni.SetFloat("AnimSpeed", 1f);
        monster.MonsterAni.SetTrigger("Dead");
        monster.MonsterAni.SetBool("IsDead", true);
        monster.View.HpUpdate(monster.Model.HP, monster.Model.MaxHP);
        monster.Invoke("OnDead", 2f);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        time -= Time.deltaTime;
    }

}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterHitState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterHitState : IState
{
    Monster monster;
    public MonsterHitState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        monster.View.HpUpdate(monster.Model.HP, monster.Model.MaxHP);
        monster.MonsterAni.SetTrigger("Hit");
        Debug.Log($"남은 체력 : {monster.Model.HP}");
        monster.ChangeState(MonsterState.idle);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        
    }
}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterIdleState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterIdleState : IState
{
    Monster monster;

    public MonsterIdleState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("대기중");
        monster.MonsterAni.SetTrigger("Idle");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (monster.IsFind && monster.Model.TargetDis <= monster.data.Range && monster.IsLive)
        {
            monster.transform.LookAt(monster.Target);
            if (monster.attackCool.IsReady)
            {
                monster.ChangeState(MonsterState.attack);
            }
        }
        if (monster.IsFind && monster.Model.TargetDis > monster.data.Range)
            monster.ChangeState(MonsterState.chase);
    }
}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterMoveState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterMoveState : IState
{
    Monster monster;

    Vector3 startPos;
    float distan;
    public MonsterMoveState(Monster monster)
    {
        this.monster = monster;
    }
    public void Enter()
    {
        monster.agent.speed = monster.data.MoveSpeed;
        Debug.Log("목표물로 가는중");
        monster.MonsterAni.SetTrigger("Walk");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        monster.agent.SetDestination(monster.Target.position);
        
        if(monster.Model.TargetDis <= 1.5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState(MonsterState.idle);
        }
        

        if (!monster.IsFind && monster.StartDis >= monster.data.SpawnRange)
        {
            monster.agent.ResetPath();
            monster.ChangeState(MonsterState.idle);
        }
    }
}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterPatrolState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterPatrolState : IState
{
    Monster monster;

    public MonsterPatrolState (Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("복귀중");
        monster.MonsterAni.SetTrigger("Run");
        monster.agent.speed += (int)monster.agent.speed << 2;
        monster.agent.SetDestination(monster.Model.StartPos);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (monster.agent.remainingDistance < 0.5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState(MonsterState.idle);
        }
        else if (monster.IsFind && monster.Model.TargetDis >= monster.data.Range)
        {
            monster.ChangeState(MonsterState.chase);
        }
    }

}
```

## Assets/0.Script/0.Game/2.Monster/Monster State/MonsterReviveState.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterReviveState : IState
{
    private Monster monster;

    public MonsterReviveState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        monster.IsLive = true;
        monster.MonsterAni.SetTrigger("Idle");

        monster.Model.HP = monster.data.Hp;

        ObjectPoolManager.Instance.ReturnObject(PoolType.Enemy, monster.gameObject);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
```

## Assets/0.Script/0.Game/2.Monster/Monster.cs

```csharp
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
    idle,
    chase,
    attack,
    patrol,
    revive,
    dead,
    hit
}
public class Monster : MonoBehaviour, IDamageable
{
    public Transform Target {  get; set; }

    private IState currentState;
    private MonsterState currentKey;

    public bool IsFind { get; private set; }
    public Cooldown attackCool { get; set; } = new Cooldown(5f);

    public Animator MonsterAni {  get; set; }

    public MonsterView View { get; set; }
    public MonsterModel Model { get; set; }

    public NavMeshAgent agent {  get; set; }
    public MonsterData data { get; set; }
    public bool IsLive { get; set; } = true;
    public MonsterState PrevState { get; private set; }
    public Dictionary<MonsterState, IState> States { get; private set; }
    public float StartDis { get; private set; }

    public void Init(MonsterData data)
    {
        this.data = data;
    }

    void Awake()
    {
        Model = new MonsterModel
            (
                data.Hp,
                transform.position
            );
        MonsterAni = GetComponent<Animator>();
        View = GetComponent<MonsterView>();
        agent = GetComponent<NavMeshAgent>();
        SettingState();
    }

    void Start()
    {
        Model.HP = Model.MaxHP = data.Hp;

        View.CreateHp(transform.position);
        ChangeState(MonsterState.idle);
    }
    void Update()
    {
        if (Target == null || agent == null)
            return;

        View.HPbar(transform.position);
        ScanTarget();
       
        attackCool.Tick(Time.deltaTime);

        StartDis = Vector3.Distance(transform.position, Model.StartPos);
        Model.TargetDis = Vector3.Distance(transform.position, Target.position);

        currentState?.Tick();
    }

    public void ChangeState(MonsterState state)
    {
        PrevState = currentKey;
        currentState?.Exit();
        currentState = States[state];
        currentKey = state;
        currentState?.Enter();
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
        GameEvents.RaiseKillChange(data, data.GetGold, data.GetExp);
        GameEvents.RaiseChangeDeadMonster();
        View.DeleteHp();
        Invoke("ReSpawn", 1f);
    }
    public void ReSpawn()
    {
        ChangeState(MonsterState.revive);
    }
    public void TakeDamage(int damage)
    {
        if (Model.HP <= damage)
        {
            if (Model.HP != 0)
            {
                Model.HP = 0;
                ChangeState(MonsterState.dead);
            }
            else
                return;
        }
        else if (Model.HP != 0)
        {
            Model.HP -= damage;
            ChangeState(MonsterState.hit);
            DamageFontManager.Instance.CreateText(damage, transform.position);
        }
        else
            return;
    }

    private void SettingState()
    {
        States = new Dictionary<MonsterState, IState>()
        {
            { MonsterState.attack, new MonsterAttackState(this) },
            { MonsterState.idle, new MonsterIdleState(this) },
            { MonsterState.chase, new MonsterMoveState(this) },
            { MonsterState.patrol, new MonsterPatrolState(this) },
            { MonsterState.revive, new MonsterReviveState(this) },
            { MonsterState.dead, new MonsterDeadState(this) },
            { MonsterState.hit, new MonsterHitState(this) }
        };
    }

    public void ScanTarget()
    {
        Vector3 pos = transform.position;
        pos.y += 1f;
        Collider[] targetScan = Physics.OverlapSphere(pos, data.ScanSize);
        foreach (var tar in targetScan)
        {
            IsFind = false;
            if (tar.CompareTag("Player"))
            {
                IsFind = true;
                break;
            }
        }
    }

    private void ChaseTarget(Transform target)
    {
        Target = target; 
    }

    private void OnEnable()
    {
        GameEvents.ChaseMonster += ChaseTarget;
    }

    private void OnDisable()
    {
        GameEvents.ChaseMonster -= ChaseTarget;
    }

}
```

## Assets/0.Script/0.Game/2.Monster/MonsterModel.cs

```csharp
using UnityEngine;

[System.Serializable]
public class MonsterModel
{
    public int HP { get; set; }

    public int MaxHP { get; set; }

    public float TargetDis {  get; set; }

    public Vector3 StartPos { get; set; }
    
    public int Damage { get; set; }
    public MonsterModel(int Hp, Vector3 startPos)
    {
        this.HP = Hp;
        this.MaxHP = Hp;
        this.StartPos = startPos;
    }
}
```

## Assets/0.Script/0.Game/2.Monster/MonsterView.cs

```csharp
using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour
{

    private GameObject hpBG;
    private Image hpImg;

    public void CreateHp(Vector3 pos)
    {
        ObjectPoolManager.Instance.CreateObj(PoolType.HpBar, 1);
        hpBG = ObjectPoolManager.Instance.GetObject(PoolType.HpBar);
        hpImg = hpBG.transform.GetChild(0).GetComponent<Image>();
        HPbar(pos);
        hpBG.SetActive(true);
    }
    public void DeleteHp()
    {
        Destroy(hpBG);
    }

    public void HPbar(Vector3 pos)
    {
        Vector3 targetPos = Camera.main.WorldToScreenPoint(pos);
        targetPos.y += 100f;
        hpBG.transform.position = targetPos;
    }

    public void HpUpdate(int HP, int maxHP)
    {
        hpImg.rectTransform.sizeDelta = new Vector2(hpImg.rectTransform.sizeDelta.x * ((float)HP / maxHP), hpImg.rectTransform.sizeDelta.y);
    }

    public void HPbarDelete(bool isLive)
    {
        hpBG.SetActive(isLive);
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossAreaAttack.cs

```csharp
using UnityEngine;

public class BossAreaAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    GameObject AreaAttack;
    GameObject chargeview;

    public BossAreaAttack(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        AreaAttack = boss.view.AreaAttack(boss.target.position);
        chargeview = AreaAttack.transform.GetChild(0).gameObject;
        Debug.Log(AreaAttack.transform.localScale);
        boss.transform.LookAt(AreaAttack.transform);
        time = 0f;
        timeDuration = 2f;
    }

    public void Exit()
    {
        boss.AreaCool.Start();
    }

    public void Tick()
    {
        time += Time.deltaTime;
        float progress = time / timeDuration;

        chargeview.transform.localScale = Vector3.Lerp(Vector3.zero, AreaAttack.transform.localScale/5, progress);
        

        if (progress >= 1f)
        {
            Collider[] attack = Physics.OverlapSphere(AreaAttack.transform.position, boss.stats.AreaRadius);
            VFXManager.Instance.Show(VFXtype.AreaAttack, AreaAttack.transform);
            foreach (Collider atk in attack)
            {
                if(atk.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    if (atk.CompareTag("Player"))
                    {
                        damage.TakeDamage(boss.data.Mdamage + boss.stats.AreaDamage);
                    }
                }
            }

            boss.ChangeState(BossState.SelectAttack);
            boss.view.DestroyObj(AreaAttack);
        }
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossChargeAttack.cs

```csharp
using Unity.VisualScripting;
using UnityEngine;

public class BossChargeAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    private GameObject ChargeAttack;
    private GameObject ChargeView;
    private Vector3 pos;

    public BossChargeAttack(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        boss.transform.LookAt(boss.target);
        ChargeAttack = boss.view.ChargeAttack();
        ChargeView = ChargeAttack.transform.GetChild(0).gameObject;
        SpriteRenderer sr = ChargeAttack.GetComponentInChildren<SpriteRenderer>();
        pos = sr.transform.TransformPoint(sr.sprite.bounds.center);
        time = 0f;
        timeDuration = 3f;
    }

    public void Exit()
    {
        if(ChargeAttack != null)
            boss.view.DestroyObj(ChargeAttack);
        boss.ChargeCool.Start();
    }

    public void Tick()
    {
        time += Time.deltaTime;
        float progress = time / timeDuration;

        ChargeView.transform.localScale = new Vector3(1f, Mathf.Lerp(0f, 1f, progress), 1f);

        if(progress >= 1f)
        {
            Collider[] attack = Physics.OverlapBox(pos, new Vector3(5f, 2f, 7f) * 0.5f, boss.transform.rotation);
            VFXManager.Instance.Show(VFXtype.ChargeAttack, ChargeAttack.transform);
            foreach (Collider atk in attack)
            {
                if(atk.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    if (atk.CompareTag("Player"))
                    {
                        damage.TakeDamage(boss.data.Mdamage + boss.stats.ChargeDamage);
                    }
                }
            }
            boss.view.DestroyObj(ChargeAttack);
            boss.ChangeState(BossState.SelectAttack);
        }
        
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossChaseState.cs

```csharp
using UnityEngine;

public class BossChaseState : IState
{
    Boss boss;

    public BossChaseState (Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        boss.agent.SetDestination(boss.target.position);

        if (boss.TargetDis < 1f)
        {
            boss.agent.ResetPath();
            boss.ChangeState(BossState.Idle);
        }
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossDeadState.cs

```csharp
using UnityEngine;

public class BossDeadState : IState
{
    Boss boss;

    public BossDeadState(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossIdleState.cs

```csharp
using UnityEngine;

public class BossIdleState : IState
{
    Boss boss;
    public BossIdleState(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        boss.bossAni.SetTrigger("Idle");
        Debug.Log("가만히");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if(boss.IsFind)
        {
            boss.ChangeState(BossState.Chase);
        }
        if(boss.TargetDis < 1.5f)
        {
            boss.ChangeState(BossState.SelectAttack);
        }
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossNormalAttackState.cs

```csharp
using UnityEngine;

public class BossNormalAttackState : IState
{
    Boss boss;
    private float time;
    private float timeDuration;
    public BossNormalAttackState(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        time = 0;
        timeDuration = 1f;
        boss.NomalCool.Start();
        boss.agent.SetDestination(boss.target.position);
        boss.bossAni.SetTrigger("Attack");
        foreach(var tar in boss.stats.TargetCheck)
        {
            if(tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                if (tar.CompareTag("Player"))
                {
                    damage.TakeDamage(boss.data.Mdamage);
                    DamageFontManager.Instance.CreateText(boss.data.Mdamage, tar.transform.position);
                }
            }
        }
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        time += Time.deltaTime;

        if(time >= timeDuration)
        {
            boss.ChangeState(boss.PrevState);
            boss.agent.ResetPath();
        }
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossPhaseChangeState.cs

```csharp
using UnityEngine;

public class BossPhaseChangeState : IState
{
    Boss boss;

    public BossPhaseChangeState(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        Debug.Log("페이즈 전환");
        boss.stats.Phase = 2;
        boss.ChangeState(boss.PrevState);
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss State/BossSelectAttack.cs

```csharp
using UnityEngine;

public class BossSelectAttack : IState
{
    Boss boss;
    public BossSelectAttack(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        boss.bossAni.SetTrigger("Idle");
        Debug.Log("공격 선택");
        Vector3 posAttack = boss.transform.position + boss.transform.forward * 1.5f;
        posAttack.y += 1f;
        boss.stats.TargetCheck = Physics.OverlapBox(posAttack, new Vector3(2f, 1.4f, 1.5f), boss.transform.rotation, LayerMask.GetMask("Player"));
        
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        boss.transform.LookAt(boss.target);
        if (boss.NomalCool.IsReady && boss.ChargeCool.IsReady && boss.AreaCool.IsReady)
        {
            if (boss.stats.Phase == 1)
            {
                boss.ChangeState((BossState)Random.Range(3, 5));
            }
            else if (boss.stats.Phase == 2)
            {
                boss.ChangeState((BossState)Random.Range(3, 6));
            }
        }
        if(boss.TargetDis > 2f)
        {
            boss.ChangeState(BossState.Idle);
        }
        
    }
}
```

## Assets/0.Script/0.Game/3.Boss/Boss.cs

```csharp
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Idle,
    Chase,
    SelectAttack,
    NormalAttack,
    ChargeAttack,
    AreaAttack,
    PhaseChange,
    Dead
}
public class Boss : MonoBehaviour, IDamageable
{
    IState currentState;
    BossState currentKey;
    public BossState PrevState { get; private set; }
    public Dictionary<BossState, IState> States { get; private set; }


    public Transform target;
    
    public BossView view { get; set; }
    public MonsterData data;
    public BossStat stats;
    public Animator bossAni;

    public Cooldown NomalCool {  get; private set; }
    public Cooldown ChargeCool { get; private set; }
    public Cooldown AreaCool { get; private set; }

    public NavMeshAgent agent {  get; set; }
    public float TargetDis {  get; private set; }
    public bool IsFind {get; private set;}

    Vector3 boxSize = new Vector3(5f, 1f, 5f);
    Vector3 pos = new Vector3(4f, 1f, 6f);
    void Start()
    {
        view = GetComponent<BossView>();
        agent = GetComponent<NavMeshAgent>();
        SettingState();
        SettingCool();
        stats.HpOne = stats.HpTwo = data.Hp / 2;
        stats.MaxHp = data.Hp;
        view.CreateHp(stats);
        ChangeState(BossState.Idle);
    }

    void Update()
    {
        ScanTarget();
        NomalCool.Tick(Time.deltaTime);
        ChargeCool.Tick(Time.deltaTime);
        AreaCool.Tick(Time.deltaTime);
        TargetDis = Vector2.Distance(transform.position, target.position);
        if(stats.HpOne == 0 && stats.Phase == 1)
        {
            ChangeState(BossState.PhaseChange);
        }
        currentState.Tick();
    }
    public void ChangeState(BossState state)
    {
        PrevState = currentKey;
        currentState?.Exit();
        currentState = States[state];
        currentKey = state;
        currentState?.Enter();
    }

    private void SettingState()
    {
        States = new Dictionary<BossState, IState>()
        {
            { BossState.Idle, new BossIdleState(this) },
            { BossState.Chase, new BossChaseState(this) },

            { BossState.SelectAttack, new BossSelectAttack(this) },
            { BossState.NormalAttack, new BossNormalAttackState(this) },
            { BossState.ChargeAttack, new BossChargeAttack(this) },
            { BossState.AreaAttack, new BossAreaAttack(this) },

            { BossState.PhaseChange, new BossPhaseChangeState(this) },
            { BossState.Dead, new BossDeadState(this) }
        };
    }

    private void SettingCool()
    {
        NomalCool = new Cooldown(5f);
        ChargeCool = new Cooldown(7f);
        AreaCool = new Cooldown(8f);
    }

    private void ScanTarget()
    {
        Collider[] targetScan = Physics.OverlapBox(pos, boxSize);
        foreach (var tar in targetScan)
        {
            IsFind = false;
            if (tar.CompareTag("Player"))
            {
                IsFind = true;
                break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("보스 데미지 피격");
        if(stats.HpOne != 0)
        {
            stats.HpOne -= damage;
            if(stats.HpOne < 0)
            {
                stats.HpTwo += stats.HpOne;
                stats.HpOne = 0;
            }
        }
        else if(stats.HpOne == 0)
        {
            stats.HpTwo -= damage;
        }
        view.UpdateHp(stats);
    }
}
```

## Assets/0.Script/0.Game/3.Boss/BossStat.cs

```csharp
using UnityEngine;

public class BossStat : MonoBehaviour
{
    private int hpOne;
    private int hpTwo;
    private int maxHp;
    private int phase = 1;
    private int chargeDamage = 10;
    private int areaDamage = 5;

    private Vector3 chargeArea = new Vector3(3f, 1f, 5f);
    private float areaRadius = 2.5f;

    public int HpOne { get { return hpOne; } set { hpOne = value; } }
    public int HpTwo { get { return hpTwo; } set { hpTwo = value; } }
    public int MaxHp { get {  return maxHp; } set { maxHp = value; } }
    public int Phase { get { return phase; } set { phase = value; } } 
    public int AreaDamage { get { return areaDamage; } set { areaDamage = value; } }
    public int ChargeDamage { get { return chargeDamage; } set { chargeDamage = value; } }

    public Vector3 ChargeArea { get { return chargeArea; } set { chargeArea = value; } }
    public float AreaRadius { get { return areaRadius; } set { areaRadius = value; } }
    

    public Collider[] TargetCheck { get; set; }

}
```

## Assets/0.Script/0.Game/3.Boss/BossView.cs

```csharp
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour
{

    [SerializeField] private Transform isParent;
    [SerializeField] private GameObject prefabHp;
    [SerializeField] private GameObject areaPrefab;
    [SerializeField] private GameObject chargePrefab;

    private GameObject hpBG;
    private Image hpImgOne;
    private Image hpImgTwo;
    private TMP_Text phaseTxt;

    public void CreateHp(BossStat stats)
    {
        hpBG = Instantiate(prefabHp, isParent);
        hpImgOne = hpBG.transform.GetChild(1).GetComponent<Image>();
        hpImgTwo = hpBG.transform.GetChild(0).GetComponent<Image>();
        phaseTxt = hpBG.transform.GetChild(2).GetComponent<TMP_Text>();
        RectTransform rt = hpBG.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = new Vector2(0f, -50f);
    }

    public void UpdateHp(BossStat stats)
    {
        hpImgOne.rectTransform.sizeDelta = new Vector2(hpImgOne.rectTransform.sizeDelta.x * ((float)stats.HpOne / (stats.MaxHp / 2)), 20f);
        hpImgTwo.rectTransform.sizeDelta = new Vector2(hpImgTwo.rectTransform.sizeDelta.x * ((float)stats.HpTwo / (stats.MaxHp / 2)), 20f);
        if(stats.HpOne == 0)
        {
            phaseTxt.text = "× 1";
        }
        else if(stats.HpTwo == 0)
        {
            phaseTxt.text = "× 0";
        }
    }

    public GameObject AreaAttack(Vector3 targetPos)
    {
        GameObject areaAttack = Instantiate(areaPrefab, targetPos, areaPrefab.transform.rotation);

        return areaAttack;
    }
    
    public GameObject ChargeAttack()
    {
        Vector3 spawnArea = transform.position + transform.forward * 1.5f;
        Quaternion rotaitionArea = Quaternion.Euler(90f, transform.eulerAngles.y, 0f);
        GameObject chargeAttack = Instantiate(chargePrefab, spawnArea, rotaitionArea);

        return chargeAttack;
    }

    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }

}
```

## Assets/0.Script/0.Game/4.Entity/Box.cs

```csharp
using UnityEngine;

public class Box : MonoBehaviour, IInterectable
{
    public void Interact()
    {
        Debug.Log("상자 상호작용");
    }
}
```

## Assets/0.Script/0.Game/4.Entity/Door.cs

```csharp
using UnityEngine;

public class Door : MonoBehaviour, IInterectable
{
    [SerializeField] private SceneType nextScene;
    public void Interact()
    {
        SceneLoader.Instance.LoadingScene(nextScene);
    }
    
}
```

## Assets/0.Script/0.Game/4.Entity/NPC/NPC.cs

```csharp
using UnityEngine;

public class NPC : MonoBehaviour, IInterectable
{
    public void Interact()
    {
        Debug.Log("대화");
    }
}
```

## Assets/0.Script/0.Game/4.Entity/NPC/QuestNPC.cs

```csharp
using TMPro;
using UnityEngine;

public class QuestNPC : MonoBehaviour, IInterectable
{
    [SerializeField] private QuestData questData;
    [SerializeField] private QuestManager questManager;

    [SerializeField] private GameObject questUI;
    [SerializeField] private TMP_Text questTitle;
    [SerializeField] private TMP_Text questInfo;


    private void Awake()
    {
        questUI.SetActive(false);

    }

    public string GetInterfaction()
    {
        QuestProgress quest = questManager.GetQuest(questData.questId);
        if (quest == null)
            return "[F] 퀘스트받기";
        if (quest.State == QuestState.canComplete)
            return "[F] 퀘스트 완료";
        if (quest.State == QuestState.Inprogress)
            return $"[F]진행도:{quest.CurrentCount}/{questData.requiredCount}";

        return "[F] 대화하기";
    }
    public void Interact()
    {
        QuestProgress quest = questManager.GetQuest(questData.questId);
        if (quest == null)
        {
            questTitle.text = $"{questData.questTitle}";
            questInfo.text = $"{questData.description} \n 보상 : {questData.rewardItem.name} 경험치 : {questData.rewardExp}";
            questUI.SetActive(true);
            return;
        }
        if(quest.State == QuestState.canComplete)
        {
            questManager.CompleteQuest(questData.questId);
            return;
        }
        Debug.Log("용무 없음");
    }

    public void Accept()
    {
        questManager.AcceptQuest(questData);
        questUI.SetActive(!questUI.activeSelf);
    }

}
```

## Assets/0.Script/0.Game/Camera/ChaseCamera.cs

```csharp
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float smoothTime = 0.2f;
    private Vector3 velocity;
    private Vector3 offset = new Vector3(0, 7, -5);

    void Start()
    {
        if (target == null)
            return;
        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref velocity,
            smoothTime
        );

    }

}
```

## Assets/0.Script/0.Game/Dungeon/CombatSensor.cs

```csharp
using System.Collections.Generic;
using UnityEngine;

public class CombatSensor : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private Transform combat;
    [Header("Block")]
    [SerializeField] private GameObject blockWall;

    private bool isUse = false;

    private void OnEnable()
    {
        GameEvents.ClearCombat += IsClear;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !isUse)
        {
            isUse = true;
            GameEvents.RaiseChangeEnPlayer(combat);
            blockWall.SetActive(true);
        }
    }

    private void IsClear()
    {
        if (transform.gameObject.activeInHierarchy)
        {
            blockWall.SetActive(false);
            transform.gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

    private void OnDisable()
    {
        GameEvents.ClearCombat -= IsClear;
    }
}
```

## Assets/0.Script/0.Game/Dungeon/MonsterSpawn.cs

```csharp
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private Transform target;
    [SerializeField] private MonsterData data;
    void Start()
    {
        ObjectPoolManager.Instance.CreateObj(PoolType.Enemy, 10);
    }

    private void OnEnable()
    {
        GameEvents.EnPlayer += SpawnMonster;

    }
    private void OnDisable()
    {
        GameEvents.EnPlayer -= SpawnMonster;
    }



    public void SpawnMonster(Transform combat)
    {
        for(int i = 0; i < spawnCount; ++i)
        {
            GameObject monster = ObjectPoolManager.Instance.GetObject(PoolType.Enemy);
            Monster mon = monster.GetComponent<Monster>();
            mon.Target = target;
            mon.data = data;
            DungeonManager.Instance.Add(monster);
            monster.transform.position = RandomPostion(combat);
            monster.SetActive(true);
        }
    }

    public Vector3 RandomPostion(Transform combat)
    {
        Vector2 pos = Random.insideUnitCircle * 13f;

        return new Vector3(combat.position.x + pos.x, 1f, combat.position.z + pos.y);
    }
}
```

## Assets/0.Script/0.Game/Dungeon/ObjectPoolManganer.cs

```csharp
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public enum PoolType
{
    Enemy,
    equip,
    potion,
    HpBar
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private Dictionary<PoolType, Queue<GameObject>> pool;
    [SerializeField] private Transform uiParent;

    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Hpbar;

    private void Awake()
    {
        SettingPool();
    }

    private void SettingPool()
    {
        pool = new Dictionary<PoolType, Queue<GameObject>>()
        {
            {PoolType.Enemy, new Queue<GameObject>() },
            {PoolType.equip, new Queue<GameObject>() },
            {PoolType.potion, new Queue<GameObject>() }
        };
    }

    public void CreateObj(PoolType type, int size)
    {
        GameObject spawnObj = null;
        switch (type)
        {
            case PoolType.Enemy:
                spawnObj = Enemy;
                break;
            case PoolType.equip:
                break;
            case PoolType.potion:
                break;
            case PoolType.HpBar:
                spawnObj = Hpbar;
                break;
        }    

        for(int i = 0; i < size; ++i)
        {
            GameObject obj = Instantiate(spawnObj, transform);

            obj.SetActive(false);

            pool[type].Enqueue(obj);
        }
    }

    public GameObject GetObject(PoolType type)
    {
        if (pool[type].Count <= 0)
        {
            switch (type)
            {
                case PoolType.Enemy:
                    return Instantiate(Enemy, transform);
                case PoolType.equip:
                    break;
                case PoolType.potion:
                    break;
                case PoolType.HpBar:
                    return Instantiate(Hpbar, uiParent);
            }
        }
        GameObject obj = pool[type].Dequeue();
        return obj;
    }

    public void ReturnObject(PoolType type, GameObject obj)
    {
        obj.SetActive(false);

        pool[type].Enqueue(obj);
    }
}
```

## Assets/0.Script/0.Game/IDamageable.cs

```csharp
using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(int damage);
}
```

## Assets/0.Script/0.Game/MonsterTest/MonsterWolf.cs

```csharp
using UnityEngine;

public class MonsterWolf : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```

## Assets/0.Script/1.UI/DamageFontManager.cs

```csharp
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageFontManager : Singleton<DamageFontManager>
{
    [SerializeField] TMP_Text damageTxt;

    public float jumpHeight = 2f;
    public float duration = 0.8f;

    public void CreateText(int damage, Vector3 pos)
    {
        Vector3 uiPos = Camera.main.WorldToScreenPoint(pos);
        TMP_Text txt = Instantiate(damageTxt, uiPos, Quaternion.identity, transform);
        txt.text = $"{damage}";
        Vector3 startPos = uiPos;
        Sequence seq = DOTween.Sequence();

        seq.Append(txt.rectTransform.DOMoveY(startPos.y + jumpHeight, duration * 0.45f)
            .SetEase(Ease.OutQuad));
        seq.Append(txt.rectTransform.DOMoveY(startPos.y, duration * 0.55f)
            .SetEase(Ease.InQuad));
        seq.Append(txt.rectTransform.DOScale(Vector3.zero, 0.15f)
            .SetEase(Ease.InBack));
        seq.OnComplete(() =>
        {
            Destroy(txt.gameObject);
        });
    }
}
```

## Assets/0.Script/1.UI/Inventory/EquipmentSlot.cs

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject txtBGObj;
    [SerializeField]
    private Image iconImg;
    [SerializeField]
    private TMP_Text itemNameTxt;
    [SerializeField]
    private EquipType type;

    private bool isExit;

    public ItemScriptable Data { get; set; }
    private InventoryItem moveItem;
    private RectTransform moveItemRectTran;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIConstroller.Instance.moveItem.Data.itemType == ItemType.Equip)
        {
            if (UIConstroller.Instance.moveItem.Data.equipType == type)
            {
                UIConstroller.Instance.equipSystem.SelectSlot = this;
            }
            else
            {
                Debug.Log($"{type}만 가능합니다");
                return;
            }
        }
        else
        {
            Debug.Log("장비만 가능합니다.");
            return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIConstroller.Instance.equipSystem.SelectSlot = null;

        moveItem = null;
    }
    public bool IsInPoint(Vector2 postion)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(iconImg.rectTransform, postion);
    }

    public void Equip()
    {
        this.moveItem = UIConstroller.Instance.moveItem;
        Data = UIConstroller.Instance.moveItem.Data;

        iconImg.sprite = moveItem.Data.Icon;
        itemNameTxt.text = moveItem.Data.ItemName;
        iconImg.gameObject.SetActive(true);
        txtBGObj.SetActive(true);
    }
    public void UnEquip()
    {
        UIConstroller.Instance.moveItem.EquipImg.gameObject.SetActive(false);
        UIConstroller.Instance.equipSystem.SelectSlot = null;
        iconImg.gameObject.SetActive(false);
        txtBGObj.SetActive(false);
        moveItem = null;
        Data = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        moveItemRectTran.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.Data = Data;

        this.moveItem = UIConstroller.Instance.moveItem;

        moveItemRectTran = UIConstroller.Instance.moveItem.GetComponent<RectTransform>();
        moveItemRectTran.position = eventData.position;
        UIConstroller.Instance.moveItem.gameObject.SetActive(true);

        UIConstroller.Instance.moveItem.Setting();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.gameObject.SetActive(false);
        if (!IsInPoint(eventData.position))
        {
            UnEquip();
        }
    }
}
```

## Assets/0.Script/1.UI/Inventory/EquipSystem.cs

```csharp
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class EquipSystem : Singleton<EquipSystem>
{
    public EquipmentSlot[] slots;

    public EquipmentSlot SelectSlot { get; set; }
    
    public int EquipTotalDamage()
    {
        int totalDamage = 0;
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalDamage += slot.Data.Damage;
            }
        }
        return totalDamage;
    }

    public int EquipTotalHP()
    {
        int totalHP = 0;
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null)
            {
                totalHP += slot.Data.HP;
            }
        }
        return totalHP;
    }

    public int EquipTotalDefence()
    {
        int totalDefence = 0;
        foreach(EquipmentSlot slot in slots)
        {
            if(slot.Data != null && slot.Data.Defence != 0)
            {
                totalDefence += slot.Data.Defence;
            }
        }
        return totalDefence;
    }

    public float EquipTotalSpeed()
    {
        float totalSpeed = 0;

        foreach (EquipmentSlot slot in slots)
        {
            if (slot.Data != null && slot.Data.Speed != 0)
            {
                totalSpeed += slot.Data.Speed;
            }
        }

        return totalSpeed;
    }

    public EquipData[] GetEquipData()
    {
        EquipData[] data = new EquipData[slots.Length];

        for(int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].Data == null)
            {
                data[i] = new EquipData
                {
                    id = 1234
                };
                continue;
            }
            data[i] = new EquipData
            {
                id = slots[i].Data.ItemID
            };
        }

        return data;
    }
    public void LoadEquip(EquipData[] data)
    {
        foreach(var equipData in data)
        {
        }
    }
}
```

## Assets/0.Script/1.UI/Inventory/Inventory.cs

```csharp
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private InventoryItem invenItem;

    [SerializeField] private ItemScriptable[] itemDatas;

    [SerializeField] private TMP_Text goldAmount;

    public int Gold { get; set; }

    private List<InventoryItem> items = new();
    private Image background;
    public Transform Inven => parent;

    private void Start()
    {
        itemDatas = Resources.LoadAll<ItemScriptable>("ItemData");
        background = parent.GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            int rand = Random.Range(0, itemDatas.Length);
            CreateItem(itemDatas[rand], 1);
        }
    }

    private void OnEnable()
    {
        GameEvents.PlayerKill += (data, gold, exp) => GoldAmount(gold);
        GameEvents.ChangeCurrency += (gold, exp) => GoldAmount(gold);
    }

    private void OnDisable()
    {
        GameEvents.PlayerKill -= (data, gold, exp) => GoldAmount(gold);
        GameEvents.ChangeCurrency -= (gold, exp) => GoldAmount(gold);
    }

    // 매개변수 int 추가
    // 반환 자료형 int  변경
    public int CreateItem(ItemScriptable item, uint count)
    {
        items.RemoveAll(item => item == null);
        if (items.Count != 0)
        {
            foreach (var i in items)
            {
                if (i.Data == item)
                {
                    if (i.Amount < item.MaxStack)
                    {
                        i.SetCount(count);
                        return 0;
                    }
                }
            }
        }
        
        InventoryItem createItem = Instantiate(invenItem, parent);
        createItem.Init(item);
        createItem.Setting();
        createItem.SetCount(count);
        items.Add(createItem);
        return 1;
    }

    public InvenData[] GetInvenDatas()
    {
        InvenData[] data = new InvenData[items.Count];
        for(int i = 0; i < items.Count; ++i)
        {
            if (items == null)
            {
                data[i] = new InvenData
                {
                    id = 1234,
                    stack = 0
                };
                continue;
            }
            data[i] = new InvenData
            {
                id = items[i].Data.ItemID,
                stack = items[i].Amount
            };
        }

        return data;
    }

    public void GoldAmount(int addGold)
    {
        Gold += addGold;
        goldAmount.text = $"{Gold}";
    }

    public void LoadInventory(InvenData[] data)
    {
        foreach(var saveData in data)
        {
            for(int i = 0; i < itemDatas.Length; ++i)
            {
                if (itemDatas[i].ItemID == saveData.id)
                    CreateItem(itemDatas[i], saveData.stack);
            }
        }
    }
}
```

## Assets/0.Script/1.UI/Inventory/InventoryItem.cs

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField]
    private Image iconImg;
    
    [SerializeField]
    private TMP_Text nameTxt;

    [SerializeField]
    private Image equipImg;

    [SerializeField]
    private TMP_Text countTxt;

    [SerializeField]
    private Image back;

    private uint count;

    private RectTransform moveItemRectTran;

    // 공유 변수
    public Image EquipImg { get { return equipImg; } set { equipImg = value; } }
    public Image IconImg { get; set; }
    public ItemScriptable Data { get; set; }
    public uint Amount => count;
    public InventoryItem Init(ItemScriptable data)
    {
        this.Data = data;
        back = GetComponent<Image>();
        return this;
    }

    public void Setting()
    {
        back.sprite = Data.BackgroundIcon;
        iconImg.sprite = Data.Icon;
        nameTxt.text = Data.ItemName;
        EquipImg.gameObject.SetActive(false);
        if(Data.itemType != ItemType.Equip)
            countTxt.gameObject.SetActive(true);
    }

    public void SetCount(uint cnt)
    {
        if(Data.itemType == ItemType.Equip)
        {
            count = 1;
        }
        else
        {
            count += cnt;
            countTxt.text = $"{Amount} / {Data.MaxStack}";
        }
    }

    public void OnUse()
    {
        if(Data.itemType != ItemType.Equip)
        {
            count -= 1;
            SetCount(0);
            Debug.Log($"{Data.ItemName}");
            if(count == 0)
            {
                OnDelete();
            }
            
        }
        else
        {
            return;
        }
    }
    public void OnDelete()
    {
        Destroy(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(UIConstroller.Instance.equipSystem.SelectSlot != null)
        {
            UIConstroller.Instance.equipSystem.SelectSlot.Equip();
            EquipImg.gameObject.SetActive(!EquipImg.gameObject.activeSelf);
            Debug.Log($"{Data.ItemName} 장착");
        }
        UIConstroller.Instance.moveItem.gameObject.SetActive(false);
        moveItemRectTran = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveItemRectTran.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIConstroller.Instance.moveItem.Data = Data;

        moveItemRectTran = UIConstroller.Instance.moveItem.GetComponent<RectTransform>();
        moveItemRectTran.position = eventData.position;
        UIConstroller.Instance.moveItem.gameObject.SetActive(true);

        UIConstroller.Instance.moveItem.Setting();
    }
}
```

## Assets/0.Script/1.UI/Inventory/InventoryUI.cs

```csharp
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
```

## Assets/0.Script/1.UI/Manager/QuestManager.cs

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private readonly List<QuestProgress> activeQuests = new();
    public IReadOnlyList<QuestProgress> ActiveQuest => activeQuests;
    
    public bool AcceptQuest(QuestData quest)
    {
        if (quest == null)
            return false;
        if(HasQuest(quest.questId))
        {
            Debug.Log("이미 가지고 있는 퀘스트");
            return false;
        }
        QuestProgress prog = new QuestProgress(quest);
        activeQuests.Add(prog);
        GameEvents.PlayerKill += (data, gold, exp) => NotifyEnemyKilled(data);
        GameEvents.RaiseQuestChanged();
        Debug.Log($"Quest Accepted:{quest.questTitle}");
        return true;
    }

    public bool HasQuest(int questId)
    {
        foreach (QuestProgress quest in activeQuests)
        {
            if(quest.Data.questId == questId)
            {
                return true;
            }
        }
        return false;
    }

    public QuestProgress GetQuest(int questId)
    {
        foreach(QuestProgress quest in activeQuests)
        {
            if(quest.Data.questId == questId)
            {
                return quest;
            }
        }
        return null;
    }

    public void NotifyEnemyKilled(MonsterData data)
    {
        bool change = false;
        int enemyId = data.MonsterId;

        foreach(QuestProgress quest in activeQuests)
        {
            if (quest.State != QuestState.Inprogress)
                continue;
            if (quest.Data.questType != QuestType.Kill)
                continue;
            if (quest.Data.targetId != enemyId)
                continue;

            quest.Addprogress(1);
            change = true;
            Debug.Log($"{quest.Data.questTitle} : {quest.CurrentCount}/{quest.Data.requiredCount}");
        }

        if(change == true)
        {
            GameEvents.RaiseQuestChanged();
        }
    }

    public bool CompleteQuest(int questId)
    {
        QuestProgress quest = GetQuest(questId);
        if (quest == null)
            return false;
        if(quest.State != QuestState.canComplete)
            return false;
        QuestData data = quest.Data;
        if(data.rewardItem != null && data.rewardItemCount != 0)
        {
            int remaining = UIConstroller.Instance.inventory.CreateItem(data.rewardItem, (uint)data.rewardItemCount);
            if(remaining > 0)
            {
                Debug.Log("가방 공간 부족");
                return false;
            }
        }
        int gold = quest.Data.rewardGold;
        float exp = (float)quest.Data.rewardExp;
        GameEvents.RaiseChangeCurrency(gold, exp);
        
        quest.Complete();
        GameEvents.PlayerKill -= (data, gold, exp) => NotifyEnemyKilled(data);
        GameEvents.RaiseQuestChanged();
        Debug.Log($"Quest Complete {quest.Data.questTitle}");
        return false;
    }
}
```

## Assets/0.Script/1.UI/Manager/UIConstroller.cs

```csharp
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIConstroller : Singleton<UIConstroller>
{
    //public StatUI statUI;

    public Inventory inventory;
    public EquipSystem equipSystem;
    public InventoryItem moveItem;

    public QuestUi Quest;

    public PlayerStat playerStat;

    private GameObject inven;
    private GameObject equip;
    private GameObject quest;
    //private GameObject stat;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        inven = inventory.transform.GetChild(0).gameObject;
        equip = equipSystem.transform.GetChild(0).gameObject;
        quest = Quest.transform.GetChild(0).gameObject;
        //stat = statUI.transform.GetChild(0).gameObject;
    }

    void Start()
    {
        moveItem.gameObject.SetActive(false);
        quest.SetActive(false);
        inven.SetActive(false);
        equip.SetActive(false);
        //stat.SetActive(false);
    }
    void Update()
    {
        UIHandleInput();
    }
    private void UIHandleInput()
    {
        if (InputManger.Instance.input.UI.Inventory.WasPressedThisFrame())
        {
            inven.SetActive(!inven.activeSelf);
            if (quest.activeInHierarchy)
            {
                quest.SetActive(!inven.activeSelf);
            }
            IsInventory(inven.activeSelf);
        }
        if (InputManger.Instance.input.UI.Quest.WasPressedThisFrame())
        {
            quest.SetActive(!quest.activeSelf);
        }
        if (InputManger.Instance.input.UI.Equip.WasPressedThisFrame())
        {
            equip.SetActive(!equip.activeSelf);
        }
        if (InputManger.Instance.input.UI.Stat.WasPressedThisFrame())
        {
            //stat.SetActive(!stat.activeSelf);
        }
    }
    private void IsInventory(bool active)
    {
        if (active)
        {
            InputManger.Instance.input.Player.Disable();
        }
        else
        {
            InputManger.Instance.input.Player.Enable();
        }
    }

    public void OnSave()    
    {
        GameSaveData data = new GameSaveData();

        data.level = PlayerStat.Level;
        data.Hp = playerStat.Hp;
        data.exp = PlayerStat.Exp;
        data.gold = inventory.Gold;
        data.invendata = inventory.GetInvenDatas();
        data.equipDatas = equipSystem.GetEquipData();

        SaveManager.Instance.Save(data);
    }
    public void OnLoad()
    {
        GameSaveData data = SaveManager.Instance.Load();
        PlayerStat.Level = data.level;
        playerStat.Hp = data.Hp;
        GameEvents.RaiseChangeCurrency(data.gold, data.exp);

        inventory.LoadInventory(data.invendata);
        //equipSystem.LoadEquip(data.equipDatas);
    }
}
```

## Assets/0.Script/1.UI/PopUp/ListPopup.cs

```csharp
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListPopup : MonoBehaviour
{
    [SerializeField] private List<Image> listImage = new List<Image>();
    [SerializeField] private TMP_Text txt;

    private int showIndex = 0;

    private void Update()
    {
    }

    public void On(string str)
    {
        int index = showIndex;

        txt.text = str;
        listImage[index].color = new Color(0, 0, 0, 180f/225f);
        listImage[index].gameObject.SetActive(true);

        listImage[index].DOFade(0f, 0.5f)
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
                listImage[index].rectTransform.localPosition = new Vector3(0f, -225f, 0f);
                listImage[index].gameObject.SetActive(false);
            });
        Sequence sequence = DOTween.Sequence();
        sequence.Append
            (
                listImage[index].rectTransform
                    .DOAnchorPos(new Vector2(0f, -25f), 1f)
            );
        showIndex++;
        if( showIndex >= listImage.Count )
        {
            showIndex = 0;
        }
    }
}
```

## Assets/0.Script/1.UI/PopUp/PopupController.cs

```csharp
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] ToastPopup toastPopup;
    [SerializeField] ListPopup listpopup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            toastPopup.OnPopup("빛나는 방어구 A를 습득했습니다.");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            // 앞에 아이템 이름, 갯수
            listpopup.On("방어구 A");
        }
    }
}
```

## Assets/0.Script/1.UI/PopUp/ToastPopup.cs

```csharp
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

public class ToastPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text txt;
    [SerializeField] private HorizontalLayoutGroup hlg;

    private Queue<string> strings = new Queue<string>();
    private bool isAnimation = false;

    public void OnPopup(string str)
    {
        strings.Enqueue(str);
    }

    private void Update()
    {
        if(strings.Count != 0 && !isAnimation)
        {
            txt.text = strings.Dequeue();
            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        isAnimation = true;

        hlg.enabled = false;
        yield return new WaitForSeconds(0.2f);
        hlg.enabled = true;
        GetComponent<Image>().rectTransform
            .DOAnchorPos(new Vector2(0f, 180f), 0.2f)
            .SetEase(Ease.OutBack)
            .SetUpdate(false)
            .OnComplete(() =>
            {
                GetComponent<Image>().rectTransform
                    .DOAnchorPos(new Vector2(0f, -80f), 0.1f)
                    .SetDelay(2f)
                    .SetEase(Ease.Linear)
                    .SetUpdate(false)
                    .OnComplete(() => isAnimation = false);
            });
        yield return null;
    }
}
```

## Assets/0.Script/1.UI/Quest/QuestProgress.cs

```csharp
using UnityEngine;

public class QuestProgress
{
    public QuestData Data {  get; private set; }

    public int CurrentCount { get; private set; }

    public QuestState State { get; private set; }

    public QuestProgress(QuestData data)
    {
        Data = data;
        CurrentCount = 0;
        State = QuestState.Inprogress;
    }

    public void Addprogress (int amount = 1)
    {
        if (State != QuestState.Inprogress)
            return;
        CurrentCount += amount;
        if(CurrentCount >= Data.requiredCount)
        {
            CurrentCount = Data.requiredCount;
            State = QuestState.canComplete;
        }
    }

    public void Complete()
    {
        if(State != QuestState.canComplete)
        {
            return;
        }
        State = QuestState.Completed;
    }
}
```

## Assets/0.Script/1.UI/Quest/QuestUi.cs

```csharp
using TMPro;
using UnityEngine;

public class QuestUi : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text progressTxt;

    private void Start()
    {
        GameEvents.OnQuestChanged += ReFresh;
    }
    private void OnDisable()
    {
        if (questManager != null)
            GameEvents.OnQuestChanged -= ReFresh;
    }

    private void ReFresh()
    {
        if (questManager.ActiveQuest.Count == 0)
        {
            titleTxt.text = string.Empty;
            progressTxt.text = string.Empty;
            gameObject.SetActive(false);
            return;
        }
        QuestProgress quest = questManager.ActiveQuest[0];
        titleTxt.text = quest.Data.questTitle;
        gameObject.SetActive(true);
        switch (quest.State)
        {
            case QuestState.Inprogress:
                progressTxt.text = $"{quest.CurrentCount}/{quest.Data.requiredCount}";
                break;
            case QuestState.Completed:
                progressTxt.text = "퀘스트 완료";
                break;
            case QuestState.canComplete:
                progressTxt.text = "퀘스트 완료 가능";
                break;

        }
    }
}
```

## Assets/0.Script/1.UI/Stat/PlusStatSlot.cs

```csharp
using TMPro;
using UnityEngine;

public class PlusStatSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text plusTxt;
    [SerializeField] private TMP_Text totalTxt;
    [SerializeField] private StatUI statUI;
    public int TotalStat { get; private set; } = 0;
    private int plusStat = 0;

    public void Plus()
    {
        if (statUI.LevelStat != 0)
        {
            plusStat++;
            statUI.LevelStat--;
            plusTxt.text = $"{plusStat}";
            statUI.view.LevelView();
        }
    }

    public void Minus()
    {
        if (plusStat != 0)
        {
            plusStat--;
            statUI.LevelStat++;
            plusTxt.text = $"{plusStat}";
            statUI.view.LevelView();
        }
    }

    public void AcceptStat()
    {
        TotalStat += plusStat;
        plusStat = 0;
        totalTxt.text = $"{TotalStat}";
        plusTxt.text = $"{plusStat}";
        statUI.view.TotalStatUpdate();
    }

    public void ResetStat()
    {
        statUI.LevelStat += plusStat;
        plusStat = 0;
        plusTxt.text = $"{plusStat}";
        statUI.view.LevelView();
    }
}
```

## Assets/0.Script/1.UI/Stat/StatUI.cs

```csharp
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : Singleton<StatUI>
{
    [SerializeField] public StatUIView view;
    [SerializeField] private GameObject UiStat;
    [SerializeField] private List<PlusStatSlot> StatSlots;

    public int TotalStr { get; private set; }
    public int TotalHp { get; private set; }
    public int TotalDef { get; private set; }
    public int LevelStat { get; set; } = 5;

    private void Awake()
    {

       
        view.LevelView();
    }

    private void Update()
    {
        TotalStr = StatSlots[0].TotalStat;
        TotalHp = StatSlots[1].TotalStat;
        TotalDef = StatSlots[2].TotalStat;
        view.TotalStatUpdate();
    }
    public void LevelChange()
    {
        LevelStat += 1;
        view.LevelView();
    }
}
```

## Assets/0.Script/1.UI/Stat/StatUIView.cs

```csharp
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUIView : MonoBehaviour
{

    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private List<TMP_Text> stat;
    [SerializeField] private TMP_Text levelStat;
    [SerializeField] private StatUI statUI;

    public void LevelView()
    {
        levelStat.text = $"{statUI.LevelStat}";
    }

    public void TotalStatUpdate ()
    {
        stat[0].text = $"{playerStat.TotalHP()}";
        stat[1].text = $"{playerStat.TotalDamage()}";
        stat[2].text = $"{playerStat.TotalDefence()}";
        stat[3].text = $"{playerStat.TotalSpeed()}";
    }
}
```

## Assets/0.Script/9.Addressable/AddressableLoader.cs

```csharp
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AddressableLoader : MonoBehaviour
{
    private AsyncOperationHandle<GameObject> handle;
    private GameObject monster;

    private void Start()
    {
        LoadEnemy("Skeleton");
    }
    
    private void LoadEnemy(string name)
    {
        handle = Addressables.LoadAssetAsync<GameObject>($"{name}");
        handle.Completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status != AsyncOperationStatus.Succeeded)
            return;
        GameObject prefab = operation.Result;
        Debug.Log("로드 성공?");
    }

    private void OnDestroy()
    {
        if(handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }
}
```

## Assets/0.Script/Audio/AudioManager.cs

```csharp
using UnityEngine;

public enum ClipType
{
    Click,
    Attack,
    Hit
}
public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    public class Clip
    {
        public ClipType type = ClipType.Click;
        public AudioClip clip;
    }
    [SerializeField] private Clip[] audioClips;
    [SerializeField] private AudioSource[] effectSources;

    private int effectPlayIndex = 0;
    public void EffectSound(ClipType clipType)
    {
        foreach (Clip clip in audioClips)
        {
            if(clip.type == clipType)
            {
                if(effectSources.Length <= effectPlayIndex)
                    effectPlayIndex = 0;

                effectSources[effectPlayIndex].clip = clip.clip;
                effectPlayIndex++;
                effectSources[effectPlayIndex].Play();


                break;
            }
        }
    }
}
```

## Assets/0.Script/Audio/ClickSount.cs

```csharp
using UnityEngine;
using UnityEngine.UI;

public class ClickSount : MonoBehaviour
{
    [SerializeField] private ClipType clipType;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.EffectSound(clipType));
    }

}
```

## Assets/0.Script/Loding.cs

```csharp
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loding : Singleton<Loding>
{
    [SerializeField] private Slider loadingbar;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private Transform loadingImage;

    public static string TargetScene { get; private set; } = string.Empty;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(SceneType sceneName)
    {
        TargetScene = sceneName.ToString();
        SceneManager.LoadScene("Loading");

    }
    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(TargetScene, LoadSceneMode.Additive);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(op.progress);
            loadingbar.value = progress;
            loadingText.text = $"Loading... {(progress * 100f):F0}%";
            yield return null;
        }

        System.GC.Collect();

        loadingbar.value = 1f;
        loadingText.text = $"Loading... 100%";
        // scene ON
        op.allowSceneActivation = true;
        // scene load finish
        while(!op.isDone)
            yield return null;

        // 타겟 씬을 active로
        Scene target = SceneManager.GetSceneByName(TargetScene);
        SceneManager.SetActiveScene(target);

        yield return new WaitForSeconds(2f);

        //scene remove
        SceneManager.UnloadSceneAsync("Loading");
    }
}
```

## Assets/0.Script/PlayFromTitle.cs

```csharp
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class PlayFromTitle
{

    static PlayFromTitle()
    {

        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/9.Scenes/TestRoom.unity");

    }

}
```

## Assets/0.Script/SceneLoader.cs

```csharp
using UnityEngine;

public enum SceneType
{
    Lobby,
    Dungeon1,
    Dungeon2,
    BossRoom,
    TestRoom
}

public class SceneLoader : Singleton<SceneLoader>
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadingScene(SceneType name)
    {
        Loding.LoadScene(name);
    }

    public void LoadGame()
    {
        Loding.LoadScene(SceneType.Lobby);
    }
}
```

## Assets/0.Script/VFX/VFXManager.cs

```csharp
using UnityEngine;


public enum VFXtype
{
    Heal,
    AreaAttack,
    ChargeAttack
}

public class VFXManager : Singleton<VFXManager>
{
    [System.Serializable]
    public class VFXData
    {
        public VFXtype type;
        public GameObject vfxObj;
    }
    [SerializeField] private VFXData[] vfxDatas;

    public void Show(VFXtype fxType, Transform tran)
    {
        Quaternion rotaitionArea = Quaternion.Euler(0f, tran.eulerAngles.y, 0f);
        foreach (var vfx in vfxDatas)
        {
            if(fxType == vfx.type)
            {
                Instantiate(vfx.vfxObj, tran.position, rotaitionArea).transform.SetParent(transform);

                break;
            }
        }
    }
}
```

## Assets/6.Data/DataScript/ItemScriptable.cs

```csharp
using System;
using UnityEngine;

public enum ItemType
{
    Equip,
    Posion,
    Gold
}

public enum EquipType
{
    Item,
    Weapon,
    Armor,
    Helmet,
    Boots
}

[CreateAssetMenu]
public class ItemScriptable : ScriptableObject
{
    [SerializeField]
    private int itemID;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private Sprite backgroundIcon;

    [SerializeField]
    private uint maxStack;

    [SerializeField]
    private int price;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int hp;

    [SerializeField]
    private int defence;

    [SerializeField]
    private float speed;

    public int ItemID => itemID;
    public ItemType itemType;
    public EquipType equipType;
    public string ItemName => itemName;
    public Sprite Icon => icon;
    public Sprite BackgroundIcon => backgroundIcon;
    public uint MaxStack => maxStack;
    public int Price => price;

    public int Damage => damage;
    public int HP => hp;
    public int Defence => defence;
    public float Speed => speed;
}
```

## Assets/6.Data/DataScript/MonsterData.cs

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "MD")]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private int monsterId;
    public int MonsterId { get { return monsterId; } }

    [SerializeField]
    private int hp = 100;
    public int Hp {  get { return hp; } }

    [SerializeField]
    private int damage = 5;
    public int Mdamage { get { return damage; } }

    [SerializeField]
    private float moveSpeed = 3.5f;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float range = 1.5f;
    public float Range { get { return range; } }

    [SerializeField]
    private float spawnRange = 7f;
    public float SpawnRange { get { return spawnRange; } }

    [SerializeField]
    private float scanSize = 5f;
    public float ScanSize { get { return scanSize; } }

    [SerializeField]
    private float getExp = 2.5f;
    public float GetExp {  get { return getExp; } }

    [SerializeField]
    private int getGold = 50;
    public int GetGold { get { return getGold; } }

}
```

## Assets/6.Data/DataScript/PlayerData.cs

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "PD")]
public class PlayerData : ScriptableObject 
{
    [SerializeField]
    private int hp;
    public int HP { get { return hp; } }
    [SerializeField]
    private int maxhp;
    public int Maxhp { get { return maxhp; } }

    [SerializeField]
    private float jumpHeight;
    public float JumpHeight { get { return jumpHeight; } }

    [SerializeField]
    private float dashForce;
    public float DashForce { get { return dashForce; } }

    [SerializeField]
    private float moveForce;
    public float MoveForce { get {return moveForce; } }

    [SerializeField]
    private int damage;
    public int Wdamage { get { return damage; } }

    [SerializeField]
    private int areaDmg;
    public int AreaDmg { get { return areaDmg; } }

    [SerializeField]
    private float maxExp;
    public float MaxExp { get { return maxExp; } }

    [SerializeField]
    private float interationScale = 2f;
    public float InterationScale { get { return interationScale; } }
}
```

## Assets/6.Data/DataScript/QuestData.cs

```csharp
using UnityEngine;

public enum QuestState
{
    Inprogress, canComplete, Completed
}

public enum QuestType
{
    Kill, Collect
}

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    [Header("Info")]
    public int questId;
    public string questTitle;

    [TextArea]
    public string description;

    [Header("Condition")]
    public QuestType questType;
    public int targetId;
    public int requiredCount = 1;

    [Header("Reward")]
    public int rewardGold;
    public int rewardExp;
    public ItemScriptable rewardItem;
    public int rewardItemCount;
}
```

## Assets/6.Data/Json/Testjson.cs

```csharp
using UnityEngine;

public class Testjson : MonoBehaviour
{

    void Start()
    {
        TestSaveData data = new TestSaveData();
        data.playerName = "Hero";
        data.level = 1;
        data.gold = 200;
        data.exp = 50f;

        // 저장 // 보안코드
        string jsonDataString = JsonUtility.ToJson(data, true);
        Debug.Log(jsonDataString);
        // 로드
        TestSaveData loadData = JsonUtility.FromJson<TestSaveData>(jsonDataString);

        Debug.Log(Application.persistentDataPath);
    }

}
```

## Assets/6.Data/Json/TestSaveData.cs

```csharp
using UnityEngine;

[System.Serializable]
public class TestSaveData
{

    public string playerName;
    public int level;
    public int gold;
    public float exp;
    public bool isDead;

}
```

