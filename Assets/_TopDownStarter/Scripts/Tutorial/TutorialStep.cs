using UnityEngine;

[System.Serializable]
public class TutorialStep
{
    [Header("Full Screen Panel")]
    public string title;

    [TextArea(3, 8)]
    public string bodyText;

    public Sprite image;

    [Header("Gameplay Overlay")]
    public string goalText;

    [Header("Behavior")]
    public bool pauseOnStart = true;
    public bool triggerVictoryOnComplete;
}