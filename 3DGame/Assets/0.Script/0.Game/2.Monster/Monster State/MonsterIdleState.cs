using UnityEngine;

public class MonsterIdleState : IState
{
    Monster monster;

    public MonsterIdleState(Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("´ë±âÁß");
        monster.MonAni.SetTrigger("Idle");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (monster.isFind && monster.Mmodel.TargetDis > monster.data.Range)
            monster.ChangeState(new MonsterMoveState(monster, this));

        if (monster.isFind && monster.Mmodel.TargetDis <= monster.data.Range)
        {
            monster.transform.LookAt(monster.target);
            if (monster.attackCool.IsReady)
            {
                monster.ChangeState(new MonsterAttackState(monster, this));
            }
        }
    }
}
