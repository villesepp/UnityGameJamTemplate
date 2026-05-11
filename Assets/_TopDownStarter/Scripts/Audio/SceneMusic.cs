using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private bool loop = true;

    private void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogWarning("AudioManager.Instance is missing.");
            return;
        }

        AudioManager.Instance.PlayMusic(musicClip, loop);
    }
}