using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
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

    private float distance;
    private IState currentState;
    private Vector3 tPos;
    public Cooldown attackCool = new Cooldown(5f);
    public MonsterView view;
    public MonsterModel model;

    void Awake()
    {
        model = new MonsterModel
            (
                mDamage,
                hp,
                distance,
                tPos
            );
    }

    void Start()
    {
        view = GetComponent<MonsterView>();
        view.CreateHp();
        ChangeState(new MonsterIdleState(this));
    }
    void Update()
    {
        if (target == null)
            return;
        view.HPbar(transform.position);
        LookAt();
        attackCool.Tick(Time.deltaTime);
        if (attackCool.IsReady)
        {
            Debug.Log($"{name}АјАн");
            ChangeState(new MonsterAttackState(this, currentState));
        }
        currentState?.Tick();
    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    private void LookAt()
    {
        tPos.y = 0;
        model.TarPos = tPos;
        transform.LookAt(model.TarPos);
        model.Dis = Vector3.Distance(transform.position, target.position);
    }
    public void TakeDamage(int damage)
    {
        ChangeState(new MonsterHitState(this, currentState,damage));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 posAttack = transform.position + transform.forward * 1f;
        posAttack.y += 0.7f;
        Gizmos.DrawWireCube(posAttack, new Vector3(1f, 1.4f, 0.7f));
    }
}
