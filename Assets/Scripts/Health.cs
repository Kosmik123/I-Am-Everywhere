using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action<int> OnChangeHealth;
    public static event Action OnDie;

    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnChangeHealth?.Invoke(health);

        if (health <= 0)
            OnDie?.Invoke();
    }
}
