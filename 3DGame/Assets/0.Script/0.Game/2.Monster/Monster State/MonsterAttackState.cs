using UnityEngine;

public class MonsterAttackState : IState
{
    Monster monster;
    IState prevState;
    public MonsterAttackState(Monster monster, IState prevState)
    {
        this.monster = monster;
        this.prevState = prevState;
    }

    public void Enter()
    {
        monster.MonAni.SetTrigger("Idle");
        Vector3 posAttack = monster.transform.position + monster.transform.forward * 1f;
        posAttack.y += 0.7f;
        Collider[] targetCheck = Physics.OverlapBox(posAttack, new Vector3(1.2f, 1.4f, 0.8f), monster.transform.rotation, LayerMask.GetMask("Player"));
        foreach (var tar in targetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                damage.TakeDamage(monster.Mmodel.mDamage);
                DamageFontManager.Instance.CreateText(monster.Mmodel.mDamage, tar.transform.position);
                monster.attackCool.Start();
                break;
            }
        }
        monster.ChangeState(prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
