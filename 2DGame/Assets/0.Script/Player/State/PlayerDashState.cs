using UnityEngine;

public class PlayerDashState : IState
{
    Player player;

    public PlayerDashState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Vector2 dashDir = InputManager.Instance.input.Player.Move.ReadValue<Vector2>();
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
