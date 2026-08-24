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
        monster.MonAni.SetTrigger("Run");
        monster.agent.speed = Mathf.MoveTowards(monster.agent.speed, 7f, 5f *Time.deltaTime);
        monster.agent.SetDestination(monster.Mmodel.StartPos);
        monster.ChangeState(prevState);
    }

    public void Exit()
    {
    }

    public void Tick()
    {  
    }

}
