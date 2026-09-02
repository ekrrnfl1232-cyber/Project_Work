using UnityEngine;

public class PlayerMoveState : IState
{
    private Player player;
    private CharacterController control;
    public PlayerMoveState(Player player, CharacterController control)
    {
        this.player = player;
        this.control = control;
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

        player.model.Movement.Normalize();
        //transform.position += movement * Time.deltaTime * speed;
        ///player.transform.position + player.model.Movement * player.data.MoveForce * Time.deltaTime
        control.Move(player.movement * player.data.MoveForce * Time.deltaTime);
        if (player.model.Movement == Vector3.zero)
        {
            player.ChangeState(PlayerState.idleState);
        }
    }
}
