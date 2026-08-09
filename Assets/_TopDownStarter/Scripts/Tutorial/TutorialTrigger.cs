using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] private int requiredStepIndex;
    [SerializeField] private bool destroyAfterTriggered = false;

    [Header("Visual")]
    [SerializeField] private GameObject visualRoot;

    private bool hasTriggered;
    private Collider2D triggerCollider;

    public int RequiredStepIndex => requiredStepIndex;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();

        if (visualRoot == null)
        {
            visualRoot = gameObject;
        }
    }

    private void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    public void SetActiveForCurrentStep(bool isActiveStep)
    {
        if (visualRoot != null)
        {
            visualRoot.SetActive(isActiveStep);
        }

        if (triggerCollider != null)
        {
            triggerCollider.enabled = isActiveStep;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (TutorialManager.Instance == null)
        {
            Debug.LogWarning("TutorialManager.Instance is missing.");
            return;
        }

        if (TutorialManager.Instance.CurrentStepIndex != requiredStepIndex)
            return;

        hasTriggered = true;

        TutorialManager.Instance.CompleteCurrentStep();

        if (destroyAfterTriggered)
        {
            Destroy(gameObject);
        }
        else
        {
            SetActiveForCurrentStep(false);
        }
    }
}