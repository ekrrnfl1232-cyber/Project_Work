using UnityEngine;

public class BossAreaAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    public BossAreaAttack(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        time = 0f;
        timeDuration = 1f;
        boss.bossAni.SetTrigger("Attack");
        foreach (var tar in boss.stats.TargetCheck)
        {
            if (tar.TryGetComponent<IDamageable>(out IDamageable damage))
            {
                int AreaDamage = boss.data.Mdamage + 5;
                damage.TakeDamage(AreaDamage);
                DamageFontManager.Instance.CreateText(AreaDamage, tar.transform.position);
                boss.AreaCool.Start();
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
