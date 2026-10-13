using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayerDashAttackState : IState
{
    Player player;

    private bool isCharging;

    private float time;
    private float charegeDelay = 0.2f;

    private Vector3 attackDir;
    private Vector3 pos;
    private Collider[] col;
    private HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();
    public PlayerDashAttackState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.ControllRock(true);
        player.animator.SetBool("DashAttack", true);
        player.IsRushing = false;
    }
    
    public void Exit()
    {
        isCharging = false;
        player.IsRushing = false;
        player.animator.speed = 1f;
    }

    public void Tick()
    {
        Debug.Log("isCharging" + isCharging);
        Debug.Log("IsRushing" + player.IsRushing);
        if (isCharging)
        {
            time -= Time.deltaTime;
            if(time <= 0f)
            {
                isCharging = false;
                player.animator.speed = 1f;
            }
            return;
        }
        if (!player.IsRushing)
            return;
        player.controll.Move(attackDir * player.stat.DashAttackSpeed * Time.deltaTime);
        pos = player.transform.position;
        pos.y = 2f;
        col = Physics.OverlapSphere(pos, 1f, LayerMask.GetMask("Monster"));
        foreach (var target in col)
        {
            IDamageable damageable = target.GetComponentInParent<IDamageable>();

            if (damageable == null || !damagedTargets.Add(damageable))
                continue;

            damageable.TakeDamage(player.data.DashAttackDamage);
        }
    }

    public void OnChargeStart()
    {
    }

    public void OnChargeEnd()
    {
        time = charegeDelay;
        isCharging = true;
        attackDir = player.transform.forward;
        player.animator.speed = 0f;
    }

    public void OnRushStart()
    {
        player.IsRushing = true;
    }

    public void OnRushEnd()
    {
        player.IsRushing = false;
    }

    public void OnRushAttackEnd()
    {
        player.ControllRock(false);
        player.animator.SetBool("DashAttack", false);
        player.ChangeState(PlayerState.idleState);
    }
}
