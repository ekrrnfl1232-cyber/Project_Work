using UnityEngine;

public class PlayerRootBindState : IState
{
    private Player player;

    public PlayerRootBindState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Cool.Start(PlayerCool.RootBind);
        player.ChangeState(PlayerState.idleState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
