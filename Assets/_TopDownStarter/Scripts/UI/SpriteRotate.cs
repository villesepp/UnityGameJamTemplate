using UnityEngine;
using UnityEngine.UI;

public class SpinRay : MonoBehaviour
{
    
    [SerializeField, Range(0, 500)] private int speed = 50;
    [SerializeField, Range(0f, 1f)] private float saturation = 1f;
    [SerializeField, Range(0f, 1f)] private float alpha = 1f;

    [SerializeField] private bool reverseDirection = false;

    private int direction = 1;
    private Image targetImage;

    private void Awake()
    {
        if (reverseDirection == false)
        {
            direction = -1;
        }

        targetImage = GetComponent<Image>();
    }

    private void Start()
    {
        Color color = Color.HSVToRGB(Random.value, saturation, 1f, true);
        color.a = alpha;
        targetImage.color = color;
        transform.Rotate (new Vector3 (0, 0, Random.value * 360));
    }

    private void Update()
    {
        transform.Rotate (new Vector3 (0, 0, 1) * Time.deltaTime * speed * direction);
    }
}