using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShineEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform shine;
    [SerializeField] private CanvasGroup shineCanvasGroup;

    [Header("Timing")]
    [SerializeField] private float delayBetweenShines = 3f;
    [SerializeField] private float shineDuration = 0.55f;
    [SerializeField] private float startDelay = 0.5f;

    [Header("Movement")]
    [SerializeField] private float startX = -260f;
    [SerializeField] private float endX = 260f;

    [Header("Appearance")]
    [Range(0f, 1f)]
    [SerializeField] private float peakAlpha = 0.35f;

    private Coroutine shineRoutine;

    private void OnEnable()
    {
        shineRoutine = StartCoroutine(ShineLoop());
    }

    private void OnDisable()
    {
        if (shineRoutine != null)
        {
            StopCoroutine(shineRoutine);
            shineRoutine = null;
        }

        HideShine();
    }

    private IEnumerator ShineLoop()
    {
        HideShine();

        if (startDelay > 0f)
        {
            yield return new WaitForSecondsRealtime(startDelay);
        }

        while (true)
        {
            yield return PlayShine();

            if (delayBetweenShines > 0f)
            {
                yield return new WaitForSecondsRealtime(delayBetweenShines);
            }
        }
    }

    private IEnumerator PlayShine()
    {
        if (shine == null)
            yield break;

        float timer = 0f;

        while (timer < shineDuration)
        {
            timer += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(timer / shineDuration);

            float easedPosition = SmoothStep(t);
            float alpha = Mathf.Sin(t * Mathf.PI) * peakAlpha;

            Vector2 position = shine.anchoredPosition;
            position.x = Mathf.Lerp(startX, endX, easedPosition);
            shine.anchoredPosition = position;

            SetAlpha(alpha);
            

            yield return null;
        }

        HideShine();
    }

    private float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }

    private void SetAlpha(float alpha)
    {
        if (shineCanvasGroup != null)
        {
            shineCanvasGroup.alpha = alpha;
            return;
        }

        Image image = shine != null ? shine.GetComponent<Image>() : null;

        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    private void HideShine()
    {
        if (shine != null)
        {
            Vector2 position = shine.anchoredPosition;
            position.x = startX;
            shine.anchoredPosition = position;
        }

        SetAlpha(0f);
    }
}