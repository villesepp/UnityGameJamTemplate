using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private Vector2 scrollSpeed = new Vector2(-0.01f, -0.01f);

    private RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        Rect uvRect = rawImage.uvRect;

        uvRect.position += scrollSpeed * Time.unscaledDeltaTime;

        uvRect.x = Mathf.Repeat(uvRect.x, 1f);
        uvRect.y = Mathf.Repeat(uvRect.y, 1f);

        rawImage.uvRect = uvRect;
    }
}