using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private PlayerData data;

    [SerializeField]
    private int hp;

    [SerializeField]
    private int level;

    [SerializeField]
    private float exp;

    [SerializeField]
    private float maxExp;

    [SerializeField]
    private int baseAttack;

    [SerializeField]
    private int baseDefence;

    [SerializeField]
    private float baseSpeed;



    public int Hp { get { return hp; } set { hp = value; } }
    public int Level {get { return level; } set { level = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }

    public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }
    public int BaseDefence { get { return baseDefence; } private set { baseDefence = value; } }
    public float BaseSpeed { get { return baseSpeed; } private set { baseSpeed = value; } }


    private void Awake()
    {
        Hp = data.Maxhp;
        BaseAttack = data.Wdamage;
        BaseSpeed = data.MoveForce;
        Level = 1;
        Exp = 0;
        MaxExp = 500;
        ResetStat();
    }

    public void ResetStat()
    {
        Hp = data.Maxhp;
        BaseAttack = baseAttack;
        BaseDefence = baseDefence;
        BaseSpeed = baseSpeed;
    }

    public int TotalDamage()
    {
        return EquipSystem.Instance.EquipTotalDamage() + BaseAttack;
    }

    public float TotalSpeed()
    {
        return BaseSpeed + EquipSystem.Instance.EquipTotalSpeed() + BaseSpeed;
    }
}
