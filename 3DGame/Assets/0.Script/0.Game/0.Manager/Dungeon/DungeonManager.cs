using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    private List<GameObject> monster;

    private void Awake()
    {
        monster = new();
        GameEvents.DeadMonster += IsKillMonster;
    }

    public void Add(GameObject mon)
    {
        monster.Add(mon);
    }

    public void IsKillMonster()
    {
        monster.RemoveAt(0);
        if(monster.Count == 0)
        {
            GameEvents.RaiseClearCombat();
        }
    }
}
