using UnityEngine;

public class PlayerJumpState : IState
{
    private Player player;
    private IState prevState;
    private Rigidbody rb;

    public PlayerJumpState(Player player, IState prevState, Rigidbody rb)
    {
        this.player = player;
        this.prevState = prevState;
        this.rb = rb;
    }

    public void Enter()
    {
        rb.linearVelocity = new Vector3(0f, Mathf.Sqrt(1f * 9.81f * player.model.JumpForce), 0f);
        player.ChangeState(prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
