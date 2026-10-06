using UnityEngine;

public class PlayerIdle : IState
{
    private Player player;
    public PlayerIdle(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetFloat("Speed", 0);
        player.animator.SetTrigger("ReturnIdle");
    }

    public void Tick()
    {
        if (player.stat.MoveDir != Vector2.zero && player.IsTargeting == false)
        {
            player.ChangeState(PlayerState.moveState);
        }
    }

    public void Exit()
    {
    }

    
}
