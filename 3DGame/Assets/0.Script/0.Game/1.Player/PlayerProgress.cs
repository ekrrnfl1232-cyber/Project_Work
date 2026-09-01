using System;
using UnityEngine;

public class PlayerProgress : Singleton<PlayerProgress>
{
    public int Gold { get; private set; }
    public float Exp { get; private set; }

    public event Action OnChanged;

    public void AddGold(int amount)
    {
        if (amount <= 0)
            return;
        Gold += amount;
        OnChanged?.Invoke();
    }

    public void AddExp(float amount)
    {
        if (amount <= 0)
            return;
        Exp += amount;
        OnChanged?.Invoke();
    }
}
