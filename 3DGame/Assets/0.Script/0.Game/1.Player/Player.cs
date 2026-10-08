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
    hitState,
    dashAttackState
}

public class Player : MonoBehaviour, IDamageable
{
    [Header("Animator")]
    public Animator animator;
    [Header("Script")]
    public PlayerView view;
    public PlayerStat stat;
    public PlayerData data;
    public PlayerAnimationEvent playerani;
    private PlayerInteract interact;
    private PlayerLook look;

    [Header("Controller")]
    public CharacterController controll;
    public BoxCollider sword;

    // »óÅÂ
    private IState currentState;
    private PlayerState currentKey;
    public PlayerState prevState { get; private set; }
    public Dictionary<PlayerState, IState> States { get; private set; }

    // ÄðÅ¸ÀÓ
    private PlayerCooldown cooldown = new PlayerCooldown();
    public PlayerCooldown Cool { get { return cooldown; } }

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
        if (InputManger.Instance.input.Player.enabled)
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
            {PlayerState.dashAttackState, new PlayerDashAttackState(this) }
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

    private void OnAction(InputAction.CallbackContext contxt)
    {
        if(!controll.isGrounded)
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
                break;
        }
    }
    private void OnDisable()
    {
        InputManger.Instance.input.Player.Get().actionTriggered -= OnAction;
    }
}
