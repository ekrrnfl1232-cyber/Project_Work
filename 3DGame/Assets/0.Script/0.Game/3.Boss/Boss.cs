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
