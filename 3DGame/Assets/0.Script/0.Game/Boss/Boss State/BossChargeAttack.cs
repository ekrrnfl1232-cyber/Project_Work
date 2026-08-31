using Unity.VisualScripting;
using UnityEngine;

public class BossChargeAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    public BossChargeAttack(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        time = 0f;
        timeDuration = 3f;
        boss.bossAni.SetTrigger("Attack");
        foreach (var tar in boss.stats.TargetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                int ChargeDamage = boss.data.Mdamage + 10;
                damage.TakeDamage(ChargeDamage);
                DamageFontManager.Instance.CreateText(ChargeDamage, tar.transform.position);
                boss.ChargeCool.Start();
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
        }
    }
}
