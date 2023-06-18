using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public event Action<int> OnHealthChanged;
    [SerializeField] int maxHealth;
    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    private int health;
    public int CurrentHealth
    {
        get => health;
        private set
        {
            health = value;
            OnHealthChanged?.Invoke(health);
        }
    }

    void Awake()
    {
        CurrentHealth = maxHealth;
    }
    public void Damage(int damage)
    {
        CurrentHealth -= damage;
    }
}
