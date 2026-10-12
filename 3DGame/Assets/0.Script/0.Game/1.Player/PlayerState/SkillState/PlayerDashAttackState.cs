using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayerDashAttackState : IState
{
    Player player;

    private bool isDash;

    private float timer;
    private Vector3 attackDir;
    private Collider[] col;
    private HashSet<IDamageable> damagedTargets = new();
    public PlayerDashAttackState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.animator.SetBool("DashAttack", true);
        timer = 0f;
        attackDir = player.transform.forward;
    }
    
    public void Exit()
    {
    }

    public void Tick()
    {
        timer += Time.deltaTime;
        player.controll.Move(attackDir * player.stat.DashAttackSpeed * Time.deltaTime);
        if (isDash)
        {
            Vector3 pos = player.transform.position + player.transform.forward;
            pos.y = 0.5f;
            col = Physics.OverlapSphere(pos, 5f, LayerMask.GetMask("Monster"));
            foreach (var target in col)
            {
                IDamageable damageable = target.GetComponentInParent<IDamageable>();

                if (damageable == null || !damagedTargets.Add(damageable))
                    continue;

                damageable.TakeDamage(player.stat.TotalDamage());
            }
        }
        if (timer >= 0.2f)
        {
            player.animator.SetBool("DashAttack", false);
            player.Cool.Start(PlayerCool.dashAttack);
            player.ChangeState(player.prevState);
        }
    }

    public void IsDashAttackStart()
    {
        isDash = true;
    }

    public void IsDashAttackEnd()
    {
        isDash = false;
    }
}
