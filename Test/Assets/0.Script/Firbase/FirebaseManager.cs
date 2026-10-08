using UnityEngine;
using Firebase;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Firebase.Extensions;

public class FirebaseManager : Singleton<FirebaseManager>
{
    private FirebaseApp app;
    
    public bool IsReady {  get; private set; }

    private void Start()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        Debug.Log("[Firebase] Initialize");

        FirebaseApp.CheckAndFixDependenciesAsync()
            .ContinueWithOnMainThread( task =>
            {
                if(task.IsFaulted)
                {
                    Debug.LogError("[Firebase] Task Failed");
                    return;
                }

                DependencyStatus status = task.Result;
                if(status != DependencyStatus.Available)
                {
                    Debug.LogError("[Firebase] Dependency Error : " + status);
                    return;
                }

                app = FirebaseApp.DefaultInstance;
                IsReady = true;
                Debug.Log($"[Firebase] Init Success");
                AuthManager.Instance.InitializeAuth();
            });
    }
}
