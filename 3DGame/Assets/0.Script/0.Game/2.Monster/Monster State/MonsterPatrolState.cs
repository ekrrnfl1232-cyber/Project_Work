using UnityEngine;

[System.Serializable]
public class MonsterPatrolState : IState
{
    Monster monster;

    public MonsterPatrolState (Monster monster)
    {
        this.monster = monster;
    }

    public void Enter()
    {
        Debug.Log("∫π±Õ¡ﬂ");
        monster.MonsterAni.SetTrigger("Run");
        monster.agent.speed += (int)monster.agent.speed << 2;
        monster.agent.SetDestination(monster.Model.StartPos);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (monster.agent.remainingDistance < 0.5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState("idleState");
        }
        else if (monster.IsFind && monster.Model.TargetDis >= monster.data.Range)
        {
            monster.ChangeState(monster.PrevState);
        }
    }

}
