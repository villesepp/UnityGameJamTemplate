using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [Header("Scene Names")]
    [SerializeField] private string bootSceneName = "00_Boot";
    [SerializeField] private string splashSceneName = "00_Splash";
    [SerializeField] private string mainMenuSceneName = "01_MainMenu";
    [SerializeField] private string gameSceneName = "02_Game";
    [SerializeField] private string tutorialSceneName = "03_Tutorial";

    private bool isLoading;

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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == bootSceneName)
        {
            LoadSplash();
        }
    }

    public void LoadSplash()
    {
        LoadScene(splashSceneName, GameState.MainMenu);
    }

    public void LoadMainMenu()
    {
        LoadScene(mainMenuSceneName, GameState.MainMenu);
    }

    public void LoadGame()
    {
        LoadScene(gameSceneName, GameState.Playing);
    }

    public void LoadTutorial()
{
    LoadScene(tutorialSceneName, GameState.Playing);
}

    public void RestartGame()
    {
        //LoadGame();
        string currentSceneName = SceneManager.GetActiveScene().name;
        LoadScene(currentSceneName, GameState.Playing);
    }

    public void QuitGame()
    {
        Debug.Log("Quit requested.");
        Application.Quit();
    }

    private void LoadScene(string sceneName, GameState targetState)
    {
        if (isLoading)
            return;

        StartCoroutine(LoadSceneRoutine(sceneName, targetState));
    }

    private IEnumerator LoadSceneRoutine(string sceneName, GameState targetState)
    {
        isLoading = true;

        Time.timeScale = 1f;

        if (SceneTransition.Instance != null)
        {
            yield return SceneTransition.Instance.PlayClose();
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOperation.isDone)
        {
            yield return null;
        }

        if (GameManager.Instance != null)
        {
            if (targetState == GameState.Playing)
            {
                GameManager.Instance.StartGame();
            }
            else
            {
                GameManager.Instance.SetState(targetState);
            }
        }

        if (SceneTransition.Instance != null)
        {
            yield return SceneTransition.Instance.PlayOpen();
        }

        isLoading = false;
    }
}