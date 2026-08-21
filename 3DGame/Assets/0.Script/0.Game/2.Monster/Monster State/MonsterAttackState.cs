using UnityEngine;

public class MonsterAttackState : IState
{
    Monster monster;
    public MonsterAttackState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Vector3 posAttack = monster.transform.position + monster.transform.forward * 1f;
        posAttack.y += 0.7f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.2f, 1.4f, 0.8f), monster.transform.rotation, LayerMask.GetMask("Player"));

        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(monster.model.mDamage);
                break;
            }
        }
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
