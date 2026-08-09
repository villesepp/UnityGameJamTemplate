using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [Header("Score Source")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Display")]
    [SerializeField] private string prefix = "";
    [SerializeField] private int minimumDigits = 6;

    [Header("Animation")]
    [SerializeField] private float pointsPerSecond = 500f;
    [SerializeField] private bool snapDownwardChanges = true;

    private int targetScore;
    private float displayedScore;

    private void Start()
    {
        if (scoreText == null)
        {
            scoreText = GetComponent<TMP_Text>();
        }

        if (scoreManager == null)
        {
            scoreManager = ScoreManager.Instance;
        }

        if (scoreManager == null)
        {
            Debug.LogWarning($"{nameof(ScoreDisplay)} could not find ScoreManager.");
            return;
        }

        scoreManager.OnScoreChanged.AddListener(HandleScoreChanged);

        targetScore = scoreManager.Score;
        displayedScore = targetScore;

        UpdateText();
    }

    private void Update()
    {
        if (Mathf.RoundToInt(displayedScore) == targetScore)
            return;

        displayedScore = Mathf.MoveTowards(
            displayedScore,
            targetScore,
            pointsPerSecond * Time.unscaledDeltaTime
        );

        UpdateText();
    }

    private void OnDestroy()
    {
        if (scoreManager != null)
        {
            scoreManager.OnScoreChanged.RemoveListener(HandleScoreChanged);
        }
    }

    private void HandleScoreChanged(int newScore)
    {
        if (snapDownwardChanges && newScore < targetScore)
        {
            displayedScore = newScore;
        }

        targetScore = newScore;

        UpdateText();
    }

    private void UpdateText()
    {
        if (scoreText == null)
            return;

        int roundedScore = Mathf.RoundToInt(displayedScore);
        roundedScore = Mathf.Max(0, roundedScore);

        scoreText.text = prefix + roundedScore.ToString($"D{minimumDigits}");
    }
}