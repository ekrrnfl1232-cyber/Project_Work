using UnityEngine;

public class BossAreaAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    GameObject AreaAttack;
    GameObject chargeview;

    public BossAreaAttack(Boss boss)
    {
        this.boss = boss;
    }
    public void Enter()
    {
        AreaAttack = boss.view.AreaAttack(boss.target.position);
        chargeview = AreaAttack.transform.GetChild(0).gameObject;
        Debug.Log(AreaAttack.transform.localScale);
        boss.transform.LookAt(AreaAttack.transform);
        time = 0f;
        timeDuration = 2f;
    }

    public void Exit()
    {
        boss.AreaCool.Start();
    }

    public void Tick()
    {
        time += Time.deltaTime;
        float progress = time / timeDuration;

        chargeview.transform.localScale = Vector3.Lerp(Vector3.zero, AreaAttack.transform.localScale/5, progress);
        

        if (progress >= 1f)
        {
            Collider[] attack = Physics.OverlapSphere(AreaAttack.transform.position, boss.stats.AreaRadius);
            VFXManager.Instance.Show(VFXtype.AreaAttack, AreaAttack.transform);
            foreach (Collider atk in attack)
            {
                if(atk.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    if (atk.CompareTag("Player"))
                    {
                        damage.TakeDamage(boss.data.Mdamage + boss.stats.AreaDamage);
                    }
                }
            }

            boss.ChangeState(BossState.SelectAttack);
            boss.view.DestroyObj(AreaAttack);
        }
    }
}
