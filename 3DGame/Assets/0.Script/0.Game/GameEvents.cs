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
    // 인벤토리를 열었을 시 플레이어 컨트롤러 작동 멈춤
    public static event Action<bool> OnInventChange;

    public static event Action OnSave;

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

    public static void RaiseInven(bool isOn)
    {
        OnInventChange?.Invoke(isOn);
    }

    public static void RaiseSave()
    {
        OnSave?.Invoke();
    }
}
