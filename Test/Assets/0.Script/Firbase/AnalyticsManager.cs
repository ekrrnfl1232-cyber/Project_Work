using UnityEngine;
using Firebase.Analytics;
using System.Xml.Serialization;

public class AnalyticsManager : Singleton<AnalyticsManager>
{
    public void StageStart()
    {
        LogStageStart(5);
    }

    public void StageClear()
    {
        LogStageClear(5, 120, 23);
    }

    public void Currency()
    {
        CurrencyEarn("1", 5000, "골드 구매");
    }

    public void InLog()
    {
        Log("유저 입장");
    }

    public void GameStart()
    {
        FirebaseAnalytics.LogEvent("game_start");
        Debug.Log("[Analytics] Game Start");
    }

    public void LogStageStart(int stageId)
    {
        FirebaseAnalytics.LogEvent("stage_start", new Parameter("stage_id", stageId));
        Debug.Log("[Analytics] Stage Start");
    }

    public void LogStageClear(int stageId, int clearTime, int playerLevel)
    {
        FirebaseAnalytics.LogEvent("stage_clear", 
            new Parameter("stage_id", stageId), 
            new Parameter("clear_time", clearTime),
            new Parameter("player_level", playerLevel));
        Debug.Log("[Analytics] Stage Clear");
    }

    public void CurrencyEarn(string type, int amount, string source)
    {
        FirebaseAnalytics.LogEvent("currency_earn",
            new Parameter("currency_type", type),
            new Parameter("currency_amount", amount),
            new Parameter("currency_source", source));
        Debug.Log("[Analytics] CurrencyEarn");
    }

    public void Log(string eventName)
    {
        FirebaseAnalytics.LogEvent(eventName);
        Debug.Log("[Analytics] In Log");
    }
}
