using UnityEngine;

[System.Serializable]
public class PlayerDatabase
{
    public string uid;
    public string nickName;

    public int level;
    public int hp;
    public float exp;
    public int gold;

    public PlayerDatabase(string uid)
    {
        this.uid = uid;
        nickName = $"Geust_{Random.Range(0, 1000000)}";

        level = 1;
        hp = 100;
        gold = 1000;
        exp = 0;
    }
}
