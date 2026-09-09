using UnityEngine;

[System.Serializable]
public class MonsterMoveState : IState
{
    Monster monster;

    Vector3 startPos;
    float distan;
    public MonsterMoveState(Monster monster)
    {
        this.monster = monster;
    }
    public void Enter()
    {
        monster.agent.speed = monster.data.MoveSpeed;
        Debug.Log("목표물로 가는중");
        monster.MonsterAni.SetTrigger("Walk");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        monster.agent.SetDestination(monster.target.position);
        
        if(monster.Model.TargetDis <= 1.5f)
        {
            monster.agent.ResetPath();
            monster.ChangeState("idleState");
        }
        

        if (!monster.IsFind && monster.StartDis >= monster.data.SpawnRange)
        {
            monster.agent.ResetPath();
            monster.ChangeState("patrolState");
        }
    }
}
