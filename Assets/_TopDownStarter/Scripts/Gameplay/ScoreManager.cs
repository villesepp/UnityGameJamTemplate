using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    [SerializeField] private int score;

    [Header("Events")]
    public UnityEvent<int> OnScoreChanged;

    public int Score => score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        OnScoreChanged?.Invoke(score);
    }

    public void AddScore(int amount)
    {
        if (amount <= 0)
            return;

        score += amount;

        Debug.Log($"Score: {score}");
        OnScoreChanged?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}