using UnityEngine;

public enum SceneType
{
    Lobby,
    Dungeon1,
    Dungeon2,
    BossRoom,
    TestRoom
}

public class SceneLoader : Singleton<SceneLoader>
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadingScene(SceneType name)
    {
        Loding.LoadScene(name);
    }

    public void LoadGame()
    {
        Loding.LoadScene(SceneType.Lobby);
    }
}
