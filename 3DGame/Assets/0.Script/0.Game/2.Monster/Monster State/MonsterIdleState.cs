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
        if(monster.model.Dis > 1.5f)
        {
            monster.ChangeState(new MonsterMoveState(monster, this));
        }

    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
