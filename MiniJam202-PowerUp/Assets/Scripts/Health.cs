using System;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }

    [Tooltip("Includes the amount of damage received.")]
    public UnityEvent<uint> receivedDamage;

    [Tooltip("Inclues the amount of healing received.")]
    public UnityEvent<uint> receivedHealing;

    [Tooltip("Includes the current health value after changes are applied.")]
    public UnityEvent<uint> changedHealth;

    [Tooltip("Invoked once current health reaches zero.")]
    public UnityEvent died;

    [Range(1, 10)]
    public int maximumHealth;

    [Range(0.1f, 5.0f), Tooltip("Seconds of invulnerability after taking damage.")]
    public float invulnerabilityPeriod;

    private bool isInvulnerable;
    private float invulnerabilityTimer;

    void Start()
    {
        CurrentHealth = maximumHealth;
        isInvulnerable = false;
        invulnerabilityTimer = invulnerabilityPeriod;
    }

    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0)
            {
                isInvulnerable = false;
                invulnerabilityTimer = invulnerabilityPeriod;
            }
        }
    }

    public void ReceiveDamage(uint damageAmount)
    {
        if (!isInvulnerable)
        {
            receivedDamage.Invoke(damageAmount);
            ModifyHealth(((int)damageAmount) * -1);
            isInvulnerable = true;
        }
    }

    public void ReceiveHealing(uint healingAmount)
    {
        receivedHealing.Invoke(healingAmount);
        ModifyHealth((int)healingAmount);
    }

    private void ModifyHealth(int healthAmount)
    {
        CurrentHealth += healthAmount;
        CurrentHealth = Math.Clamp(CurrentHealth, 0, maximumHealth);
        changedHealth.Invoke((uint)CurrentHealth);
        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        died.Invoke();
    }

}
