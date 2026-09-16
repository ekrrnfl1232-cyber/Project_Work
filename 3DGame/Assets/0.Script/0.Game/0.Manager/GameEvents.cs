using System;
using UnityEngine;

public static class GameEvents
{
    // Action = 일반함수(), Action<자료형> = 매개함수(변수)

    public static Action OnQuestChanged;
    // 플레이어가 몬스터 잡았을때의 이벤트
    public static Action<MonsterData, int, float> PlayerKill;
    public static Action<int, float> ChangeCurrency;

    public static Action LevelChange;
    public static void RaiseQuestChanged()
    {
        OnQuestChanged?.Invoke();
    }

    public static void RaiseKillChange(MonsterData data, int gold, float exp)
    {
        PlayerKill?.Invoke(data, gold, exp);
    }

    public static void RaiseChangeCurrency(int gold, float exp)
    {
        ChangeCurrency?.Invoke(gold, exp);
    }

    public static void RaiseLevelChange()
    {
        LevelChange?.Invoke();
    }
}
