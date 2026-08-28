using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{

    [Header("Move")]
    public Vector3 movement = Vector3.zero;

    [Header("InteractScale")]
    [SerializeField] private float InterationScale = 2f;

    [Header("Animator")]
    [SerializeField] public Animator animator;

    private IState currentState;
    public Rigidbody rb;
    LayerMask ground;
    private Cooldown coolDown = new Cooldown(1f);
    public Cooldown AtkCool { get { return coolDown; }}
    [HideInInspector] public PlayerView view;
    [HideInInspector] public PlayerModel model;
    [HideInInspector] public PlayerStat stat;
    [SerializeField] public PlayerData data;
    public UIConstroller UiCon { get; private set; }
    public int Dmg { get; set; }
    public GameObject UiSystem;

    private void Awake()
    {
        stat = new PlayerStat();
        model = new PlayerModel
            (
            InterationScale, data.Maxhp,movement
            );
    }

    private void Start()
    {
        stat.BaseAttack = data.Wdamage;
        Debug.Log($"{stat.BaseAttack}");
        model.IsGrounded = true;
        UiCon = UiSystem.GetComponent<UIConstroller>();
        view = GetComponent<PlayerView>();
        view.CreateHp();
        ChangeState(new PlayerIdle(this));
    }

    private void Update()
    {
        view.HPbar(transform.position);
        Vector3 move = Vector3.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        
        model.Movement = move;
        if (!UiCon.isOnInventory)
        {
            if (Input.GetMouseButtonDown(0) && model.IsGrounded && AtkCool.IsReady)
            {
                Debug.Log("공격키 입력");
                ChangeState(new PlayerAttackState(this, currentState));
            }
            if (Input.GetKeyDown(KeyCode.Space) && model.IsGrounded)
            {
                Debug.Log("점프 키 입력");
                ChangeState(new PlayerJumpState(this, currentState, rb));
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ChangeState(new PlayerDashState(this, currentState, rb));
            }
            AtkCool.Tick(Time.deltaTime);
            Interect();
            Look();
        }
        currentState?.Tick();
    }

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
    public void Dash(IState prevState)
    {
        animator.SetBool("ShieldRush", true);
        rb.AddRelativeForce(Vector3.forward * data.DashForce, ForceMode.VelocityChange);
        Invoke("StopDash", 0.2f);
        ChangeState(prevState);
    }
    void StopDash()
    {
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        animator.SetBool("ShieldRush", false);
    }

    public void TakeDamage(int damage)
    {
        ChangeState(new PlayerHitState(this, currentState, damage));
    }

    void Interect()
    {
        Vector3 posInter = transform.position;
        posInter.y += 1f;
        Collider[] colls = Physics.OverlapSphere(posInter, model.InterationScale);
        bool isFind = false;

        foreach (var col in colls)
        {
            if (col.TryGetComponent<IInterectable>(out IInterectable interact))
            {
                isFind = true;
                if (Input.GetKeyDown(KeyCode.F))
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
                //transform.forward = dir;
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 0.5f, 1f));
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            model.IsGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            model.IsGrounded = false;
        }
    }
}
