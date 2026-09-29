using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    idle,
    move,
    dash
}

public class Player : MonoBehaviour
{

    private IState currentState;
    private PlayerState stateKey;

    private Dictionary<PlayerState, IState> states = new Dictionary<PlayerState, IState>();

    private float moveSpeed = 20f;
    private float dashSpeed = 50f;

    public Rigidbody2D rb;

    public Animator anim;

    private Vector2 moveDir;
    private PlayerCooldown cool = new PlayerCooldown();
    public PlayerCooldown Cool { get{ return cool; } }
    private bool isDash;

    private void Start()
    {
        InputManager.Instance.input.Player.Get().actionTriggered += OnAction;
        SettingState();
        isDash = false;
    }

    void Update()
    {
        Cool.TIck(Time.deltaTime);

        if (InputManager.Instance.input.Player.Move.IsPressed())
        {
            moveDir = InputManager.Instance.input.Player.Move.ReadValue<Vector2>();
            anim.SetFloat("DirX", moveDir.x);
            anim.SetFloat("DirY", moveDir.y);
        }
        else
        {
            anim.SetTrigger("Idle");
        }
        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        if(InputManager.Instance.input.Player.Move.IsPressed())
        {
            anim.SetTrigger("Run");
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    public void ChangeState(PlayerState key)
    {
        currentState?.Exit();
        currentState = states[key];
        currentState?.Enter();
    }

    private void SettingState()
    {
        states = new Dictionary<PlayerState, IState>
        {
            { PlayerState.dash, new PlayerDashState(this) }
        };
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        switch (context.action.name)
        {
            case "Sprint":
                if (Cool.IsReady(PlayerCool.Dash))
                {
                    ChangeState(PlayerState.dash);
                }
                break;
        }
    }
}
