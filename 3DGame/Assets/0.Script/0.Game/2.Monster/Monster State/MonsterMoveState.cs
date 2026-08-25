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
        monster.agent.speed = monster.data.MoveSpeed;
        Debug.Log("목표물로 가는중");
        monster.MonAni.SetTrigger("Walk");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        monster.agent.SetDestination(monster.target.position);
        
        if(monster.Mmodel.TargetDis <= 1.5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState(prevState);
        }
        float StartDis = Vector3.Distance(monster.transform.position, monster.Mmodel.StartPos);

        if (!monster.isFind && StartDis >= monster.data.SpawnRange)
        {
            monster.agent.ResetPath();
            monster.ChangeState(new MonsterPatrolState(monster, this));
        }
    }
}
