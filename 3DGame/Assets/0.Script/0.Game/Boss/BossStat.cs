using UnityEngine;

public class BossStat : MonoBehaviour
{
    private int hp;
    private int maxHp;
    private int phase = 1;

    public int Hp { get { return hp; } set { hp = value; } }
    public int MaxHp { get {  return maxHp; } set { maxHp = value; } }
    public int Phase { get { return phase; } set { phase = value; } } 

    public Collider[] TargetCheck { get; set; }
}
