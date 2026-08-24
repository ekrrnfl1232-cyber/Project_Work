using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    public int mDamage {  get; private set; }

    public int HP { get; set; }

    public int MaxHP { get; set; }

    public float TargetDis {  get; set; }

    public float StartDis { get; set; }

    public Vector3 StartPos { get; }

    public MonsterModel(int mDamage, int Hp, Vector3 startPos)
    {
        this.mDamage = mDamage;
        this.HP = Hp;
        this.MaxHP = Hp;
        this.StartPos = startPos;
    }
}
