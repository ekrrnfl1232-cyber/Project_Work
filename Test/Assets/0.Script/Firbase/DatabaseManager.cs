using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class DatabaseManager : Singleton<DatabaseManager>
{
    private FirebaseDatabase database;
    string uid;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SavePlayerData(new PlayerDatabase(uid));
        }
    }

    public void InitalizeDatabase(string uid)
    {
        database = FirebaseDatabase.DefaultInstance;
        this.uid = uid;
        Debug.Log("[Database] Initalize");
    }

    public void SavePlayerData(PlayerDatabase data)
    {
        string json = JsonUtility.ToJson(data);

        DatabaseReference reference =
            database
            .GetReference("users")
            .Child(data.uid);
        reference
            .SetRawJsonValueAsync(json)
            .ContinueWithOnMainThread(task =>
            {
                if(task.IsFaulted)
                {
                    Debug.Log("[Database] Player Save Failed");
                    Debug.LogException(task.Exception);
                    return;
                }

                Debug.Log("[Database] Player Data Success");
            });
    }

    public void LoadPlayerData(DataSnapshot snapshot)
    {
        string json = snapshot.GetRawJsonValue();
        PlayerDatabase data = JsonUtility.FromJson<PlayerDatabase>(json);
        Debug.Log($"[Database] Load Success : {data.gold}");
    }

    public void LoadOrCreatePlayer()
    {
        DatabaseReference reference = database
            .GetReference("users")
            .Child(uid);

        reference
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("[Database] Load Failed");
                    Debug.LogException(task.Exception);
                    return;
                }

                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    LoadExistionPlayer(snapshot);
                }
                else
                {
                    CreateNewPlayer(uid);
                }
            });
    }

    private void LoadExistionPlayer(DataSnapshot snapshot)
    {
        Debug.Log("[Database] Old Player");
        LoadPlayerData(snapshot);
    }

    private void CreateNewPlayer(string uid)
    {
        Debug.Log("[Database] New Player");
        SavePlayerData(new PlayerDatabase(uid));
    }
}
