using UnityEngine;

public class PlayerDashState : IState
{
    private Player player;

    private float timer;
    private Vector3 dashDir;

    public PlayerDashState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.SetBool("ShieldRush", true);
        timer = 0f;
        dashDir = player.transform.forward;
    }
    
    public void Exit()
    {
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        player.controll.Move(dashDir * player.data.DashForce * Time.deltaTime);

        if(timer >= 0.2f)
        {
            player.animator.SetBool("ShieldRush", false);
            player.ChangeState(PlayerState.idleState);
        }

    }

   

}
