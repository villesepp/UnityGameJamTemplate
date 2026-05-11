using UnityEngine;
using UnityEngine.EventSystems;

public class SliderReleaseSound : MonoBehaviour, IPointerUpHandler
{
    [Header("Sound")]
    [SerializeField] private AudioClip sampleClip;
    [SerializeField] private SoundType soundType = SoundType.UI;

    public void OnPointerUp(PointerEventData eventData)
    {
        PlaySample();
    }

    private void PlaySample()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager.Instance is missing.");
            return;
        }

        if (sampleClip == null)
        {
            Debug.LogWarning($"{gameObject.name} is missing a sample clip.");
            return;
        }

        switch (soundType)
        {
            case SoundType.SFX:
                AudioManager.Instance.PlaySFX(sampleClip);
                break;

            case SoundType.UI:
                AudioManager.Instance.PlayUI(sampleClip);
                break;

            case SoundType.Music:
                AudioManager.Instance.PlayMusic(sampleClip);
                break;
        }
    }
}