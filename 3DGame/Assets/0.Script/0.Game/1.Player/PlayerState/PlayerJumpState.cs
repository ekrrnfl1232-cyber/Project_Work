using UnityEngine;

public class PlayerJumpState : IState
{
    private Player player;
    private float timer;

    public PlayerJumpState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetFloat("Speed", 0);
        player.animator.SetTrigger("ReturnIdle");
        player.model.VerticalVelo = Mathf.Sqrt(player.data.JumpHeight * player.model.gravity.y * -2f);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (player.controll.isGrounded && player.model.VerticalVelo < 0f)
            player.ChangeState(PlayerState.idleState);
    }
}
