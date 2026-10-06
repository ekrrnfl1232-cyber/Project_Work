using UnityEngine;

[System.Serializable]
public class MonsterModel
{
    public int HP { get; set; }

    public int MaxHP { get; set; }

    public float TargetDis {  get; set; }

    public Vector3 StartPos { get; set; }
    
    public int Damage { get; set; }
    public MonsterModel(int Hp, Vector3 startPos)
    {
        this.HP = Hp;
        this.MaxHP = Hp;
        this.StartPos = startPos;
    }
}
