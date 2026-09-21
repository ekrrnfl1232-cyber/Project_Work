using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [Header("Animator")]
    public Animator animator;
    [Header("Script")]
    public PlayerView view { get; private set; }
    public PlayerStat stat;
    public PlayerData data;

    [Header("Controller")]
    public CharacterController controll;
    public BoxCollider sword;

    [Header("UI")]
    public GameObject UiSystem;

    // »óÅÂ
    private IState currentState;
    private PlayerState currentKey;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }

    // ÄðÅ¸ÀÓ
    private PlayerCooldown cooldown = new PlayerCooldown(1f, 1f);
    public PlayerCooldown Cool { get { return cooldown; } }

    private void Awake()
    {
        SettingState();
    }

    private void Start()
    {
        view = GetComponent<PlayerView>();
        view.ExpUpdata();
        view.CreateHp();
        ChangeState(PlayerState.idleState);
        InputManger.Instance.input.Player.Get().actionTriggered += OnAction;
    }

    private void Update()
    {
        
        view.HPbar(transform.position);
        if (InputManger.Instance.input.Player.Move.IsPressed())
        {
            stat.MoveDir = InputManger.Instance.input.Player.Move.ReadValue<Vector2>();
        }
        else
        {
            stat.MoveDir = Vector2.zero;
        }
        stat.Movement = new Vector3(stat.MoveDir.x, 0, stat.MoveDir.y).normalized;
        if (InputManger.Instance.input.Player.enabled)
        {
            Interect();
            Look();
        }
        Gravity();
        Cool.TIck(Time.deltaTime);
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
    }

    private void Gravity()
    {
        if(controll.isGrounded)
        {
            if(stat.VerticalVelo < 0f)
                stat.VerticalVelo = -2f;
        }
        else
        {
            stat.VerticalVelo += Physics.gravity.y * Time.deltaTime;
        }
        stat.Gravity = new Vector3 (0, stat.VerticalVelo, 0 );
        controll.Move(stat.Gravity * Time.deltaTime);
    }

    void Interect()
    {
        Vector3 posInter = transform.position;
        posInter.y += 1f;
        Collider[] colls = Physics.OverlapSphere(posInter, stat.InterationScale);
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

    private void OnAction(InputAction.CallbackContext contxt)
    {
        if(!controll.isGrounded)
        {
            return;
        }
        switch (contxt.action.name)
        {
            case "Attack":
                if(Cool.IsReady(PlayerCool.Attack))
                {
                    ChangeState(PlayerState.attackState);
                }
                break;
            case "Jump":
                ChangeState(PlayerState.jumpState);
                break;
            case "Sprint":
                if (Cool.IsReady(PlayerCool.Dash))
                {
                    ChangeState(PlayerState.dashState);
                }
                break;
        }
    }
}
