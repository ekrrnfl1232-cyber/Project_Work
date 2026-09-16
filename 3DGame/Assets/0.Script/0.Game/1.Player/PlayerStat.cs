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
    public int BaseMaxHp { get; set; }
    public int Level {get { return level; } set { level = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
    public float MaxExp { get { return maxExp; } set { maxExp = value; } }

    public int BaseAttack { get { return baseAttack; } set { baseAttack = value; } }
    public int BaseDefence { get { return baseDefence; } private set { baseDefence = value; } }
    public float BaseSpeed { get { return baseSpeed; } private set { baseSpeed = value; } }

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

        VerticalVelo = 0f;
        Level = 1;
        Exp = 0;
        MaxExp = 500;
        ResetStat();
    }

    private void AddExp(float addExp)
    {
        Exp += addExp;
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
