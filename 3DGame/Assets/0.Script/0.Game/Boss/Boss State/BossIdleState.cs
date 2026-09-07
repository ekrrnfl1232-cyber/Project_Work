using UnityEngine;

public class BossIdleState : IState
{
    Boss boss;
    public BossIdleState(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        boss.bossAni.SetTrigger("Idle");
        Debug.Log("°¡¸¸È÷");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if(boss.IsFind)
        {
            boss.ChangeState(BossState.Chase);
        }
        if(boss.TargetDis < 1.5f)
        {
            boss.ChangeState(BossState.SelectAttack);
        }
    }
}
