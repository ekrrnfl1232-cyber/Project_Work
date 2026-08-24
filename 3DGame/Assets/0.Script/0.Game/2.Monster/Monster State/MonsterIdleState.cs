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
        monster.MonAni.SetTrigger("Idle");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if(monster.isFind)
            monster.ChangeState(new MonsterMoveState(monster, this));
        if (monster.attackCool.IsReady && monster.Mmodel.TargetDis < 1.5f)
        {
            Debug.Log($"{monster.name}АјАн");
            monster.ChangeState(new MonsterAttackState(monster, this));
        }
    }
}
