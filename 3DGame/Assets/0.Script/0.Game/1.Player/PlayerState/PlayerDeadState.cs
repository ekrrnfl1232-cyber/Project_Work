using UnityEngine;

public class PlayerDeadState : IState
{
    private Player player;

    public PlayerDeadState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Player Dead");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
