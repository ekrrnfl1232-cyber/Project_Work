using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    idleState,
    moveState,
    attackState,
    jumpState,
    dashState,
    hitState
}

public class Player : MonoBehaviour, IDamageable
{

    [Header("Move")]
    public Vector3 movement = Vector3.zero;

    [Header("InteractScale")]
    [SerializeField] private float InterationScale = 2f;

    [Header("Animator")]
    [SerializeField] public Animator animator;
    public Cooldown AtkCool { get { return coolDown; }}
    [HideInInspector] public PlayerView view;
    [HideInInspector] public PlayerModel model;
    public PlayerStat stat;
    [SerializeField] public PlayerData data;
    public int Dmg { get; set; }
    public GameObject UiSystem;
    public CharacterController controll;

    private bool isOn;
    private IState currentState;
    private PlayerState currentKey;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }
    LayerMask ground;
    private Cooldown coolDown = new Cooldown(1f);
    public InputSystem_Actions inputAction { get; private set; }
    private void Awake()
    {
        model = new PlayerModel
            (
            InterationScale, data.Maxhp,movement, data.MaxExp
            );
        GameEvents.OnInventChange += OnController;
        inputAction = new InputSystem_Actions();
        inputAction.Enable();
        SettingState();
    }

    private void Start()
    {
        model.IsGrounded = true;
        isOn = false;
        view = GetComponent<PlayerView>();
        view.ExpUpdata(0);
        view.CreateHp();
        ChangeState(PlayerState.idleState);
    }

    private void Update()
    {
        view.HPbar(transform.position);
        Vector2 movedir = inputAction.Player.Move.ReadValue<Vector2>();
        model.Movement = new Vector3(movedir.x, 0, movedir.y);
        if (inputAction.Player.Attack.triggered && model.IsGrounded && AtkCool.IsReady)
        {
            Debug.Log("공격키 입력");
            ChangeState(PlayerState.attackState);
        }
        if (inputAction.Player.Jump.triggered && model.IsGrounded)
        {
            Debug.Log("점프 키 입력");
            ChangeState(PlayerState.jumpState);
        }
        if (inputAction.Player.Sprint.triggered)
        {
            ChangeState(PlayerState.dashState);
        }
        if (!isOn)
        {
            Interect();
            Look();
        }
        AtkCool?.Tick(Time.deltaTime);
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
            { PlayerState.moveState, new PlayerMoveState(this, controll) },
            { PlayerState.attackState, new PlayerAttackState(this) },
            { PlayerState.jumpState, new PlayerJumpState(this, controll) },
            { PlayerState.dashState, new PlayerDashState(this, controll) },
            { PlayerState.hitState, new PlayerHitState(this) }
        };
    }

    public void OnController(bool isOn)
    {
        if (!isOn)
        {
            inputAction.Enable();
        }
        else
        {
            inputAction.Disable();
        }
        this.isOn = isOn;
    }
    void StopDash()
    {
        animator.SetBool("ShieldRush", false);
        ChangeState(PlayerState.idleState);
    }

    public void TakeDamage(int damage)
    {
        if (model.HP > 0)
        {
            model.HP -= damage;
            ChangeState(PlayerState.hitState);
        }
        else
        {
            Debug.Log($"{name} Dead");
        }
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
                if (inputAction.Player.Interact.triggered)
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

    private void OnDisable()
    {
        GameEvents.OnInventChange -= OnController;
    }

}
