using UnityEngine;

public class PauseMenuButton  : MonoBehaviour
{
    public void OnPauseButtonClicked()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager.Instance is missing");
            return;
        }

        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            GameManager.Instance.PauseGame();
        }
    }
}
