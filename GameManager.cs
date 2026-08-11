using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing, Pause, Stop
}

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        State = GameState.Playing;
        DontDestroyOnLoad(gameObject);
    }

    public int Score { get; set; }

    public GameState State { get; set; }

}
