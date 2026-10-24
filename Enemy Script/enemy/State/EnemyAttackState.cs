using UnityEngine;

public class EnemyAttackState : IState
{
    private EnemyController enemy;
    public EnemyAttackState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
    }

    public void Exit()
    {

    }

    public void Tick()
    {
        if (!enemy.CanKeepAttack)
        {
            enemy.ChangeState(EnemyState.Chase);
            return;
        }

        if (!enemy.CanAttack)
            return;

        Vector2 dir = enemy.Area.Target.position - enemy.transform.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            enemy.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        switch (enemy.Type)
        {
            case EnemyAttackType.Melee:
                break;
            case EnemyAttackType.Ranged:
                break;
        }

    }
}
