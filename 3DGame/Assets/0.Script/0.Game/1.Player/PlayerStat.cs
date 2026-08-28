using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private int maxHP;

    [SerializeField]
    private int baseAttack;

    [SerializeField]
    private int baseDefence;

    [SerializeField]
    private float baseSpeed;

    public int MaxHP { get { return maxHP; } private set { maxHP = value; } }
    public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }
    public int BaseDefence { get { return baseDefence; } private set { baseDefence = value; } }
    public float BaseSpeed { get { return baseSpeed; } private set { baseSpeed = value; } }

    private void Awake()
    {
        ResetStat();
    }

    public void ResetStat()
    {
        MaxHP = maxHP;
        BaseAttack = baseAttack;
        BaseDefence = baseDefence;
        BaseSpeed = baseSpeed;
    }

    public int TotalDamage()
    {
        return EquipSystem.Instance.EquipTotalDamage();
    }

    public float TotalSpeed()
    {
        return BaseSpeed + EquipSystem.Instance.EquipTotalSpeed();
    }
}
