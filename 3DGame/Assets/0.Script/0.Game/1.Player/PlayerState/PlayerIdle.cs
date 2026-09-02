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
        player.animator.SetFloat("Speed", 0);
        player.animator.SetTrigger("ReturnIdle");
    }

    public void Tick()
    {
        if (player.model.Movement != Vector3.zero && !player.view.isOnInventory)
        {
            player.ChangeState(PlayerState.moveState);
        }
    }

    public void Exit()
    {
    }

    
}
