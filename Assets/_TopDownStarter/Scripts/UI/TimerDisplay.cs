using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameTimer gameTimer;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    private void OnEnable()
    {
        if (gameTimer != null)
        {
            gameTimer.OnTimeChanged.AddListener(UpdateDisplay);
        }
    }

    private void OnDisable()
    {
        if (gameTimer != null)
        {
            gameTimer.OnTimeChanged.RemoveListener(UpdateDisplay);
        }
    }

    private void Start()
    {
        if (gameTimer != null)
        {
            UpdateDisplay(gameTimer.CurrentTime);
        }
    }

    private void UpdateDisplay(float time)
    {
        if (timerText == null)
            return;

        int seconds = Mathf.CeilToInt(time);
        timerText.text = $"Time: {seconds}";
    }
}