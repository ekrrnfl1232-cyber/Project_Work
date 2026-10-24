using UnityEngine;

public class EnemyChaseState : IState
{
    private EnemyController enemy;

    private Vector2 dir;

    public EnemyChaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
        enemy.model.MoveDirection = Vector2.zero;
    }

    public void Tick()
    {
        if(enemy.CanAttack)
        {
            enemy.ChangeState(EnemyState.Attack);
            return;
        }
        dir = enemy.Area == null ? Vector2.zero : enemy.Area.GetDirection(enemy.transform.position);

        enemy.model.MoveDirection = dir;

        Vector3 movement = new Vector3(dir.x, dir.y, 0f);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        enemy.transform.rotation = Quaternion.RotateTowards( enemy.transform.rotation,rotation, 720f * Time.deltaTime );
        enemy.transform.position += movement * enemy.model.MoveSpeed * Time.deltaTime;
    }
}
