using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float startTime = 60f;
    [SerializeField] private bool startAutomatically = true;
    [SerializeField] private bool gameOverWhenTimeRunsOut = true;

    [Header("Events")]
    public UnityEvent<float> OnTimeChanged;
    public UnityEvent OnTimerFinished;

    private float currentTime;
    private bool isRunning;
    private bool hasFinished;

    public float CurrentTime => currentTime;
    public bool IsRunning => isRunning;

    private void Awake()
    {
        currentTime = startTime;
    }

    private void Start()
    {
        OnTimeChanged?.Invoke(currentTime);

        if (startAutomatically)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        if (!isRunning)
            return;

        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameState.Playing)
            return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);

        OnTimeChanged?.Invoke(currentTime);

        if (currentTime <= 0f && !hasFinished)
        {
            FinishTimer();
        }
    }

    public void StartTimer()
    {
        if (hasFinished)
            return;

        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        hasFinished = false;
        isRunning = false;

        OnTimeChanged?.Invoke(currentTime);
    }

    private void FinishTimer()
    {
        hasFinished = true;
        isRunning = false;

        Debug.Log("Timer finished.");
        OnTimerFinished?.Invoke();

        if (gameOverWhenTimeRunsOut && GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}