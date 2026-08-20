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
        Debug.Log("점프 들어옴");
        player.animator.SetTrigger("HeavyAttack");
        rb.linearVelocity = new Vector3(0f, Mathf.Sqrt(1f * 9.81f * player.jumpForce), 0f);
        player.ChangeState(prevState);
    }

    public void Exit()
    {
        Debug.Log("점프 나감");
    }

    public void Tick()
    {
    }
}
