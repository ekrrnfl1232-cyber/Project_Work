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

    [Header("Animator")]
    [SerializeField] public Animator animator;
    [HideInInspector] public PlayerView view;
    public PlayerModel model { get; set; }
    public PlayerStat stat;
    [SerializeField] public PlayerData data;
    public int Dmg { get; set; }
    public GameObject UiSystem;
    public CharacterController controll;

    private IState currentState;
    private PlayerState currentKey;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }
    private Cooldown coolDown = new Cooldown(1f);
    public Cooldown AtkCool { get { return coolDown; }}
    private void Awake()
    {
        model = new PlayerModel(data);
        SettingState();
    }

    private void Start()
    {
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
        if (controll.isGrounded)
        {
            if (InputManger.Instance.input.Player.Attack.WasPressedThisFrame() && AtkCool.IsReady)
            {
                Debug.Log("공격키 입력");
                ChangeState(PlayerState.attackState);
            }
            if (InputManger.Instance.input.Player.Jump.WasPressedThisFrame())
            {
                Debug.Log("점프 키 입력");
                ChangeState(PlayerState.jumpState);
            }
            if (InputManger.Instance.input.Player.Sprint.WasPressedThisFrame())
            {
                ChangeState(PlayerState.dashState);
            }
        }
        if (InputManger.Instance.input.Player.enabled)
        {
            Interect();
            Look();
        }
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
        if (stat.Hp > 0)
        {
            stat.Hp -= damage;
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
            }
        }
    }
}
