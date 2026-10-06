using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public enum PoolType
{
    Enemy,
    equip,
    potion,
    HpBar
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private Dictionary<PoolType, Queue<GameObject>> pool;
    [SerializeField] private Transform uiParent;

    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Hpbar;

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
            {PoolType.potion, new Queue<GameObject>() },
            {PoolType.HpBar, new Queue<GameObject>() }
        };
    }

    public void CreateObj(PoolType type, int size)
    {
        GameObject spawnObj = null;
        Transform parent = null;
        switch (type)
        {
            case PoolType.Enemy:
                spawnObj = Enemy;
                parent = transform;
                break;
            case PoolType.equip:
                break;
            case PoolType.potion:
                break;
            case PoolType.HpBar:
                spawnObj = Hpbar;
                parent = uiParent;
                break;
        }    

        for(int i = 0; i < size; ++i)
        {
            GameObject obj = Instantiate(spawnObj, parent);

            obj.SetActive(false);

            pool[type].Enqueue(obj);
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
                case PoolType.HpBar:
                    return Instantiate(Hpbar, uiParent);
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
