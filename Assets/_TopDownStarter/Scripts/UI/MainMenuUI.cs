using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Start()
    {
        ShowMainMenu();
    }

    public void OnStartGameClicked()
    {
        Debug.Log("Start Game button clicked.");

        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadGame();
        }
        else
        {
            Debug.LogWarning("SceneLoader.Instance is missing.");
        }
    }

    public void OnSettingsClicked()
    {
        ShowSettings();
    }

    public void OnSettingsBackClicked()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveVolumeSettings();
        }

        ShowMainMenu();
    }

    public void OnCreditsClicked()
    {
        ShowCredits();
    }

    public void OnCreditsBackClicked()
    {
        ShowMainMenu();
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit button clicked.");

        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.QuitGame();
        }
        else
        {
            Debug.LogWarning("SceneLoader.Instance is missing.");
        }
    }

    private void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

    private void ShowSettings()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);

        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

    private void ShowCredits()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }
}