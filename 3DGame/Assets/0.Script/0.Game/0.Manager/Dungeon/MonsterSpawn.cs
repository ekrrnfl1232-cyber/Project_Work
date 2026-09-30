using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private BoxCollider combat01;
    [SerializeField] private int spawnCount = 5;
    void Start()
    {
        ObjectPoolManager.Instance.CreateObj(PoolType.Enemy, 10);
    }

    private void OnEnable()
    {
        GameEvents.EnPlayer += SpawnMonster;
    }
    private void OnDisable()
    {
        GameEvents.EnPlayer -= SpawnMonster;
    }

    public void SpawnMonster(BoxCollider col)
    {
        for(int i = 0; i < spawnCount; ++i)
        {
            GameObject monster = ObjectPoolManager.Instance.GetObject(PoolType.Enemy);
            monster.transform.position = RandomPostion(col);
            monster.SetActive(true);
        }
    }

    public Vector3 RandomPostion(BoxCollider col)
    {
        Bounds bounds = col.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        Vector3 pos = new Vector3(x, bounds.center.y, z);
        return pos;
    }
}
