using System;
using UnityEngine;

public class PlayerProgress : Singleton<PlayerProgress>
{
    public int Gold { get; private set; }
    public float Exp { get; private set; }

    public void AddGold(int amount)
    {
        if (amount <= 0)
            return;
        Gold += amount;
        GameEvents.RaiseGoldChange();
    }

    public void AddExp(float amount)
    {
        if (amount <= 0)
            return;
        Exp += amount;
        GameEvents.RaiseExpChange(Exp);
    }
}
