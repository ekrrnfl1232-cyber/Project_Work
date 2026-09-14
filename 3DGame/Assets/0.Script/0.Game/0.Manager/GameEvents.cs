using System;
using UnityEngine;

public static class GameEvents
{
    // Action = 일반함수(), Action<자료형> = 매개함수(변수)

    // exp ui 업데이트
    public static event Action<float> OnExpChange;
    public static event Action OnGoldChange;

    public static Action OnQuestChanged;
    // 플레이어가 몬스터 잡았을때의 이벤트
    public static Action<int> PlayerKill;

    public static Action LevelChange;

    public static void RaiseExpChange(float exp)
    {
        OnExpChange?.Invoke(exp);
    }
    public static void RaiseGoldChange()
    {
        OnGoldChange?.Invoke();
    }
    public static void RaiseQuestChanged()
    {
        OnQuestChanged?.Invoke();
    }

    public static void RaiseKillChange(int monsterId)
    {
        PlayerKill?.Invoke(monsterId);
    }

    public static void RaiseLevelChange()
    {
        LevelChange?.Invoke();
    }
}
