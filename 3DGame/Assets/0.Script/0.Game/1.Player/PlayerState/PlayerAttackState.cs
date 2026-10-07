using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    private Player player;

    private int comboIndex;
    private bool attackQueue;
    private bool hitApplied;
    private bool comboCheck;

    private float thrustDistance = 1.5f;
    private float thrustSpeed = 7f;

    private Vector3 thrustDir;
    private float thrustRemain;
    private bool thrustStart;

    private HashSet<IDamageable> damagedTargets = new();
    public PlayerAttackState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        BeginAttack(1);
    }

    public void Exit()
    {
        thrustRemain = 0f;
    }

    public void Tick()
    {
        if (comboIndex != 3 || thrustRemain <= 0f)
            return;

        float distance = Mathf.Min(
            thrustSpeed * Time.deltaTime,
            thrustRemain
        );

        player.controll.Move(thrustDir * distance);
        thrustRemain -= distance;
    }

    public void QueueAttack()
    {
        if(comboIndex < 3 && !comboCheck)
        {
            attackQueue = true;
        }
    }

    private void BeginAttack(int index)
    {
        comboIndex = index;
        attackQueue = false;
        hitApplied = false;
        comboCheck = false;
        damagedTargets.Clear();

        thrustDir = player.transform.forward;
        thrustRemain = 0f;
        thrustStart = false;

        // 이동 값과 이전 대기 트리거가 공격을 일찍 끝내지 않도록 정리한다.
        player.animator.SetFloat("Speed", 0f);
        player.animator.ResetTrigger("ReturnIdle");
        player.animator.ResetTrigger("Sword01");
        player.animator.ResetTrigger("Sword02");
        player.animator.ResetTrigger("Sword03");

        player.animator.SetTrigger($"Sword0{comboIndex}");
    }

    public void OnHit(int attackNumber)
    {
        if (attackNumber != comboIndex || hitApplied)
            return;
        hitApplied = true;

        Vector3 position = player.transform.position + player.transform.forward * 1f;
        position.y += 0.5f;

        Collider[] targets = Physics.OverlapBox(position, new Vector3(1.4f, 1.4f, 1f), player.transform.rotation, LayerMask.GetMask("Monster"));

        foreach (var target in targets)
        {
            IDamageable damageable = target.GetComponentInParent<IDamageable>();

            if(damageable == null || !damagedTargets.Add(damageable))
                continue;

            damageable.TakeDamage(player.stat.TotalDamage());
        }
    }

    public void OnComboCheck(int attackNumber)
    {
        if (attackNumber != comboIndex || comboCheck)
            return;
        comboCheck = true;

        if(attackQueue && comboIndex < 3)
        {
            BeginAttack(comboIndex + 1);
        }
    }

    public void OnAnimationEnd(int attackNumber)
    {
        if(attackNumber != comboIndex)
            return;

        player.Cool.Start(PlayerCool.Attack);
        player.ChangeState(PlayerState.idleState);
    }
    public void OnThrustStart(int attackNumber)
    {
        Debug.Log($"전진 이벤트: {attackNumber}, 현재 콤보: {comboIndex}");

        if (attackNumber != comboIndex ||
            comboIndex != 3 ||
            thrustStart)
            return;

        thrustStart = true;
        thrustRemain = thrustDistance;
    }

    public void OnThrustEnd(int attackNumber)
    {
        if (attackNumber != comboIndex || comboIndex != 3)
            return;

        thrustRemain = 0f;
    }
}
