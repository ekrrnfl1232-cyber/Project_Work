using UnityEngine;
using Firebase.Auth;

public class AuthManager : Singleton<AuthManager>
{
    private FirebaseAuth auth;

    public void Init()
    {
        Debug.Log("[Auth] DefaultInstance");
        auth = FirebaseAuth.DefaultInstance;
        SignInAnonymous();
    }

    public void SignInAnonymous()
    {
        //SignInAnonymouslyAsync : 익명 사용자
        auth.SignInAnonymouslyAsync()
            .ContinueWith( task =>
            {
                if(task.IsCanceled)
                {
                    Debug.LogWarning("[Auth] Login Cancel");
                    return;
                }

                if(task.IsFaulted)
                {
                    Debug.LogError("[Auth] Login Failed");
                    Debug.LogException(task.Exception);
                    return;
                }

                Debug.Log("[Auth] Login Success");
            });
    }
}
