using System;
using UnityEngine;

public static class GameEvents
{
    // Action = 일반함수(), Action<자료형> = 매개함수(변수)

    public static Action OnQuestChanged;
    // 플레이어가 몬스터 잡았을때의 이벤트
    public static Action<MonsterData, int, float> PlayerKill;
    public static Action DeadMonster;
    public static Action<int, float> ChangeCurrency;
    public static Action ChangeEXPUpdate;
    // 플레이어가 전투방 진입 시
    public static Action<Transform> EnPlayer;
    public static Action<Transform> SpawnMonster;
    public static Action<Transform> ChaseMonster;
    public static Action ClearCombat;
    public static Action DeadPlayer;

    public static void RaiseQuestChanged()
    {
        OnQuestChanged?.Invoke();
    }

    public static void RaiseKillChange(MonsterData data, int gold, float exp)
    {
        PlayerKill?.Invoke(data, gold, exp);
    }

    public static void RaiseChangeDeadMonster()
    {
        DeadMonster?.Invoke();
    }

    public static void RaiseChangeCurrency(int gold, float exp)
    {
        ChangeCurrency?.Invoke(gold, exp);
    }

    public static void RaiseChangeEXPUpdate()
    {
        ChangeEXPUpdate?.Invoke();
    }

    public static void RaiseChangeEnPlayer(Transform combat)
    {
        EnPlayer?.Invoke(combat);
    }

    public static void RaiseChangeSpawnMonster(Transform mon)
    {
        SpawnMonster?.Invoke(mon);
    }

    public static void RaiseChaseMonster(Transform target)
    {
        ChaseMonster?.Invoke(target);
    }

    public static void RaiseClearCombat()
    {
        ClearCombat?.Invoke();
    }

    public static void RaiseChangeDeadPlayer()
    {
        DeadPlayer?.Invoke();
    }
}
