using System.Collections.Generic;
using Unity.VisualScripting;
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

    public Animator MonsterAni;

    public MonsterView View;
    public MonsterModel Model { get; set; }

    public NavMeshAgent agent;  
    public MonsterData data { get; set; }
    public bool IsLive { get; set; } = true;
    public MonsterState PrevState { get; private set; }
    public Dictionary<MonsterState, IState> States { get; private set; }
    public float StartDis { get; private set; }

    void Start()
    {
        Model = new MonsterModel
            (
                data.Hp,
                transform.position
            );
        SettingState();
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
        GameEvents.RaiseKillChange(data, data.GetGold, data.GetExp);
        GameEvents.RaiseChangeDeadMonster();
        View.ReturnHp();

        Model.HP = data.Hp;

        ObjectPoolManager.Instance.ReturnObject(PoolType.Enemy, gameObject);
    }
    public void ReSpawn()
    {
        ChangeState(MonsterState.revive);
    }
    public void TakeDamage(int damage)
    {
        DamageFontManager.Instance.CreateText(damage, transform.position);
        if (Model.HP <= damage)
        {
            Model.HP = 0;
            ChangeState(MonsterState.dead);
        }
        else
        {
            Model.HP -= damage;
            View.HpUpdate(Model.HP, Model.MaxHP);
        }
    }

    private void SettingState()
    {
        States = new Dictionary<MonsterState, IState>()
        {
            { MonsterState.attack, new MonsterAttackState(this) },
            { MonsterState.idle, new MonsterIdleState(this) },
            { MonsterState.chase, new MonsterMoveState(this) },
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
