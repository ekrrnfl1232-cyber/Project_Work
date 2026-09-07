using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
    public Vector2 movedir {  get; private set; } = Vector2.zero;

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

    private IState currentState;
    private PlayerState currentKey;
    private Vector3 velocity = Vector3.zero;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }
    private Cooldown coolDown = new Cooldown(1f);
    private void Awake()
    {
        model = new PlayerModel
            (
            InterationScale, data.Maxhp,movement, data.MaxExp
            );
        SettingState();
    }

    private void Start()
    {
        model.IsGrounded = true;
        view = GetComponent<PlayerView>();
        view.ExpUpdata(0);
        view.CreateHp();
        ChangeState(PlayerState.idleState);
    }

    private void Update()
    {
        view.HPbar(transform.position);
        if (InputManger.Instance.input.Player.Move.IsPressed())
        {
            movedir = InputManger.Instance.input.Player.Move.ReadValue<Vector2>();
        }
        else
        {
            movedir = Vector2.zero;
        }
            model.Movement = new Vector3(movedir.x, 0, movedir.y).normalized;
        if (InputManger.Instance.input.Player.Attack.triggered && AtkCool.IsReady && controll.isGrounded)
        {
            Debug.Log("공격키 입력");
            ChangeState(PlayerState.attackState);
        }
        if (InputManger.Instance.input.Player.Jump.triggered && controll.isGrounded)
        {
            Debug.Log("점프 키 입력");
            ChangeState(PlayerState.jumpState);
        }
        if (InputManger.Instance.input.Player.Sprint.triggered)
        {
            ChangeState(PlayerState.dashState);
        }
        Interect();
        if(InputManger.Instance.input.Player.enabled)
            Look();
        
        Gravity();
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

    private void Gravity()
    {
        if(controll.isGrounded)
        {
            if(model.VerticalVelo < 0f)
                model.VerticalVelo = -2f;
        }
        else
        {
            model.VerticalVelo += Physics.gravity.y * Time.deltaTime;
        }
        model.gravity = new Vector3 (0, model.VerticalVelo, 0 );
        controll.Move(model.gravity * Time.deltaTime);
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
            { PlayerState.hitState, new PlayerHitState(this) }
        };
    }

    public void TakeDamage(int damage)
    {
        if (model.HP > 0)
        {
            model.HP -= damage;
            stat.Hp = model.HP;
            DamageFontManager.Instance.CreateText(damage, transform.position);
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
                //transform.forward = dir;
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 0.5f, 1f));
    }*/
}
