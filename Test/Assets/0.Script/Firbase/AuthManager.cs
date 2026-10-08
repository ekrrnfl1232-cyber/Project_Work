using UnityEngine;
using Firebase.Auth;

public class AuthManager : Singleton<AuthManager>
{
    private FirebaseAuth auth;
    private FirebaseUser CurrentUser => auth?.CurrentUser;
    private bool isLoggedIn => CurrentUser != null;

    public void InitializeAuth()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += OnAuthStateChanged;

        if(isLoggedIn)
        {
            PrintUser();
        }
        else
        {
            SignInAnonymous();
        }
    }

    private void OnAuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(isLoggedIn)
        {
            Debug.Log("[Auth] Login State");
        }
        else
        {
            Debug.Log("[Auth] Logout State");
        }
    }

    public void SignInAnonymous()
    {
        if(auth == null)
        {
            Debug.LogError("[Auth] Auth Not Initialized");
            return;
        }
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
                PrintUser();
            });
    }

    private void CheckLogin()
    {
        if(CurrentUser != null)
        {
            Debug.Log("[Auth] Existing");
            PrintUser();
            return;
        }
        SignInAnonymous();
    }

    private void PrintUser()
    {
        if (CurrentUser == null)
            return;

        Debug.Log($"[Auth] UID: {CurrentUser.UserId}");
        // 여기서 익명 계정 확인 가능
        Debug.Log($"[Auth] Anonymous {CurrentUser.IsAnonymous}");
    }
    public void SignIn()
    {
        CheckLogin();
    }

    public void SignOut()
    {
        if (auth == null)
            return;
        if(auth.CurrentUser == null)
            return;

        auth.SignOut();
    }

    private void OnDestroy()
    {
        if(auth != null)
        {
            auth.StateChanged -= OnAuthStateChanged;
        }
    }
}
