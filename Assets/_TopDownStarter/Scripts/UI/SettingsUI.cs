using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;

    private bool isInitializing;

    private void OnEnable()
    {
        InitializeSliders();
    }

    private void InitializeSliders()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager.Instance is missing.");
            return;
        }

        isInitializing = true;

        if (masterVolumeSlider != null)
            masterVolumeSlider.value = AudioManager.Instance.MasterVolume;

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = AudioManager.Instance.MusicVolume;

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = AudioManager.Instance.SFXVolume;

        if (uiVolumeSlider != null)
            uiVolumeSlider.value = AudioManager.Instance.UIVolume;

        isInitializing = false;
    }

    public void OnMasterVolumeChanged(float value)
    {
        if (isInitializing)
            return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMasterVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        if (isInitializing)
            return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        if (isInitializing)
            return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.SetSFXVolume(value);
    }

    public void OnUIVolumeChanged(float value)
    {
        if (isInitializing)
            return;

        if (AudioManager.Instance != null)
            AudioManager.Instance.SetUIVolume(value);
    }
}