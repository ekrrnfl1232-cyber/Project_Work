using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    public string[] sceneName =
    {
        "Title",
        "Lobby",
        "Loading",
        "Game",
        "Dungeon1"
    };

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void TitleLoadScene()
    {
        Loding.LoadScene(sceneName[0]);
    }
    public void LobbyLoadScene()
    {
        Loding.LoadScene(sceneName[1]);
    }
    public void GameLoadScene()
    {
        Loding.LoadScene(sceneName[3]);
    }
    public void Dungeon1LoadScene()
    {
        Loding.LoadScene(sceneName[4]);
    }
}
