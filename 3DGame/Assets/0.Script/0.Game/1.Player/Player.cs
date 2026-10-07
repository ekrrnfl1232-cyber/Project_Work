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
    AreaState
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

    [Header("LayerRaycast")]
    public LayerMask layerRay;
    private bool isTargeting = false;
    public bool IsTargeting { get { return isTargeting; } set { isTargeting = value; } }

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
    }

    private void Start()
    {
        view = GetComponent<PlayerView>();
        view.ExpUpdata();
        view.CreateHp();
        InputManger.Instance.input.Player.Get().actionTriggered += OnAction;
        ChangeState(PlayerState.idleState);
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
            { PlayerState.hitState, new PlayerHitState(this) },
            { PlayerState.AreaState, new PlayerAreaSkillState(this) }
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
        Plane aimPlane = new Plane(Vector3.up, transform.position);
        if (!aimPlane.Raycast(ray, out float distance))
            return;

        Vector3 aimPoint = ray.GetPoint(distance);
        Vector3 dir = aimPoint - transform.position;
        dir.y = 0f;

        if(dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
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
                if (IsTargeting)
                    break;

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
                if (IsTargeting == false)
                {
                    ChangeState(PlayerState.jumpState);
                }
                break;
            case "Sprint":
                if (Cool.IsReady(PlayerCool.Dash) && IsTargeting == false)
                {
                    ChangeState(PlayerState.dashState);
                }
                break;
            case "Area":
                if(Cool.IsReady(PlayerCool.Area))
                {
                    IsTargeting = true;
                    ChangeState(PlayerState.AreaState);
                }
                break;
        }
    }

    public void AttackHit(int attackNumber)
    {
        if(currentState is PlayerAttackState attack)
        {
            attack.OnHit(attackNumber);
        }
    }

    public void AttackComboCheck(int attackNumber)
    {
        if (currentState is PlayerAttackState attack)
        {
            attack.OnComboCheck(attackNumber);
        }
    }

    public void AttackEnd(int attackNumber)
    {
        if (currentState is PlayerAttackState attack)
        {
            attack.OnAnimationEnd(attackNumber);
        }
    }

    public void AttackThrustStart(int attackNumber)
    {
        if (currentState is PlayerAttackState attack)
            attack.OnThrustStart(attackNumber);
    }

    public void AttackThrustEnd(int attackNumber)
    {
        if (currentState is PlayerAttackState attack)
            attack.OnThrustEnd(attackNumber);
    }

    private void OnDisable()
    {
        InputManger.Instance.input.Player.Get().actionTriggered -= OnAction;
    }
}
