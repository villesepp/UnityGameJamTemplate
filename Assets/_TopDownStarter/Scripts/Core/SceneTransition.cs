using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject transitionCanvas;
    [SerializeField] private CanvasGroup transitionGroup;
    [SerializeField] private RectTransform transitionPanel;

    [Header("Timing")]
    [SerializeField] private float closeDuration = 0.25f;
    [SerializeField] private float holdDuration = 0.08f;
    [SerializeField] private float openDuration = 0.25f;

    [Header("Style")]
    [SerializeField] private float closedHeight = 3000f;
    [SerializeField] private AnimationCurve closeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private AnimationCurve openCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private bool isTransitioning;

    public bool IsTransitioning => isTransitioning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        HideInstant();
    }

    public IEnumerator PlayClose()
    {


    if (transitionCanvas == null || transitionGroup == null || transitionPanel == null)
    {
        Debug.LogWarning("Transition references are missing.");
        yield break;
    }

    isTransitioning = true;
    transitionCanvas.SetActive(true);
    transitionGroup.blocksRaycasts = true;

    float timer = 0f;

    while (timer < closeDuration)
    {
        timer += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(timer / closeDuration);
        float curvedT = closeCurve.Evaluate(t);

        transitionGroup.alpha = curvedT;
        SetPanelHeight(Mathf.Lerp(0f, closedHeight, curvedT));

        yield return null;
    }

    transitionGroup.alpha = 1f;
    SetPanelHeight(closedHeight);

    if (holdDuration > 0f)
    {
        yield return new WaitForSecondsRealtime(holdDuration);
    }
}

    public IEnumerator PlayOpen()
{
    if (transitionCanvas == null || transitionGroup == null || transitionPanel == null)
    {
        Debug.LogWarning("Transition references are missing.");
        yield break;
    }

    transitionCanvas.SetActive(true);
    transitionGroup.blocksRaycasts = true;

    float timer = 0f;

    while (timer < openDuration)
    {
        timer += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(timer / openDuration);
        float curvedT = openCurve.Evaluate(t);

        float value = 1f - curvedT;

        transitionGroup.alpha = value;
        SetPanelHeight(Mathf.Lerp(0f, closedHeight, value));

        yield return null;
    }

    HideInstant();
    isTransitioning = false;

}

    public void HideInstant()
    {
        if (transitionGroup != null)
        {
            transitionGroup.alpha = 0f;
            transitionGroup.blocksRaycasts = false;
        }

        if (transitionPanel != null)
        {
            SetPanelHeight(0f);
        }

        if (transitionCanvas != null)
        {
            transitionCanvas.SetActive(false);
        }
    }

    private void SetPanelHeight(float height)
    {
        transitionPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}