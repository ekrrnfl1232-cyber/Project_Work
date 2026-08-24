using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MonsterMoveState : IState
{
    Monster monster;
    IState prevState;

    Vector3 startPos;
    float distan;
    public MonsterMoveState(Monster monster, IState prevState)
    {
        this.monster = monster;
        this.prevState = prevState;
    }
    public void Enter()
    {
        monster.agent.speed = 3.5f;
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        monster.agent.SetDestination(monster.target.position);
        monster.MonAni.SetTrigger("Walk");
        if (!monster.isFind || monster.Mmodel.StartDis >= 5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState(new MonsterPatrolState(monster, prevState));
        }
        if(monster.agent.remainingDistance <= 1f)
        {
            monster.agent.ResetPath();
            monster.ChangeState(prevState);
        }
    }
}
