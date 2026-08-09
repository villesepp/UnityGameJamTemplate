using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.15f;
    [SerializeField] private float speed = 2f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float progress = Mathf.PingPong(Time.time * speed, 1f);
        progress = Mathf.SmoothStep(0f, 1f, progress);

        transform.localScale = Vector3.Lerp(
            originalScale,
            originalScale * scaleMultiplier,
            progress
        );
    }
}