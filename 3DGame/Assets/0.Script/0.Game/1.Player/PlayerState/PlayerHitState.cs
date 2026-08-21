using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitState : IState
{

    private int damage;
    private IState prevState;
    private Player player;

    public PlayerHitState(Player player, IState preState, int damage)
    {
        this.player = player;
        this.prevState = preState;
        this.damage = damage;
        
    }
    public void Enter()
    {
        player.animator.SetTrigger("Hit");
        player.model.HP -= damage;
        Debug.Log("Player 타격 받음");
        Debug.Log($"남은 체력 : {player.model.HP}");
        player.view.HpUpdate(player.model.HP, player.model.MaxHP);
        if (player.model.HP <= 0)
        {
            Debug.Log("Player Dead");
        }
        player.ChangeState(prevState);
    }

    public void Exit()
    {
        Debug.Log("피격 나감");
    }

    public void Tick()
    {
    }
}
