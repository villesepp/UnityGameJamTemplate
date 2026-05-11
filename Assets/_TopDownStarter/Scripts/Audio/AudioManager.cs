using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const string UIVolumeKey = "UIVolume";

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float masterVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.7f;

    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float uiVolume = 1f;

    public float MasterVolume => masterVolume;
    public float MusicVolume => musicVolume;
    public float SFXVolume => sfxVolume;
    public float UIVolume => uiVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumeSettings();
        ApplyVolumes();
    }

    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicSource == null || musicClip == null)
            return;

        if (musicSource.clip == musicClip && musicSource.isPlaying)
            return;

        musicSource.clip = musicClip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource == null)
            return;

        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(AudioClip clip)
    {
        if (uiSource == null || clip == null)
            return;

        uiSource.PlayOneShot(clip);
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        ApplyVolumes();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        ApplyVolumes();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        ApplyVolumes();
    }

    public void SetUIVolume(float value)
    {
        uiVolume = Mathf.Clamp01(value);
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        if (musicSource != null)
        {
            musicSource.volume = masterVolume * musicVolume;
        }

        if (sfxSource != null)
        {
            sfxSource.volume = masterVolume * sfxVolume;
        }

        if (uiSource != null)
        {
            uiSource.volume = masterVolume * uiVolume;
        }
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, masterVolume);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume);
        PlayerPrefs.SetFloat(UIVolumeKey, uiVolume);
        PlayerPrefs.Save();

        Debug.Log("Audio settings saved.");
    }

    private void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, masterVolume);
        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, musicVolume);
        sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, sfxVolume);
        uiVolume = PlayerPrefs.GetFloat(UIVolumeKey, uiVolume);

        Debug.Log("Audio settings loaded.");
    }

    public void ResetVolumeSettings()
    {
        masterVolume = 1f;
        musicVolume = 0.7f;
        sfxVolume = 1f;
        uiVolume = 1f;

        SaveVolumeSettings();
        ApplyVolumes();
    }
}