using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged.AddListener(UpdateDisplay);
        }
    }

    private void OnDisable()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged.RemoveListener(UpdateDisplay);
        }
    }

    private void Start()
    {
        if (scoreManager != null)
        {
            UpdateDisplay(scoreManager.Score);
        }
    }

    private void UpdateDisplay(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}