using UnityEngine;

public class BossStat : MonoBehaviour
{
    private int hpOne;
    private int hpTwo;
    private int maxHp;
    private int phase = 1;
    private int chargeDamage = 10;
    private int areaDamage = 5;

    private Vector3 chargeArea = new Vector3(3f, 1f, 5f);
    private float areaRadius = 2.5f;

    public int HpOne { get { return hpOne; } set { hpOne = value; } }
    public int HpTwo { get { return hpTwo; } set { hpTwo = value; } }
    public int MaxHp { get {  return maxHp; } set { maxHp = value; } }
    public int Phase { get { return phase; } set { phase = value; } } 
    public int AreaDamage { get { return areaDamage; } set { areaDamage = value; } }
    public int ChargeDamage { get { return chargeDamage; } set { chargeDamage = value; } }

    public Vector3 ChargeArea { get { return chargeArea; } set { chargeArea = value; } }
    public float AreaRadius { get { return areaRadius; } set { areaRadius = value; } }
    

    public Collider[] TargetCheck { get; set; }

}
