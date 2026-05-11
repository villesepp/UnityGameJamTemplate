using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    [Header("Healing")]
    [SerializeField] private int healAmount = 1;
    [SerializeField] private bool destroyOnPickup = true;
    [Header("Audio")]
    [SerializeField] private AudioClip healSound;

    private bool hasBeenCollected;

    private void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasBeenCollected)
            return;

        if (!other.CompareTag("Player"))
            return;

        Health health = other.GetComponent<Health>();

        if (health == null)
            return;

        Collect(health);
    }

    private void Collect(Health health)
    {
        hasBeenCollected = true;

        health.Heal(healAmount);

        if (AudioManager.Instance != null && healSound != null)
        {
            AudioManager.Instance.PlaySFX(healSound);
        }

        Debug.Log($"{gameObject.name} healed {health.gameObject.name} by {healAmount}.");

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}