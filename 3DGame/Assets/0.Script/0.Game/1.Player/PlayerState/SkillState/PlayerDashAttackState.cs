using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class PlayerDashAttackState : IState
{
    Player player;

    private bool isCharging;
    private bool isRushing;

    private float elaps;

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
    }
    
    public void Exit()
    {
        player.ControllRock(false);
        player.RotationLock = false;
        player.animator.speed = 1f;
        damagedTargets.Clear();
    }

    public void Tick()
    {
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
        if (isRushing)
        {
            OnRush();
            OnHit();
        }
    }

    public void OnChargeStart()
    {
    }

    public void OnChargeEnd()
    {
        attackDir = player.transform.forward;
        player.animator.speed = 0f;
        time = charegeDelay;
        player.RotationLock = true;
        isCharging = true;
    }

    public void OnRushStart()
    {
        elaps = 0f;
        isRushing = true;
    }

    public void OnRushEnd()
    {
        isRushing = false;
    }

    public void OnRushAttackEnd()
    {
        player.Cool.Start(PlayerCool.dashAttack);
        player.RotationLock = false;
        player.ControllRock(false);
        damagedTargets.Clear();
        player.animator.SetBool("DashAttack", false);
        player.ChangeState(PlayerState.idleState);
    }

    private void OnRush()
    {
        float dashAttackSpeed = 10f;
        float cruiseDuration = 0.2f;
        float brakeDuration = 0.1f;

        float GetDistance(float elapsed)
        {
            float cruiseTime = Mathf.Clamp(elapsed, 0f, cruiseDuration);
            float brakeTime = Mathf.Clamp(elapsed - cruiseDuration, 0f, brakeDuration);

            return dashAttackSpeed * cruiseTime + dashAttackSpeed * 
                (brakeTime - brakeTime * brakeTime / (2f * brakeDuration));
        }
        float nextTime = elaps + Time.deltaTime;
        float distance = GetDistance(nextTime) - GetDistance(elaps);

        player.controll.Move(attackDir * distance);
        elaps = nextTime;
    }

    private void OnHit()
    {
        pos = player.transform.position;
        pos.y = 1.3f;
        col = Physics.OverlapSphere(pos, 1f, LayerMask.GetMask("Monster"));
        foreach (var target in col)
        {
            IDamageable damageable = target.GetComponentInParent<IDamageable>();

            if (damageable == null || !damagedTargets.Add(damageable))
                continue;

            damageable.TakeDamage(player.data.DashAttackDamage);
        }
    }
}
