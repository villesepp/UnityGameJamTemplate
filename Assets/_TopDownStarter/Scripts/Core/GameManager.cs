using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Booting,
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Booting;

    [Header("Events")]
    public UnityEvent<GameState, GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        GameState oldState = CurrentState;
        CurrentState = newState;

        Debug.Log($"Game state changed: {oldState} -> {CurrentState}");

        OnGameStateChanged?.Invoke(oldState, CurrentState);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SetState(GameState.Playing);
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        Time.timeScale = 0f;
        SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        Time.timeScale = 1f;
        SetState(GameState.Playing);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        SetState(GameState.GameOver);
    }

    public void Victory()
    {
        Time.timeScale = 0f;
        SetState(GameState.Victory);
    }
}