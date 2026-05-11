using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip clip;
    [SerializeField] private SoundType soundType = SoundType.SFX;

    public void Play()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager.Instance is missing.");
            return;
        }

        switch (soundType)
        {
            case SoundType.SFX:
                AudioManager.Instance.PlaySFX(clip);
                break;

            case SoundType.UI:
                AudioManager.Instance.PlayUI(clip);
                break;

            case SoundType.Music:
                AudioManager.Instance.PlayMusic(clip);
                break;
        }
    }
}

public enum SoundType
{
    SFX,
    UI,
    Music
}