using UnityEngine;
using Firebase.Crashlytics;

public class CrashlyticsManager : Singleton<CrashlyticsManager>
{
    public void SetUser(string uid)
    {
        Crashlytics.SetUserId(uid);
    }

    public void SetStage(int stageId)
    {
        Crashlytics.SetCustomKey("stage_id", stageId.ToString());
        // Crash 직전에 로그 남기기
        Crashlytics.Log("Stage Start : " + stageId);
    }

    public void RecordException(System.Exception exception)
    {
        //Non-Fatal Exception 기록
        Crashlytics.LogException(exception);
    }

    /*private void OnPlayerReady()
    {
        string uid = AuthManager.Instance.CurrentUser.UserId;
        CrashlyticsManager.Instance.SetUser(uid);
        AnalyticsManager.Instance.GameStart();
    }*/
}
