using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    [Header("Events")]
    public UnityEvent<int, int> OnHealthChanged;
    public UnityEvent OnDied;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
            return;

        if (amount <= 0)
            return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        Debug.Log($"{gameObject.name} health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead)
            return;

        if (amount <= 0)
            return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        Debug.Log($"{gameObject.name} health: {currentHealth}/{maxHealth}");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        OnDied?.Invoke();
    }
}