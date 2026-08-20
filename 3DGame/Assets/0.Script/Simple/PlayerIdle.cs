using UnityEngine;

public class PlayerIdle : IState
{
    private Player player;
    private IState prevState;
    public PlayerIdle(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        //Debug.Log("가만히");
        player.animator.SetFloat("Speed", 0);
        player.animator.SetTrigger("ReturnIdle");
    }

    public void Tick()
    {
        if (player.movement != Vector3.zero)
        {
            player.ChangeState(new PlayerMoveState(player, this));
        }
    }

    public void Exit()
    {
        //Debug.Log("가만히 나감");
    }

    
}
