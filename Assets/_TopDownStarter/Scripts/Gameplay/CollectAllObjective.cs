using UnityEngine;

public class CollectAllObjective : MonoBehaviour
{
    [Header("Objective")]
    [SerializeField] private Pickup[] pickups;

    [Header("Debug")]
    [SerializeField] private int remainingPickups;

    private bool objectiveCompleted;

    private void Start()
    {
        if (pickups == null || pickups.Length == 0)
        {
            pickups = FindObjectsByType<Pickup>(FindObjectsInactive.Exclude);
        }

        remainingPickups = pickups.Length;

        Debug.Log($"Objective started. Pickups remaining: {remainingPickups}");

        if (remainingPickups == 0)
        {
            CompleteObjective();
        }
    }

    public void RegisterPickupCollected()
    {
        if (objectiveCompleted)
            return;

        remainingPickups--;

        Debug.Log($"Pickups remaining: {remainingPickups}");

        if (remainingPickups <= 0)
        {
            CompleteObjective();
        }
    }

    private void CompleteObjective()
    {
        objectiveCompleted = true;

        Debug.Log("Objective complete.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.Victory();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance is missing.");
        }
    }
}