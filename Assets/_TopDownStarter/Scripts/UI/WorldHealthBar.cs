using UnityEngine;

public class WorldHealthBar : MonoBehaviour
{
    [Header("Health Source")]
    [SerializeField] private Health health;

    [Header("Bar")]
    [SerializeField] private Transform fillPivot;
    [SerializeField] private GameObject barRoot;

    [Header("Visibility")]
    [SerializeField] private bool hideWhenFull = false;
    [SerializeField] private bool hideWhenDead = true;

    private void Start()
    {
        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }

        if (barRoot == null)
        {
            barRoot = gameObject;
        }

        if (health == null)
        {
            Debug.LogWarning($"{nameof(WorldHealthBar)} could not find Health.");
            return;
        }

        health.OnHealthChanged.AddListener(HandleHealthChanged);

        UpdateBar(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnHealthChanged.RemoveListener(HandleHealthChanged);
        }
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        UpdateBar(currentHealth, maxHealth);
    }

    private void UpdateBar(int currentHealth, int maxHealth)
    {
        if (fillPivot == null)
            return;

        float fillAmount = 0f;

        if (maxHealth > 0)
        {
            fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
        }

        Vector3 scale = fillPivot.localScale;
        scale.x = fillAmount;
        fillPivot.localScale = scale;

        UpdateVisibility(currentHealth, maxHealth);
    }

    private void UpdateVisibility(int currentHealth, int maxHealth)
    {
        if (barRoot == null)
            return;

        bool shouldShow = true;

        if (hideWhenDead && currentHealth <= 0)
        {
            shouldShow = false;
        }

        if (hideWhenFull && currentHealth >= maxHealth)
        {
            shouldShow = false;
        }

        barRoot.SetActive(shouldShow);
    }
}