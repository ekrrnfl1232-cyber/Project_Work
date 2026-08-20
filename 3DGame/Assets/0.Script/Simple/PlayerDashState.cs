using UnityEngine;

public class PlayerDashState : IState
{
    private Player player;
    private IState prevState;
    private Rigidbody rb;

    public PlayerDashState(Player player, IState prevState, Rigidbody rb)
    {
        this.player = player;
        this.prevState = prevState;
        this.rb = rb;
    }

    public void Enter()
    {
        Debug.Log("대쉬 들옴");
        player.Dash(prevState);
    }
    
    public void Exit()
    {
        Debug.Log("대쉬 나감");
    }

    public void Tick()
    {
    }

   

}
