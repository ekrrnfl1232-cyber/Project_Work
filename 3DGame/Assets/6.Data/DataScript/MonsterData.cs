using UnityEngine;

[CreateAssetMenu(fileName = "MD")]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private int hp = 100;
    public int Hp {  get { return hp; } }

    [SerializeField]
    private int damage = 5;
    public int Mdamage { get { return damage; } }

    [SerializeField]
    private float moveSpeed = 3.5f;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private float range = 1.5f;
    public float Range { get { return range; } }

    [SerializeField]
    private float spawnRange = 7f;
    public float SpawnRange { get { return spawnRange; } }

    [SerializeField]
    private float scanSize = 5f;
    public float ScanSize { get { return scanSize; } }

}
