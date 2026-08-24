using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Monster : MonoBehaviour, IDamageable
{

    [SerializeField] private GameObject hpBG;
    [SerializeField] private Image hpImg;

    [SerializeField] private int mDamage = 5;

    [SerializeField] public Transform target;

    [SerializeField] public int hp, maxhp = 100;

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

    void Awake()
    {
        Mmodel = new MonsterModel
            (
                mDamage,
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
        Mmodel.StartDis = Vector3.Distance(transform.position, Mmodel.StartPos);
        currentState?.Tick();
    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
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
        ChangeState(new MonsterHitState(this, currentState,damage));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 posAttack = transform.position;
        posAttack.y += 1f;
        Gizmos.DrawWireSphere(posAttack, 5f);
    }
}
