using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private Transform target;
    [SerializeField] private MonsterData data;
    void Start()
    {
        ObjectPoolManager.Instance.CreateObj(PoolType.Enemy, spawnCount);
        ObjectPoolManager.Instance.CreateObj(PoolType.HpBar, spawnCount);
    }

    private void OnEnable()
    {
        GameEvents.EnPlayer += SpawnMonster;

    }
    private void OnDisable()
    {
        GameEvents.EnPlayer -= SpawnMonster;
    }



    public void SpawnMonster(Transform combat)
    {
        for(int i = 0; i < spawnCount; ++i)
        {
            GameObject monster = ObjectPoolManager.Instance.GetObject(PoolType.Enemy);
            Monster mon = monster.GetComponent<Monster>();
            mon.Target = target;
            mon.data = data;
            DungeonManager.Instance.Add(monster);
            monster.transform.position = RandomPostion(combat);
            monster.SetActive(true);
        }
    }

    public Vector3 RandomPostion(Transform combat)
    {
        Vector2 pos = Random.insideUnitCircle * 13f;

        return new Vector3(combat.position.x + pos.x, 1f, combat.position.z + pos.y);
    }
}
