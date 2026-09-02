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
        Debug.Log("Player 타격 받음");
        Debug.Log($"남은 체력 : {player.model.HP}");
        player.view.HpUpdate(player.model.HP, player.data.Maxhp);
        player.ChangeState(player.prevState);
    }

    public void Exit()
    {
        Debug.Log("피격 나감");
    }

    public void Tick()
    { 
    }
}
