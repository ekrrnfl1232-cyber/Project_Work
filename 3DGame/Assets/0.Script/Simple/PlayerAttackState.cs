using UnityEngine;

public class PlayerAttackState : IState
{
    private Player player;
    private IState prevState;
    public PlayerAttackState(Player player, IState prevState)
    {
        this.player = player;
        this.prevState = prevState;
    }
    public void Enter()
    {
        player.animator.SetTrigger("Sword01");
        Vector3 posAttack = player.transform.position + player.transform.forward * 1f;
        posAttack.y += 0.5f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.4f, 1.4f, 1f), player.transform.rotation, LayerMask.GetMask("Monster"));
        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(player.wDamage);
                DamageFontManager.Instance.CreateText(pos:tar.transform.position, damage:1);
                break;
            }
        }
        player.ChangeState(prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
