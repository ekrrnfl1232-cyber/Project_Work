using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private PlayerData data;

    [SerializeField]
    private int hp;

    [SerializeField]
    private static int level;

    [SerializeField]
    private static float exp;

    [SerializeField]
    private static float maxExp;

    [SerializeField]
    private int baseAttack;

    [SerializeField]
    private int baseDefence;

    [SerializeField]
    private float baseSpeed;

    [SerializeField]
    private int areaDamage;

    private static bool isInit = false;

    public int Hp { get { return hp; } set { hp = value; } }
    public int BaseMaxHp { get; set; }
    public static int Level {get { return level; } set { level = value; } }
    public static float Exp { get { return exp; } set { exp = value; } }
    public static float MaxExp { get { return maxExp; } set { maxExp = value; } }

    public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }
    public int BaseDefence { get { return baseDefence; } private set { baseDefence = value; } }
    public float BaseSpeed { get { return baseSpeed; } private set { baseSpeed = value; } }
    public int AreaDamage { get { return areaDamage; } private set { areaDamage = value; } }

    public float InterationScale { get; set; }
    public float VerticalVelo { get; set; }
    public Vector3 Movement { get; set; }
    public Vector3 Gravity { get; set; }
    public Vector2 MoveDir { get; set; }


    private void Awake()
    {
        GameEvents.PlayerKill += (data, gold, exp) => AddExp(exp);
        GameEvents.ChangeCurrency += (gold, exp) => AddExp(exp);
        Hp = BaseMaxHp = data.Maxhp;
        InterationScale = data.InterationScale;
        Gravity = Vector2.zero;

        BaseAttack = data.Wdamage;
        BaseSpeed = data.MoveForce;
        AreaDamage = data.AreaDmg;

        if(!isInit)
        {
            Level = 1;
            Exp = 0f;
            MaxExp = 500f;
            isInit = true;
        }

        VerticalVelo = 0f;
        ResetStat();
    }

    private void AddExp(float addExp)
    {
        Exp += addExp;
        if (Exp >= MaxExp)
        {
            while (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                Level += 1;
                MaxExp += 100f;
            }
        }
        GameEvents.RaiseChangeEXPUpdate();
    }

    public void ResetStat()
    {
        Hp = BaseMaxHp = data.Maxhp;
        BaseAttack = baseAttack;
        BaseDefence = baseDefence;
        BaseSpeed = baseSpeed;
    }

    public int TotalDamage()
    {
        return EquipSystem.Instance.EquipTotalDamage() + BaseAttack;
    }

    public int TotalDefence()
    {
        return EquipSystem.Instance.EquipTotalDamage() + BaseDefence;
    }

    public int TotalHP()
    {
        return EquipSystem.Instance.EquipTotalHP() + BaseMaxHp;
    }

    public float TotalSpeed()
    {
        return EquipSystem.Instance.EquipTotalSpeed() + BaseSpeed;
    }

    private void OnDestroy()
    {
        GameEvents.PlayerKill -= (data, gold, exp) => AddExp(exp);
        GameEvents.ChangeCurrency -= (gold, exp) => AddExp(exp);
    }
}
