using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHitState : IState
{

    private int damage, hp, maxhp;
    private IState prevState;
    private Player player;

    public PlayerHitState(Player player, IState preState,object obj)
    {
        this.player = player;
        this.prevState = preState;
        
    }
    public void Enter()
    {
        Debug.Log("Player Hit Enter");
        hp -= damage;
        if(hp > 0)
        {
            player.ChangeState(prevState);
        }
        else
        {
            //player.ChangeState(PlayerDeadState);
        }
    }

    public void Exit()
    {
        Debug.Log("Player Hit Exit");
    }

    public void Tick()
    {

    }
}
