using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField] public Transform target;

    [SerializeField] public int hp, maxhp;

    [HideInInspector] public LayerMask targetlayer;
    [HideInInspector] public float ScanSize = 5f;
    private IState currentState;

    [HideInInspector] public bool isFind;
    [HideInInspector] public bool isReturn;

    [HideInInspector] public Collider[] targetScan;

    [HideInInspector] public Cooldown attackCool = new Cooldown(5f);

    [HideInInspector] public Animator MonAni;

    [HideInInspector] public MonsterView view;
    [HideInInspector] public MonsterModel Mmodel;

    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] public MonsterData data;
    [HideInInspector] public bool isLive = true;
    void Awake()
    {
        Mmodel = new MonsterModel
            (
                hp,
                transform.position
            );
    }

    void Start()
    {
        MonAni = GetComponent<Animator>();
        targetlayer = LayerMask.GetMask("Player");
        view = GetComponent<MonsterView>();
        agent = GetComponent<NavMeshAgent>();
        Mmodel.HP = Mmodel.MaxHP = data.Hp;

        view.CreateHp();
        ChangeState(new MonsterIdleState(this));
    }
    void Update()
    {
        if (target == null || agent == null)
            return;

        view.HPbar(transform.position);
        ScanTarget();
       
        attackCool.Tick(Time.deltaTime);

        Mmodel.TargetDis = Vector3.Distance(transform.position, target.position);

        currentState?.Tick();
    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void OnDead()
    {
        Invoke("Delete", 1.5f);
    }
    public void Delete()
    {
        gameObject.SetActive(false);
    }

    public void ScanTarget()
    {
        Vector3 pos = transform.position;
        pos.y += 1f;
        targetScan = Physics.OverlapSphere(pos, ScanSize);
        foreach (var tar in targetScan)
        {
            isFind = false;
            if (tar.CompareTag("Player"))
            {
                isFind = true;
                break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isLive)
        {
            ChangeState(new MonsterHitState(this, currentState, damage));
        }
    }
}
