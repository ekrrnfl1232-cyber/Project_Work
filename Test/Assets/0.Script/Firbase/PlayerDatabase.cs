using UnityEngine;

[System.Serializable]
public class PlayerDatabase
{
    public string uid;
    public int level;
    public int hp;
    public float exp;

    public PlayerDatabase(string uid)
    {
        this.uid = uid;
        level = 1;
        hp = 100;
        exp = 0;
    }
}
