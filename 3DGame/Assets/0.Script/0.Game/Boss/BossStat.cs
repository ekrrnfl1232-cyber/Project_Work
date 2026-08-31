using UnityEngine;

public class BossStat : MonoBehaviour
{
    private int hp;
    private int maxHp;

    public int Hp { get { return hp; } set { hp = value; } }
    public int MaxHp { get {  return maxHp; } set { maxHp = value; } }

    public Collider[] TargetCheck { get; set; }
}
