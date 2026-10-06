using UnityEngine;

public class BossNormalAttackState : IState
{
    Boss boss;
    private float time;
    private float timeDuration;
    public BossNormalAttackState(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        time = 0;
        timeDuration = 1f;
        boss.NomalCool.Start();
        boss.agent.SetDestination(boss.target.position);
        boss.bossAni.SetTrigger("Attack");
        foreach(var tar in boss.stats.TargetCheck)
        {
            if(tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                if (tar.CompareTag("Player"))
                {
                    damage.TakeDamage(boss.data.Mdamage);
                    DamageFontManager.Instance.CreateText(boss.data.Mdamage, tar.transform.position);
                }
            }
        }
    }

    public void Exit()
    {
    }

    public void Tick()
    {
        time += Time.deltaTime;

        if(time >= timeDuration)
        {
            boss.ChangeState(boss.PrevState);
            boss.agent.ResetPath();
        }
    }
}
