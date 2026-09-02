using UnityEngine;

public class PlayerJumpState : IState
{
    private Player player;
    private CharacterController control;

    public PlayerJumpState(Player player, CharacterController control)
    {
        this.player = player;
        this.control = control;
    }

    public void Enter()
    {
        //new Vector3(0f, Mathf.Sqrt(1f * 9.81f * player.data.JumpForce), 0f);
        control.Move(new Vector3(0f, Mathf.Sqrt(1f * 9.81f * player.data.JumpForce), 0f));
        player.ChangeState(PlayerState.idleState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
