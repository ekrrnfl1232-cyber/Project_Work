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
        
    }

    public void Tick()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            player.ChangeState(new PlayerMoveState(player));
        }
    }

    public void Exit()
    {
        
    }

    
}
