using System.Collections;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    private bool isSubscribed;

    private void OnEnable()
    {
        StartCoroutine(SubscribeWhenReady());
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private IEnumerator SubscribeWhenReady()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        Subscribe();
        Refresh(GameManager.Instance.CurrentState);
    }

    private void Subscribe()
    {
        if (isSubscribed)
            return;

        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        isSubscribed = true;
    }

    private void Unsubscribe()
    {
        if (!isSubscribed)
            return;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStateChanged.RemoveListener(HandleGameStateChanged);
        }

        isSubscribed = false;
    }

    private void HandleGameStateChanged(GameState oldState, GameState newState)
    {
        Refresh(newState);
    }

    private void Refresh(GameState state)
    {
        if (state == GameState.Paused)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        if (pausePanel != null && !pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
        }
    }

    public void Hide()
    {
        if (pausePanel != null && pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }
    }

    public void OnResumeClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    public void OnRestartClicked()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.RestartGame();
        }
    }

    public void OnMainMenuClicked()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadMainMenu();
        }
    }
}