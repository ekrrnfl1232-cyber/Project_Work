using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public enum PoolType
{
    Enemy,
    equip,
    potion
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] private Dictionary<PoolType, Queue<GameObject>> pool;

    [SerializeField] private GameObject Enemy;

    private void Awake()
    {
        SettingPool();
    }

    private void SettingPool()
    {
        pool = new Dictionary<PoolType, Queue<GameObject>>()
        {
            {PoolType.Enemy, new Queue<GameObject>() },
            {PoolType.equip, new Queue<GameObject>() },
            {PoolType.potion, new Queue<GameObject>() }
        };
    }

    public void CreateObj(PoolType type, int size)
    {
        GameObject spawnObj = null;
        switch (type)
        {
            case PoolType.Enemy:
                spawnObj = Enemy;
                break;
            case PoolType.equip:
                break;
            case PoolType.potion:
                break;
        }    

        for(int i = 0; i < size; ++i)
        {
            GameObject Mon = Instantiate(spawnObj, transform);

            Mon.SetActive(false);

            pool[type].Enqueue(Mon);
        }
    }

    public GameObject GetObject(PoolType type)
    {
        if (pool[type].Count <= 0)
        {
            switch (type)
            {
                case PoolType.Enemy:
                    return Instantiate(Enemy, transform);
                case PoolType.equip:
                    break;
                case PoolType.potion:
                    break;
            }
        }
        GameObject obj = pool[type].Dequeue();
        return obj;
    }

    public void ReturnObject(PoolType type, GameObject obj)
    {
        obj.SetActive(false);

        pool[type].Enqueue(obj);
    }
}
