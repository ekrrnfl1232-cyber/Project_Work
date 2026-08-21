using UnityEngine;

public class PlayerMoveState : IState
{
    private Player player;
    private IState prevState;
    public PlayerMoveState(Player player, IState prevState)
    {
        this.player = player;
        this.prevState = prevState;
    }
    public void Enter()
    {
        player.animator.SetFloat("Speed", player.moveForce);
    }

    public void Exit()
    {

    }

    public void Tick()
    {

        player.model.Movement.Normalize();
        //transform.position += movement * Time.deltaTime * speed;
        player.transform.Translate(player.model.Movement * Time.deltaTime * player.model.MoveForce, Space.World);
        if (player.model.Movement == Vector3.zero)
        {
            player.ChangeState(new PlayerIdle(player));
        }
    }
}
