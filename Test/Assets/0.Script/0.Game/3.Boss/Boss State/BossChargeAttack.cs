using Unity.VisualScripting;
using UnityEngine;

public class BossChargeAttack : IState
{

    Boss boss;
    private float time;
    private float timeDuration;

    private GameObject ChargeAttack;
    private GameObject ChargeView;
    private Vector3 pos;

    public BossChargeAttack(Boss boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        boss.transform.LookAt(boss.target);
        ChargeAttack = boss.view.ChargeAttack();
        ChargeView = ChargeAttack.transform.GetChild(0).gameObject;
        SpriteRenderer sr = ChargeAttack.GetComponentInChildren<SpriteRenderer>();
        pos = sr.transform.TransformPoint(sr.sprite.bounds.center);
        time = 0f;
        timeDuration = 3f;
    }

    public void Exit()
    {
        if(ChargeAttack != null)
            boss.view.DestroyObj(ChargeAttack);
        boss.ChargeCool.Start();
    }

    public void Tick()
    {
        time += Time.deltaTime;
        float progress = time / timeDuration;

        ChargeView.transform.localScale = new Vector3(1f, Mathf.Lerp(0f, 1f, progress), 1f);

        if(progress >= 1f)
        {
            Collider[] attack = Physics.OverlapBox(pos, new Vector3(5f, 2f, 7f) * 0.5f, boss.transform.rotation);
            VFXManager.Instance.Show(VFXtype.ChargeAttack, ChargeAttack.transform);
            foreach (Collider atk in attack)
            {
                if(atk.TryGetComponent<IDamageable>(out IDamageable damage))
                {
                    if (atk.CompareTag("Player"))
                    {
                        damage.TakeDamage(boss.data.Mdamage + boss.stats.ChargeDamage);
                    }
                }
            }
            boss.view.DestroyObj(ChargeAttack);
            boss.ChangeState(BossState.SelectAttack);
        }
        
    }
}
