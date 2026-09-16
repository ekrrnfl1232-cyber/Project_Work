using UnityEngine;

public class PlayerMoveState : IState
{
    private Player player;
    public PlayerMoveState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.animator.SetFloat("Speed", player.data.MoveForce);
    }

    public void Exit()
    {

    }

    public void Tick()
    {
        player.controll.Move(player.stat.Movement * player.stat.TotalSpeed() * Time.deltaTime);
        if (player.stat.MoveDir == Vector2.zero)
        {
            player.ChangeState(PlayerState.idleState);
        }
    }
}
