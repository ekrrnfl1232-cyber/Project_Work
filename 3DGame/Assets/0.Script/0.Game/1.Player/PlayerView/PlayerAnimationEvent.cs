using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerAttackState attackState;
    private PlayerDashAttackState dashAttackState;

    public void Init(Player player)
    {
        attackState = (PlayerAttackState)player.States[PlayerState.attackState];
        dashAttackState = (PlayerDashAttackState)player.States[PlayerState.dashAttackState];
    }
    public void AttackHit(int attackNumber) => attackState.OnHit(attackNumber);
    public void AttackComboCheck(int attackNumber) => attackState.OnComboCheck(attackNumber);

    public void AttackEnd(int attackNumber) => attackState.OnAnimationEnd(attackNumber);

    public void AttackThrustStart(int attackNumber) => attackState.OnThrustStart(attackNumber);

    public void AttackThrustEnd(int attackNumber) => attackState.OnThrustEnd(attackNumber);

    public void RushChargeStart() => dashAttackState.OnChargeStart();

    public void RushChargeEnd() => dashAttackState.OnChargeEnd();

    public void RushStart() => dashAttackState.OnRushStart();

    public void RushEnd() => dashAttackState.OnRushEnd();

    public void RushAttackEnd() => dashAttackState.OnRushAttackEnd();
}
