using UnityEngine;

public class MonsterPatrolState : IState
{
    Monster monster;
    IState prevState;

    public MonsterPatrolState (Monster monster, IState prevState)
    {
        this.monster = monster;
        this.prevState = prevState;
    }

    public void Enter()
    {
        Debug.Log("∫π±Õ¡ﬂ");
        monster.MonAni.SetTrigger("Run");
        monster.agent.speed += (int)monster.agent.speed << 2;
        monster.agent.SetDestination(monster.Mmodel.StartPos);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (monster.agent.remainingDistance < 0.5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState(new MonsterIdleState(monster));
        }
        else if (monster.isFind && monster.Mmodel.TargetDis >= monster.data.Range)
        {
            monster.ChangeState(prevState);
        }
    }

}
