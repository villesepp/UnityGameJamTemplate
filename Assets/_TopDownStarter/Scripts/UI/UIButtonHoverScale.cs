using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [Header("Target")]
    [SerializeField] private RectTransform target;

    [Header("Scale")]
    [SerializeField] private float normalScale = 1f;
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float duration = 0.08f;

    private Coroutine scaleRoutine;

    private void Awake()
    {
        if (target == null)
        {
            target = transform as RectTransform;
        }

        SetScaleInstant(normalScale);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateToScale(hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateToScale(normalScale);
    }

    public void OnSelect(BaseEventData eventData)
    {
        AnimateToScale(hoverScale);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        AnimateToScale(normalScale);
    }

    private void AnimateToScale(float targetScale)
    {
        if (scaleRoutine != null)
        {
            StopCoroutine(scaleRoutine);
        }

        scaleRoutine = StartCoroutine(ScaleRoutine(targetScale));
    }

    private IEnumerator ScaleRoutine(float targetScale)
    {
        if (target == null)
            yield break;

        Vector3 startScale = target.localScale;
        Vector3 endScale = Vector3.one * targetScale;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);

            // Smoothstep easing.
            t = t * t * (3f - 2f * t);

            target.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        target.localScale = endScale;
        scaleRoutine = null;
    }

    private void SetScaleInstant(float scale)
    {
        if (target != null)
        {
            target.localScale = Vector3.one * scale;
        }
    }
}