using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    public int mDamage {  get; private set; }

    public int HP { get; set; }

    public int MaxHP { get; set; }

    public float Dis {  get; set; }
    public Vector3 TarPos { get; set; }

    public MonsterModel(int mDamage, int Hp, float dis,Vector3 tPos)
    {
        this.mDamage = mDamage;
        this.HP = Hp;
        this.MaxHP = Hp;
        this.Dis = dis;
        this.TarPos = tPos;
    }
}
