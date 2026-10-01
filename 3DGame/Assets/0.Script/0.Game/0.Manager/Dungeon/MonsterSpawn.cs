using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] private int spawnCount = 5;
    void Start()
    {
        ObjectPoolManager.Instance.CreateObj(PoolType.Enemy, 10);
        SpawnMonster();
    }

    private void OnEnable()
    {
        //GameEvents.EnPlayer += SpawnMonster;

    }
    private void OnDisable()
    {
        //GameEvents.EnPlayer -= SpawnMonster;
    }



    public void SpawnMonster()
    {
        for(int i = 0; i < spawnCount; ++i)
        {
            GameObject monster = ObjectPoolManager.Instance.GetObject(PoolType.Enemy);
            monster.transform.position = RandomPostion();
            monster.SetActive(true);
        }
    }

    public Vector3 RandomPostion()
    {
        Vector2 pos = Random.insideUnitCircle * 13f;

        return new Vector3(transform.position.x + pos.x, 1f, transform.position.z + pos.y);
    }

    public void ReturnObject()
    {

    }
}
