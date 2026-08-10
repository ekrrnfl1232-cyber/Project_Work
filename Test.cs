using System.Collections.Generic;
using UnityEngine;

public interface AttackDamage
{
    void Attack(Transform attackerTransform);
}

public class Sword : AttackDamage
{
    public void Attack(Transform attackerTransform)
    {
        Debug.Log("30데미지");
    }
}

public class Bow : AttackDamage
{
    public void Attack(Transform attackerTransform)
    {
        Debug.Log("15데미지");
    }
}

public class Test : MonoBehaviour
{
    private AttackDamage atd;

    public void SetWeapon (AttackDamage newWeapon)
    {
        atd = newWeapon;
    }

    private void Start()
    {
        SetWeapon(new Sword());
    }

    void Update()
    {

    }
}
