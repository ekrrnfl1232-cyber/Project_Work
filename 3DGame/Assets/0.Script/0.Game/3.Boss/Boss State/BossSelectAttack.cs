using UnityEngine;

public class BossSelectAttack : IState
{
    Boss boss;
    public BossSelectAttack(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        boss.bossAni.SetTrigger("Idle");
        Debug.Log("공격 선택");
        Vector3 posAttack = boss.transform.position + boss.transform.forward * 1.5f;
        posAttack.y += 1f;
        boss.stats.TargetCheck = Physics.OverlapBox(posAttack, new Vector3(2f, 1.4f, 1.5f), boss.transform.rotation, LayerMask.GetMask("Player"));
        
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        if (boss.TargetDis > 2f)
        {
            boss.ChangeState(BossState.Idle);
            return;
        }
        if (boss.ChargeCool.IsReady)
        {
            boss.ChangeState(BossState.ChargeAttack);
        }
            /*if (boss.NomalCool.IsReady || boss.ChargeCool.IsReady || boss.AreaCool.IsReady)
            {
                if (boss.stats.Phase == 1)
                {

                    boss.ChangeState((BossState)Random.Range(3, 5));
                }
                else if (boss.stats.Phase == 2)
                {
                    boss.ChangeState((BossState)Random.Range(3, 6));
                }
            }*/
    }
}
