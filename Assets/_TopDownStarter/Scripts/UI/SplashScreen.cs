using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float holdDuration = 1.2f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private bool allowSkip = true;

    [Header("UI")]
    [SerializeField] private CanvasGroup canvasGroup;

    private bool isSkipping;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        Time.timeScale = 1f;
    }

    private void Start()
    {
        StartCoroutine(SplashRoutine());
    }

    private void Update()
    {
        if (!allowSkip || isSkipping)
            return;

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SkipSplash();
            return;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            SkipSplash();
            return;
        }

        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            SkipSplash();
        }
    }

    private IEnumerator SplashRoutine()
    {
        yield return Fade(0f, 1f, fadeInDuration);
        yield return new WaitForSecondsRealtime(holdDuration);
        yield return Fade(1f, 0f, fadeOutDuration);

        LoadMainMenu();
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        if (canvasGroup == null)
            yield break;

        if (duration <= 0f)
        {
            canvasGroup.alpha = to;
            yield break;
        }

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);

            t = t * t * (3f - 2f * t);

            canvasGroup.alpha = Mathf.Lerp(from, to, t);

            yield return null;
        }

        canvasGroup.alpha = to;
    }

    private void SkipSplash()
    {
        isSkipping = true;
        StopAllCoroutines();
        StartCoroutine(SkipRoutine());
    }

    private IEnumerator SkipRoutine()
    {
        yield return Fade(canvasGroup != null ? canvasGroup.alpha : 1f, 0f, 0.2f);
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadMainMenu();
        }
        else
        {
            Debug.LogWarning("SceneLoader.Instance is missing. Cannot load Main Menu.");
        }
    }
}