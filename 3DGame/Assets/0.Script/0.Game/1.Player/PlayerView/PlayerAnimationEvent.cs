using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerAttackState attackState;

    public void Init(Player player)
    {
        attackState = (PlayerAttackState)player.States[PlayerState.attackState];
    }
    public void AttackHit(int attackNumber) => attackState.OnHit(attackNumber);
    public void AttackComboCheck(int attackNumber) => attackState.OnComboCheck(attackNumber);

    public void AttackEnd(int attackNumber) => attackState.OnAnimationEnd(attackNumber);

    public void AttackThrustStart(int attackNumber) => attackState.OnThrustStart(attackNumber);

    public void AttackThrustEnd(int attackNumber) => attackState.OnThrustEnd(attackNumber);
}
