using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [Header("Timer Source")]
    [SerializeField] private GameTimer gameTimer;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    [Header("Display")]
    [SerializeField] private int warningThresholdSeconds = 10;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.red;

    private void Start()
    {
        if (timerText == null)
        {
            timerText = GetComponent<TMP_Text>();
        }

        if (gameTimer == null)
        {
            gameTimer = FindAnyObjectByType<GameTimer>();
        }

        if (gameTimer == null)
        {
            Debug.LogWarning($"{nameof(TimerDisplay)} could not find GameTimer.");
            return;
        }

        gameTimer.OnTimeChanged.AddListener(HandleTimeChanged);
    }

    private void OnDestroy()
    {
        if (gameTimer != null)
        {
            gameTimer.OnTimeChanged.RemoveListener(HandleTimeChanged);
        }
    }

    private void HandleTimeChanged(float remainingTime)
    {
        UpdateDisplay(remainingTime);
    }

    private void UpdateDisplay(float remainingTime)
    {
        if (timerText == null)
            return;

        int seconds = Mathf.CeilToInt(Mathf.Max(0f, remainingTime));

        timerText.text = "TIME " + seconds.ToString("000");

        timerText.color = seconds <= warningThresholdSeconds
            ? warningColor
            : normalColor;
    }
}