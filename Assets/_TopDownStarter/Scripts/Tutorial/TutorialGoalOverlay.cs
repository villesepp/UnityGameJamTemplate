using TMPro;
using UnityEngine;

public class TutorialGoalOverlay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject overlayRoot;
    [SerializeField] private TMP_Text goalText;

    public void Show(string text)
    {
        if (overlayRoot != null)
            overlayRoot.SetActive(true);

        if (goalText != null)
            goalText.text = text;
    }

    public void Hide()
    {
        if (overlayRoot != null)
            overlayRoot.SetActive(false);
    }
}