using UnityEngine;

public class PlayerMoveState : IState
{
    private Player player;
    public PlayerMoveState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        Debug.Log("Player Move Enter");
    }

    public void Exit()
    {
        Debug.Log("Player Move Exit");
    }

    public void Tick()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            player.ChangeState(new PlayerIdle(player));
        }
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        player.transform.Translate(movement* Time.deltaTime * player.moveForce, Space.World);
    }
}
