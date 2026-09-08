using UnityEngine;

public class BossChaseState : IState
{
    Boss boss;

    public BossChaseState (Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        boss.agent.SetDestination(boss.target.position);

        if (boss.TargetDis < 1f)
        {
            boss.agent.ResetPath();
            boss.ChangeState(BossState.Idle);
        }
    }
}
