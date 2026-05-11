using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private bool destroyOnPickup = true;
    [Header("Objective")]
    [SerializeField] private CollectAllObjective collectAllObjective;
    [Header("Audio")]
    [SerializeField] private AudioClip pickupSound;

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

        Collect();
    }

    private void Collect()
    {
        hasBeenCollected = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
        else
        {
            Debug.LogWarning("ScoreManager.Instance is missing.");
        }

        if (collectAllObjective != null)
        {
            collectAllObjective.RegisterPickupCollected();
        }

        if (AudioManager.Instance != null && pickupSound != null)
        {
            AudioManager.Instance.PlaySFX(pickupSound);
        }

        Debug.Log($"{gameObject.name} collected.");

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