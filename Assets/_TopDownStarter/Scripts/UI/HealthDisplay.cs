using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Health targetHealth;

    [Header("UI")]
    [SerializeField] private TMP_Text healthText;

    private void OnEnable()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged.AddListener(UpdateDisplay);
        }
    }

    private void OnDisable()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged.RemoveListener(UpdateDisplay);
        }
    }

    private void Start()
    {
        if (targetHealth != null)
        {
            UpdateDisplay(targetHealth.CurrentHealth, targetHealth.MaxHealth);
        }
    }

    private void UpdateDisplay(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth} / {maxHealth}";
        }
    }
}