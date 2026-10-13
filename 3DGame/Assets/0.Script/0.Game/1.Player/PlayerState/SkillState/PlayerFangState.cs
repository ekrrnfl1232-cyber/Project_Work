using UnityEngine;

public class PlayerFangState : IState
{
    private Player player;

    public PlayerFangState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Cool.Start(PlayerCool.fangAttack);
        player.ChangeState(PlayerState.idleState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
