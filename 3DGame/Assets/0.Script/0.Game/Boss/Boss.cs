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
    private BossView view;

    public Transform target;
    

    public MonsterData data;
    public BossStat stats;
    public GameObject area;
    public GameObject charge;
    public BossState PrevState { get; private set; }
    public Dictionary<BossState, IState> States { get; private set; } = new Dictionary<BossState, IState>();
    public Cooldown NomalCool {  get; private set; }
    public Cooldown ChargeCool { get; private set; }
    public Cooldown AreaCool { get; private set; }
    public NavMeshAgent agent {  get; set; }
    public Animator bossAni;
    public float TargetDis {  get; private set; }
    public bool IsFind {get; private set;}
    Vector3 boxSize = new Vector3(5f, 1f, 5f);
    Vector3 pos = new Vector3(4f, 1f, 6f);
    void Start()
    {
        view = GetComponent<BossView>();
        SettingState();
        SettingCool();
        agent = GetComponent<NavMeshAgent>();
        stats.Hp = stats.MaxHp = data.Hp;
        view.CreateHp();
        ChangeState(BossState.Idle);
    }

    void Update()
    {
        NomalCool.Tick(Time.deltaTime);
        ChargeCool.Tick(Time.deltaTime);
        AreaCool.Tick(Time.deltaTime);
        ScanTarget();
        TargetDis = Vector2.Distance(transform.position, target.position);
        if(stats.Hp <= stats.MaxHp/2)
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
        States.Add(BossState.Idle, new BossIdleState(this));
        States.Add(BossState.Chase, new BossChaseState(this));

        States.Add(BossState.SelectAttack, new BossSelectAttack(this));
        States.Add(BossState.NormalAttack, new BossNormalAttackState(this));
        States.Add(BossState.ChargeAttack, new BossChargeAttack(this));
        States.Add(BossState.AreaAttack, new BossAreaAttack(this));

        States.Add(BossState.PhaseChange, new BossPhaseChangeState(this));
        States.Add(BossState.Dead, new BossDeadState(this));
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
        stats.Hp -= damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 posAttack = transform.position + transform.forward * 1.5f;
        posAttack.y += 1f;
        Gizmos.DrawWireCube(posAttack, new Vector3(2f, 1.4f, 1.5f));
    }
}
