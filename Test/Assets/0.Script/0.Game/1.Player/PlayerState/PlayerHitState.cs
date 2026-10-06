using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitState : IState
{
    private Player player;

    public PlayerHitState(Player player)
    {
        this.player = player;
        
    }
    public void Enter()
    {
        player.animator.SetTrigger("Hit");
        player.view.HpUpdate(player.stat.Hp, player.data.Maxhp);
        player.ChangeState(player.prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    { 
    }
}
