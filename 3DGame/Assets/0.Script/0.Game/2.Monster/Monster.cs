using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageable
{
    public Transform target;

    public LayerMask targetlayer { get; private set; }
    private IState currentState;
    private string currentKey;

    public bool IsFind { get; private set; }
    public Cooldown attackCool { get; set; } = new Cooldown(5f);

    public Animator MonsterAni {  get; set; }

    public MonsterView View { get; set; }
    public MonsterModel Model { get; set; }

    public QuestManager qm;
    public NavMeshAgent agent {  get; set; }
    [SerializeField] public MonsterData data;
    public bool IsLive { get; set; } = true;
    public string PrevState { get; private set; }
    public Dictionary<string, IState> States { get; private set; }
    public float StartDis { get; private set; }
    void Awake()
    {
        Model = new MonsterModel
            (
                data.Hp,
                transform.position
            );
        SettingState();
    }

    void Start()
    {
        MonsterAni = GetComponent<Animator>();
        View = GetComponent<MonsterView>();
        agent = GetComponent<NavMeshAgent>();
        targetlayer = LayerMask.GetMask("Player");
        Model.HP = Model.MaxHP = data.Hp;

        View.CreateHp();
        ChangeState("idleState");
    }
    void Update()
    {
        if (target == null || agent == null)
            return;

        View.HPbar(transform.position);
        ScanTarget();
       
        attackCool.Tick(Time.deltaTime);

        StartDis = Vector3.Distance(transform.position, Model.StartPos);
        Model.TargetDis = Vector3.Distance(transform.position, target.position);

        currentState?.Tick();
    }

    public void ChangeState(string state)
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
        GameEvents.RaiseKillChange(data.MonsterId);
        PlayerProgress.Instance.AddExp(data.GetExp);
        PlayerProgress.Instance.AddGold(data.GetGold);
        View.DeleteHp();
        Invoke("ReSpawn", 1f);
    }
    public void ReSpawn()
    {
        ChangeState("reviveState");
    }
    public void TakeDamage(int damage)
    {
        if (Model.HP <= damage)
        {
            if (Model.HP != 0)
            {
                Model.HP = 0;
                ChangeState("deadState");
            }
            else
                return;
        }
        else if (Model.HP != 0)
        {
            Model.HP -= damage;
            ChangeState("hitState");
            DamageFontManager.Instance.CreateText(damage, transform.position);
        }
        else
            return;
    }

    private void SettingState()
    {
        States = new Dictionary<string, IState>()
        {
            { "attackState", new MonsterAttackState(this) },
            { "idleState", new MonsterIdleState(this) },
            { "moveState", new MonsterMoveState(this) },
            { "patrolState", new MonsterPatrolState(this) },
            { "reviveState", new MonsterReviveState(this) },
            { "deadState", new MonsterDeadState(this) },
            { "hitState", new MonsterHitState(this) }
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
    
}
