using UnityEngine;

public class PlayerDashState : IState
{
    private Player player;
    private CharacterController control;

    public PlayerDashState(Player player, CharacterController control)
    {
        this.player = player;
        this.control = control;
    }

    public void Enter()
    {
        Debug.Log("대쉬 들옴");
        player.animator.SetBool("ShieldRush", true); //Vector3.forward * player.data.DashForce, ForceMode.Velocity
        control.Move(Vector3.forward * player.data.DashForce * Time.deltaTime);
        player.Invoke("StopDash", 0.2f);
    }
    
    public void Exit()
    {
        Debug.Log("대쉬 나감");
    }

    public void Tick()
    {
    }

   

}
