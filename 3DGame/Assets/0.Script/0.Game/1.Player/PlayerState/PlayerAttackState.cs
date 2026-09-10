using UnityEngine;

public class PlayerAttackState : IState
{
    private Player player;
    public PlayerAttackState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.animator.SetTrigger("Sword01");
        Vector3 posAttack = player.transform.position + player.transform.forward * 1f;
        posAttack.y += 0.5f;
        Collider[] targetCheck = Physics.OverlapBox
            (posAttack, new Vector3(1.4f, 1.4f, 1f), player.transform.rotation, 
            LayerMask.GetMask("Monster"));
        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(player.stat.TotalDamage());
                break;
            }
        }
        player.AtkCool.Start();
        player.ChangeState(PlayerState.idleState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
