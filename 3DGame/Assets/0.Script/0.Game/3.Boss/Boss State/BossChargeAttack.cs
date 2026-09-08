using Unity.VisualScripting;
using UnityEngine;

public class BossChargeAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    private GameObject ChargeAttack;
    private GameObject ChargeView;

    public BossChargeAttack(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        ChargeAttack = boss.view.ChargeAttack();
        ChargeView = ChargeAttack.transform.GetChild(0).gameObject;
        boss.transform.LookAt(ChargeAttack.transform);
        boss.transform.LookAt(ChargeView.transform);
        time = 0f;
        timeDuration = 3f;
    }

    public void Exit()
    {
        boss.ChargeCool.Start();
    }

    public void Tick()
    {
        time += Time.deltaTime;
        float progress = time / timeDuration;

        ChargeView.transform.localScale = new Vector3(1f, Mathf.Lerp(0f, 1f, progress), 1f);

        if(progress >= 1f)
        {
            Collider[] attack = Physics.OverlapBox(ChargeAttack.transform.position, ChargeAttack.transform.localScale*2);
            foreach(Collider atk in attack)
            {
                if(atk.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    damage.TakeDamage(boss.data.Mdamage + boss.stats.ChargeDamage);
                }
            }
        }

        boss.ChangeState(BossState.SelectAttack);
        boss.view.DestroyObj(ChargeAttack);
    }
}
