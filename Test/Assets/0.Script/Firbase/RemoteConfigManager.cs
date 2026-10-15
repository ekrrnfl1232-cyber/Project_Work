using UnityEngine;
using Firebase.RemoteConfig;
using System.Collections.Generic;
using Firebase.Extensions;

public static class GameBalance
{
    public static int EnemyDamage
    {
        get; set;
    }

    public static float StageReward
    {
        get; set;
    }

    public static string EventBonus
    {
        get; set;
    }

    public static string MinimumVersion
    {
        get; set;
    }
}

public class RemoteConfigManager : Singleton<RemoteConfigManager>
{
    private FirebaseRemoteConfig remoteConfig;

    private void Awake()
    {
        remoteConfig = FirebaseRemoteConfig.DefaultInstance;
    }

    public void SetDefaults()
    {
        Dictionary<string, object> defaults = new()
        {
            {"enemy_damage", 10},
            {"event_bonus", 1.0},
            {"minimum_version", "1.0.0"},
            {"stage_reward", 1000}
        };

        remoteConfig
            .SetDefaultsAsync(defaults)
            .ContinueWithOnMainThread( task =>
            {
                if(task.IsFaulted)
                {
                    Debug.LogError("[Remote Config] Defaults Fail");
                    return;
                }

                Debug.Log("[Remote Config] Default Ready");
                FetchRemoteConfig();
            });
    }

    private void FetchRemoteConfig()
    {
        Debug.Log("[Remote Config] Fetch Start");
        remoteConfig
            .FetchAsync()
            .ContinueWithOnMainThread(task =>
            {
                if(task.IsFaulted)
                {
                    Debug.LogError("[Remote Config] Fetch Fail");
                    return;
                }

                remoteConfig
                .ActivateAsync()
                .ContinueWithOnMainThread(activateTask =>
                {
                    if (activateTask.IsFaulted)
                    {
                        Debug.LogError("[Remote Config] Active Fail");
                        return;
                    }

                    Debug.Log("[Remote Config] Activate Success");
                    ApplyValues();
                });

                Debug.Log("[Remote Config] Fetch Success");
            });
    }

    private void ApplyValues()
    {
        Debug.Log("[Remote Config] ApplyValue");
        GameBalance.EnemyDamage = (int)remoteConfig.GetValue("enemy_damage").LongValue;
        GameBalance.StageReward = remoteConfig.GetValue("stage_reward").LongValue;
        GameBalance.EventBonus = remoteConfig.GetValue("event_bonus").DoubleValue.ToString();
        Debug.Log("[Remote Config] Debug");
        GameBalance.MinimumVersion = remoteConfig.GetValue("minimum_version").StringValue;

        Debug.Log($"[Remote Config] Damage : {GameBalance.EnemyDamage}");
        Debug.Log($"[Remote Config] Reward : {GameBalance.StageReward}");
        Debug.Log($"[Remote Config] Bonus : {GameBalance.EventBonus}");
        Debug.Log($"[Remote Config] Version : {GameBalance.MinimumVersion}");
    }
}

//("[Remote Config]");
