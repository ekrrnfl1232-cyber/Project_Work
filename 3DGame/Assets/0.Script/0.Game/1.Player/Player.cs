using DG.Tweening.Core.Easing;
using System.Collections.Generic;
using UnityEditor;
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
    hitState,
    dashAttackState,
    fangState,
    rootBindState
}

public class Player : MonoBehaviour, IDamageable
{
    [Header("Character Component")]
    public Animator animator;
    public CharacterController controll;

    [Header("Player Data")]
    public PlayerStat stat;
    public PlayerData data;

    [Header("View And AnimationEnvent")]
    public PlayerView view;
    public PlayerAnimationEvent playerani;

    // 플레이어 기능
    private PlayerInteract interact;
    private PlayerLook look;

    // 쿨타임
    private PlayerCooldown cooldown = new PlayerCooldown();
    public PlayerCooldown Cool => cooldown;

    // 상태 패턴
    private IState currentState;
    private PlayerState currentKey;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }

    // 액션 제어
    public bool RotationLock { get; set; } = false;
    private bool actionLock;

    private void Awake()
    {
        SettingState();
        interact = new PlayerInteract(this);
        look = new PlayerLook(this);
    }

    private void Start()
    {
        view.ExpUpdata();
        view.CreateHp();
        InputManger.Instance.input.Player.Get().actionTriggered += OnAction;
        ChangeState(PlayerState.idleState);
    }

    private void Update()
    {
        IsMove();
        if (InputManger.Instance.input.Player.enabled && !RotationLock)
        {
            look?.Tick(Input.mousePosition);
        }
        interact.Tick();
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
            { PlayerState.hitState, new PlayerHitState(this) },
            { PlayerState.dashAttackState, new PlayerDashAttackState(this) },
            { PlayerState.fangState, new PlayerFangState(this) },
            { PlayerState.rootBindState, new PlayerRootBindState(this) }
        };
        playerani.Init(this);
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

    private void IsMove()
    {
        if (actionLock)
        {
            stat.MoveDir = Vector2.zero;
            stat.Movement = Vector3.zero;
            return;
        }
        if (InputManger.Instance.input.Player.Move.IsPressed())
        {
            stat.MoveDir = InputManger.Instance.input.Player.Move.ReadValue<Vector2>();
        }
        else
        {
            stat.MoveDir = Vector2.zero;
        }
        stat.Movement = new Vector3(stat.MoveDir.x, 0, stat.MoveDir.y).normalized;
    }

    public void ControllRock(bool isRock)
    {
        actionLock = isRock;
    }

    private void OnAction(InputAction.CallbackContext contxt)
    {
        if (actionLock)
        {
            return;
        }
        if (!controll.isGrounded)
        {
            return;
        }
        if(!contxt.performed)
        {
            return;
        }
        switch (contxt.action.name)
        {
            case "Attack":
                if (currentState is PlayerAttackState attack)
                {
                    attack.QueueAttack();
                }
                else if (Cool.IsReady(PlayerCool.Attack))
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
            case "Interact":
                interact.Interact();
                break;
            case "Skill":
                SkillInput(contxt);
                break;
        }
    }

    private void SkillInput(InputAction.CallbackContext contxt)
    {
        if (contxt.control is not UnityEngine.InputSystem.Controls.KeyControl key)
            return;
        switch (key.keyCode)
        {
            case Key.Digit1:
                if (Cool.IsReady(PlayerCool.fangAttack))
                    ChangeState(PlayerState.fangState);
                break;
            case Key.Digit2:
                if (Cool.IsReady(PlayerCool.dashAttack))
                    ChangeState(PlayerState.dashAttackState);
                break;
            case Key.Digit3:
                if(Cool.IsReady(PlayerCool.RootBind))
                    ChangeState(PlayerState.rootBindState);
                break;
            case Key.Digit4:
                break;
            case Key.E:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.y = 1.3f;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, 1f);
    }

    private void OnDisable()
    {
        InputManger.Instance.input.Player.Get().actionTriggered -= OnAction;
    }
}
