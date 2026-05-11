using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageZone : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private bool damageRepeatedly = true;
    [SerializeField] private bool damageImmediatelyOnEnter = true;

    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;

    private readonly Dictionary<Health, float> targetsInZone = new();

    private void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void Update()
    {
        if (!damageRepeatedly)
            return;

        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameState.Playing)
            return;

        List<Health> targets = new List<Health>(targetsInZone.Keys);

        foreach (Health health in targets)
        {
            if (health == null || health.IsDead)
            {
                targetsInZone.Remove(health);
                continue;
            }

            if (Time.time >= targetsInZone[health])
            {
                DamageTarget(health);
                targetsInZone[health] = Time.time + damageCooldown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Health health = other.GetComponent<Health>();

        if (health == null)
            return;

        if (!targetsInZone.ContainsKey(health))
        {
            targetsInZone.Add(health, Time.time + damageCooldown);
        }

        if (damageImmediatelyOnEnter)
        {
            DamageTarget(health);

            if (damageRepeatedly)
            {
                targetsInZone[health] = Time.time + damageCooldown;
            }
        }

        if (!damageRepeatedly)
        {
            targetsInZone.Remove(health);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null && targetsInZone.ContainsKey(health))
        {
            targetsInZone.Remove(health);
        }
    }

    private void DamageTarget(Health health)
    {
        health.TakeDamage(damageAmount);

        if (AudioManager.Instance != null && damageSound != null)
        {
            AudioManager.Instance.PlaySFX(damageSound);
        }

        Debug.Log($"{gameObject.name} damaged {health.gameObject.name}.");
    }
}