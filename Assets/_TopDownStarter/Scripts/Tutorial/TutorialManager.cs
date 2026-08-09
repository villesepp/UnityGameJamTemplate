using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public static bool IsTutorialPanelOpen { get; private set; }

    [Header("Steps")]
    [SerializeField] private TutorialStep[] steps;

    [Header("UI")]
    [SerializeField] private TutorialPanel tutorialPanel;
    [SerializeField] private TutorialGoalOverlay goalOverlay;

    [Header("Debug")]
    [SerializeField] private int currentStepIndex = -1;

    [Header("Triggers")]
    [SerializeField] private TutorialTrigger[] tutorialTriggers;

    public int CurrentStepIndex => currentStepIndex;

    private void Awake()
    {
        Instance = this;
        IsTutorialPanelOpen = false;
    }

    private void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.Initialize(this);

        if (goalOverlay != null)
            goalOverlay.Hide();

        if (steps != null && steps.Length > 0)
        {
            StartStep(0);
        }
        else
        {
            Debug.LogWarning("TutorialManager has no tutorial steps.");
        }
    }

    private void OnDestroy()
    {
        IsTutorialPanelOpen = false;
        Time.timeScale = 1f;
    }

    public void StartStep(int stepIndex)
    {
        if (steps == null || stepIndex < 0 || stepIndex >= steps.Length)
        {
            Debug.LogWarning($"Invalid tutorial step index: {stepIndex}");
            return;
        }

        currentStepIndex = stepIndex;

        UpdateTriggerVisibility();

        TutorialStep step = steps[currentStepIndex];

        if (goalOverlay != null)
            goalOverlay.Show(step.goalText);

        if (step.pauseOnStart)
        {
            ShowStepPanel(step);
        }
        else
        {
            ResumeTutorialGameplay();
        }

        Debug.Log($"Tutorial step started: {currentStepIndex} - {step.title}");
    }

    private void UpdateTriggerVisibility()
    {
        if (tutorialTriggers == null)
            return;

        foreach (TutorialTrigger trigger in tutorialTriggers)
        {
            if (trigger == null)
                continue;

            bool isCurrentStepTrigger = trigger.RequiredStepIndex == currentStepIndex;
            trigger.SetActiveForCurrentStep(isCurrentStepTrigger);
        }
    }

    private void HideAllTriggers()
    {
        if (tutorialTriggers == null)
            return;

        foreach (TutorialTrigger trigger in tutorialTriggers)
        {
            if (trigger == null)
                continue;

            trigger.SetActiveForCurrentStep(false);
        }
    }

    private void ShowStepPanel(TutorialStep step)
    {
        IsTutorialPanelOpen = true;
        Time.timeScale = 0f;

        if (tutorialPanel != null)
            tutorialPanel.Show(step);
    }

    public void ContinueCurrentStep()
    {
        if (tutorialPanel != null)
            tutorialPanel.Hide();

        ResumeTutorialGameplay();
    }

    private void ResumeTutorialGameplay()
    {
        IsTutorialPanelOpen = false;

        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState == GameState.Playing)
        {
            Time.timeScale = 1f;
        }
    }

    public void CompleteCurrentStep()
    {
        if (steps == null || currentStepIndex < 0 || currentStepIndex >= steps.Length)
            return;

        TutorialStep completedStep = steps[currentStepIndex];

        Debug.Log($"Tutorial step completed: {currentStepIndex} - {completedStep.title}");

        if (completedStep.triggerVictoryOnComplete)
        {
            CompleteTutorial();
            return;
        }

        int nextStepIndex = currentStepIndex + 1;

        if (nextStepIndex >= steps.Length)
        {
            CompleteTutorial();
            return;
        }

        StartStep(nextStepIndex);
    }

    private void CompleteTutorial()
    {
        IsTutorialPanelOpen = false;

        if (tutorialPanel != null)
            tutorialPanel.Hide();

        if (goalOverlay != null)
            goalOverlay.Hide();

        HideAllTriggers();

        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Victory();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is missing.");
        }
    }
}