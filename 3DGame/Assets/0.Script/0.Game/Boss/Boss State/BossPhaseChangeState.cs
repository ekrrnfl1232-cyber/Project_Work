using UnityEngine;

public class BossPhaseChangeState : IState
{
    Boss boss;

    public BossPhaseChangeState(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        Debug.Log("페이즈 전환");
        boss.ChangeState(boss.PrevState);
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        
    }
}
