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
        player.controll.Move(player.model.Movement * player.data.MoveForce * Time.deltaTime);
        if (player.movedir == Vector2.zero)
        {
            player.ChangeState(PlayerState.idleState);
        }
    }
}
