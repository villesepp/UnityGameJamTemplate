using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerDeathHandler : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.OnDied.AddListener(HandleDeath);
    }

    private void OnDisable()
    {
        health.OnDied.RemoveListener(HandleDeath);
    }

    private void HandleDeath()
    {
        Debug.Log("Player death handled.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is missing.");
        }
    }
}