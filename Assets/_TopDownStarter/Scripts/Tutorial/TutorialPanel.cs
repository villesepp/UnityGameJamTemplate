using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private Image tutorialImage;

    private TutorialManager tutorialManager;

    public void Initialize(TutorialManager manager)
    {
        tutorialManager = manager;
        Hide();
    }

    public void Show(TutorialStep step)
    {
        if (panelRoot != null)
            panelRoot.SetActive(true);

        if (titleText != null)
            titleText.text = step.title;

        if (bodyText != null)
            bodyText.text = step.bodyText;

        if (tutorialImage != null)
        {
            tutorialImage.sprite = step.image;
            tutorialImage.gameObject.SetActive(step.image != null);
        }
    }

    public void Hide()
    {
        if (panelRoot != null)
            panelRoot.SetActive(false);
    }

    public void OnContinueClicked()
    {
        if (tutorialManager != null)
        {
            tutorialManager.ContinueCurrentStep();
        }
    }
}